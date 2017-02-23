using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SaveTogether.DAL.Entities;
using SaveTogether.DAL.Context;

namespace SaveTogether.Models
{
    public class AutorizedPersonManager: UserManager<AuthorizedPerson>
    {
        public AutorizedPersonManager(IUserStore<AuthorizedPerson> store) : base(store) 
        {
        }

        public static AutorizedPersonManager Create(IdentityFactoryOptions<AutorizedPersonManager> options, IOwinContext context)
        {
            SaveTogetherContext db = context.Get<SaveTogetherContext>();
            AutorizedPersonManager manager = new AutorizedPersonManager(new UserStore<AuthorizedPerson>(db));
            return manager;
        }
    }
}