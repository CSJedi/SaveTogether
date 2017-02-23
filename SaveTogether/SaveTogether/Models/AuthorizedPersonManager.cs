using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SaveTogether.DAL.Entities;
using SaveTogether.DAL.Context;

namespace SaveTogether.Models
{
    public class AuthorizedPersonManager: UserManager<AuthorizedPerson>
    {
        public AuthorizedPersonManager(IUserStore<AuthorizedPerson> store) : base(store) 
        {
        }

        public static AuthorizedPersonManager Create(IdentityFactoryOptions<AuthorizedPersonManager> options, IOwinContext context)
        {
            SaveTogetherContext db = context.Get<SaveTogetherContext>();
            AuthorizedPersonManager manager = new AuthorizedPersonManager(new UserStore<AuthorizedPerson>(db));
            return manager;
        }
    }
}