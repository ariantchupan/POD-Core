using System.ComponentModel.DataAnnotations;
using IdentityServer.Domain.Common;

namespace IdentityServer.Domain.Entities
{
    public class UserSecret : EntityBase, IConcurrencyAware
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Secret { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; }

        public string ConcurrencyStamp { get; set; } =
            Guid.NewGuid().ToString();
    }
}
