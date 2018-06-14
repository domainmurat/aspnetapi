using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using UserInfo.Data.Context;
using UserInfo.Data.Identity;
using UserInfo.Data.Models;
using UserInfo.Data.Service;

namespace UserInfo.WebApi.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        /// <summary>
        /// Register
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public IdentityResult Register(AccountModel model)
        {
            IdentityResult result = null;
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DepartmentId = model.DepartmentId;
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6
            };
            if (model != null)
            {
                manager.Create(user, model.Password);
            }
            else
            {
                result = new IdentityResult(new string[] { "Please write invalid content." });
            }

            return result;
        }

        [HttpGet]
        [Route("GetUserClaims")]
        public AccountModel GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            AccountModel model = new AccountModel()
            {
                UserName = identityClaims.FindFirst("Username").Value,
                Email = identityClaims.FindFirst("Email").Value,
                FirstName = identityClaims.FindFirst("FirstName").Value,
                LastName = identityClaims.FindFirst("LastName").Value,
                LoggedOn = identityClaims.FindFirst("LoggedOn").Value
            };
            return model;
        }

        /// <summary>
        /// Kullanici Isim- Soy Isim Departman Addres Guncelleme
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Update")]
        public AccountModel Update(AccountModel accountModel)
        {
            UserService userService = new UserService();
            var result = userService.UpdateUserWithAddress(accountModel);

            return result;
        }

        /// <summary>
        /// Aynı Departmandaki kişileri listeleyebilme
        /// </summary>
        /// <param name="departmentId">Gets Given Department Users</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDepartment")]
        public IList<AccountModel> GetDepartment(int departmentId)
        {
            UserService userService = new UserService();
            var result = userService.GetGivenDepartmentUsers(departmentId);

            return result;
        }

        /// <summary>
        ///  Departman listesindeki bir kişi detaylarını listeleme
        /// </summary>
        /// <param name="userId">Gets Given User Detail If It is in a Department</param>
        /// <returns></returns>
        [HttpPost]
        [Route("DepartmentUserDetail")]
        public AccountModel GetUserDetailIfIsInDepartment(string userId)
        {
            UserService userService = new UserService();
            var result = userService.GetUserDetailIfIsInDepartment(userId);

            return result;
        }
    }
}
