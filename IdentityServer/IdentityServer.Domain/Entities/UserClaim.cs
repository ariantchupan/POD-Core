using System.ComponentModel.DataAnnotations;
using IdentityServer.Domain.Common;

namespace IdentityServer.Domain.Entities
{
    public class UserClaim : EntityBase, IConcurrencyAware
    {

        [MaxLength(250)]
        [Required]
        public string Type { get; set; }

        [MaxLength(250)]
        [Required]
        public string Value { get; set; }

        [ConcurrencyCheck]
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
