using System.ComponentModel.DataAnnotations.Schema;
using UserInfo.Data.Identity;

namespace UserInfo.Data.Domain
{
    [Table("Address")]
    public class Address : BaseClass
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
