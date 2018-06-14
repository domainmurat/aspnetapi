using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UserInfo.Data.Identity;

namespace UserInfo.Data.Domain
{
    [Table("Department")]
    public class Department : BaseClass
    {
        public Department()
        {
            ApplicationUser = new HashSet<ApplicationUser>();
        }
        public string Name { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}
