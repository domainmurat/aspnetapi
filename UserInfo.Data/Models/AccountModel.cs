using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfo.Data.Models
{
    public class AccountModel
    {
        public AccountModel()
        {
            Address = new List<AddressModel>();
        }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public string LoggedOn { get; set; }
        public virtual string PhoneNumber { get; set; }
        public IList<AddressModel> Address { get; set; }
    }
}
