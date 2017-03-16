using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SaveTogether.DAL.Context;
using SaveTogether.DAL.Entities;

namespace SaveTogether.Models
{
    public class SaveTogetherDbInitializer : DropCreateDatabaseAlways<SaveTogetherContext>
    {
        protected override void Seed(SaveTogetherContext db)
        {
            CreateRolesAndUsers(ref db);
            CreateRegions(ref db);
            base.Seed(db);
        }

        protected void CreateRegions(ref SaveTogetherContext db)
        {
            db.Regions.Add(new Region
            {
                Name = "Forest near the villages of Madirobe ",
                Population = 50,
                Description = "This region is small remaining patche in Madagascar’s far northern regions."
            });
            db.Regions.Add(new Region
            {
                Name = "Forest near the villages of Ankarongana",
                Population = 65,
                Description = "This region locate on Madagascar’s far north in the Sahafary region."
            });
            db.Regions.Add(new Region
            {
                Name = "In the vicinity of Andrahona",
                Population = 75,
                Description = "A small mountain emerging out of the surrounding lowlands about 30 km south of Antsiranana. This species has only been recorded at elevations below 300 m."
            });
        }

        protected void CreateRolesAndUsers(ref SaveTogetherContext db)
        {
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            UserManager<Administrator> userManager = new UserManager<Administrator>(new UserStore<Administrator>(db));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new Administrator();
                user.UserName = "Admin";
                user.Email = "admin@gmail.com";

                string userPWD = "admin123";

                var chkUser = userManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");

                }
            }

            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }
        }
    }
}