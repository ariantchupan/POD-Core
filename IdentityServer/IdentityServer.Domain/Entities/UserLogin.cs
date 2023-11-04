using System.ComponentModel.DataAnnotations;
using IdentityServer.Domain.Common;

namespace IdentityServer.Domain.Entities
{
    public class UserLogin : EntityBase, IConcurrencyAware
    {

        [MaxLength(200)]
        [Required]
        public string Provider { get; set; }

        [MaxLength(200)]
        [Required]
        public string ProviderIdentityKey { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; }

        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    }
}
