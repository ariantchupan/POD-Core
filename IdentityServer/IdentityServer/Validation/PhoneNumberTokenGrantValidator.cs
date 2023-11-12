using System.Linq;

using IdentityModel;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System.Threading.Tasks;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Validation
{
    public class PhoneNumberTokenGrantValidator : IExtensionGrantValidator
    {
        private readonly PhoneNumberTokenProvider<IdentityUser> _phoneNumberTokenProvider;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEventService _events;
        private readonly ILogger<PhoneNumberTokenGrantValidator> _logger;

        public string GrantType => Constants.AuthConstants.GrantType.PhoneNumberToken;

        public PhoneNumberTokenGrantValidator(
            PhoneNumberTokenProvider<IdentityUser> phoneNumberTokenProvider,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEventService events,
            ILogger<PhoneNumberTokenGrantValidator> logger)
        {
            _phoneNumberTokenProvider = phoneNumberTokenProvider;
            _userManager = userManager;
            _signInManager = signInManager;
            _events = events;
            _logger = logger;
        }
        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var createUser = false;
            var raw = context.Request.Raw;
            var credential = raw.Get(OidcConstants.TokenRequest.GrantType);
            if (credential == null || credential != Constants.AuthConstants.GrantType.PhoneNumberToken)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant,
                    "invalid verify_phone_number_token credential");
                return;
            }

            var phoneNumber = raw.Get(Constants.AuthConstants.TokenRequest.PhoneNumber);
            var verificationToken = raw.Get(Constants.AuthConstants.TokenRequest.Token);

            var user = await _userManager.Users.SingleOrDefaultAsync(x =>
                x.PhoneNumber == _userManager.NormalizeName(phoneNumber));
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = phoneNumber,
                    PhoneNumber = phoneNumber,
                    SecurityStamp = new Secret("your_secret_key").Value + phoneNumber.Sha256()
                };
                createUser = true;
            }
            var result =
                await _phoneNumberTokenProvider.ValidateAsync("verify_number", verificationToken, _userManager, user);
            if (!result)
            {
                _logger.LogInformation("Authentication failed for token: {token}, reason: invalid token",
                    verificationToken);
                await _events.RaiseAsync(new UserLoginFailureEvent(verificationToken,
                    "invalid token or verification id", false));
                return;
            }

            if (createUser)
            {
                user.PhoneNumberConfirmed = true;
                var resultCreation = await _userManager.CreateAsync(user);
                if (resultCreation != IdentityResult.Success)
                {
                    _logger.LogInformation("User creation failed: {username}, reason: invalid user", phoneNumber);
                    await _events.RaiseAsync(new UserLoginFailureEvent(phoneNumber,
                        resultCreation.Errors.Select(x => x.Description).Aggregate((a, b) => a + ", " + b), false));
                    return;
                }
            }

            _logger.LogInformation("Credentials validated for username: {phoneNumber}", phoneNumber);
            await _events.RaiseAsync(new UserLoginSuccessEvent(phoneNumber, user.Id, phoneNumber, false));
            await _signInManager.SignInAsync(user, true);
            context.Result = new GrantValidationResult(user.Id, OidcConstants.AuthenticationMethods.ConfirmationBySms);
        }
    }
}
