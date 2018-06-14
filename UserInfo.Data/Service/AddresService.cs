using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInfo.Data.Domain;

namespace UserInfo.Data.Service
{
    public class AddresService : EFRepository<Address>
    {
        /// <summary>
        /// Gets Given User Addreses
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<Address> GetUserAddreses(string userId)
        {
            return GetAll(x => x.UserId == userId).ToList();
        }
    }
}
