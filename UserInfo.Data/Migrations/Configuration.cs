namespace UserInfo.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using UserInfo.Data.Domain;
    using UserInfo.Data.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<UserInfo.Data.Context.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UserInfo.Data.Context.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            if (!context.Department.Any())
            {
                context.Department.AddOrUpdate(
                             new Department { Name = "IT" },
                             new Department { Name = "AI" },
                             new Department { Name = "ML" }
                           );
                context.SaveChanges();
            }

            var user = new ApplicationUser
            {
                FirstName = "Murat Can",
                LastName = "OGUZHAN",
                Email = "m.c.ogzhan@gmail.com",
                UserName = "muratcanoguzhan",
                PhoneNumber = "905388758610",
                DepartmentId = 1
            };

            var user2 = new ApplicationUser
            {
                FirstName = "Soyhan",
                LastName = "BEYAZIT",
                Email = "soyhan.beyazit@mobilion.com.tr",
                UserName = "soyhanbeyazit",
                DepartmentId = 1
            };

            var user3 = new ApplicationUser
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test@test.com",
                UserName = "testuser",
                DepartmentId = 2
            };

            if (!context.Users.Any())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(userStore);
                manager.Create(user, "123456");
                manager.Create(user2, "admin1");
                manager.Create(user3, "admin1");

                if (!context.Address.Any())
                {
                    context.Address.AddOrUpdate(
                                 new Address { Name = "Loreipsum Sokak Loresiramed Mah Uskudar Istanbul", UserId = user.Id },
                                 new Address { Name = "Emniyet Mah Mah No:1 Uskudar/Istanbul", UserId = user.Id },
                                 new Address { Name = "Gayrettepe Sisli/Istanbul", UserId = user2.Id }
                               );
                    context.SaveChanges();
                }
            }

        }
    }
}
