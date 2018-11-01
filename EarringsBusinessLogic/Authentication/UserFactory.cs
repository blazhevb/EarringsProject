using EarringsBusinessLogic.Authentication.Contracts;
using EarringsDbProj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarringsBusinessLogic.Authentication
{
    public class UserFactory : IUserFactory
    {
        public void CreateUser(string email, string username, string password)
        {
            USERSCREDENTIAL credential = new USERSCREDENTIAL() { USERNAME = username, USERNAMEPASSWORD = password };

            using(EarringsEntitiesContext ctx = new EarringsEntitiesContext())
            {
                ctx.USERSCREDENTIALS.Add(credential);
                ctx.SaveChanges();
            }
        }
    }
}
