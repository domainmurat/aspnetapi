using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInfo.Data.Domain;
using UserInfo.Data.Identity;
using UserInfo.Data.Mapping;
using UserInfo.Data.Models;

namespace UserInfo.Data.Service
{
    public class UserService : EFRepository<ApplicationUser>
    {
        readonly AddresService addresService;
        public UserService()
        {
            addresService = new AddresService();
        }
        /// <summary>
        /// This method using just exist User
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        public AccountModel UpdateUserWithAddress(AccountModel accountModel)
        {
            try
            {
                var user = Get(x => x.Id == accountModel.Id);
                var addreses = addresService.GetUserAddreses(accountModel.Id);

                user.UserName = accountModel.UserName;
                user.Email = accountModel.Email;
                user.FirstName = accountModel.FirstName;
                user.LastName = accountModel.LastName;
                user.DepartmentId = accountModel.DepartmentId;
                user.PhoneNumber= accountModel.PhoneNumber;

                Update(user);

                foreach (var addressModel in accountModel.Address)
                {
                    var address = addreses.FirstOrDefault(x => x.Id == addressModel.Id);

                    if (address != null)
                    {
                        address.UserId = addressModel.UserId;
                        address.Name = addressModel.Name;
                        addresService.Update(address);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(addressModel.UserId))
                        {
                            var createdAddres = addresService.Add(addressModel.MapTo<AddressModel, Address>(cfg =>
                                 { cfg.CreateMap<AddressModel, Address>(); }
                            ));
                            addressModel.Id = createdAddres.Id;
                        }
                    }
                }

                return accountModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IList<AccountModel> GetGivenDepartmentUsers(int departmentId)
        {
            try
            {
                IList<AccountModel> accountModels = new List<AccountModel>();
                var users = GetAll(x => x.DepartmentId == departmentId).ToList();

                var mappedUserModel = users.MapTo<ApplicationUser, AccountModel>(cfg =>
                {
                    cfg.CreateMap<ApplicationUser, AccountModel>();
                    cfg.CreateMap<Address, AddressModel>();
                });

                return mappedUserModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public AccountModel GetUserDetailIfIsInDepartment(string userId)
        {
            try
            {
                var user = Get(x => x.Id == userId);

                if (user != null && user.DepartmentId > 0)
                {
                    return user.MapTo<ApplicationUser, AccountModel>(cfg =>
                    {
                        cfg.CreateMap<ApplicationUser, AccountModel>();
                        cfg.CreateMap<Address, AddressModel>();
                    });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //public AccountModel UpdateUserWithAddress(AccountModel accountModel)
        //{
        //    var userStore = new UserStore<ApplicationUser>(this._dbContext);
        //    var manager = new UserManager<ApplicationUser>(userStore);
        //    ApplicationUser user =manager.FindById(accountModel.Id);

        //    //we update password
        //    var validPass = manager.CheckPassword(user, accountModel.Password);

        //    user.PasswordHash = manager.PasswordHasher.HashPassword(accountModel.Password);
        //    var result = manager.UpdateAsync(user);

        //    var addreses = addresService.GetUserAddreses(accountModel.Id);

        //    user.FirstName = accountModel.FirstName;
        //}
    }
}
