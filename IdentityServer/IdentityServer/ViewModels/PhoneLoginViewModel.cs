using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.ViewModels
{
    public class PhoneLoginViewModel
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        [JsonProperty("phone")]
        public string PhoneNumber { get; set; }
    }
}
