using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EarringsDbProj;

namespace EarringsBusinessLogic
{
    public class Class1
    {
        public void DoSomething()
        {
            using(EarringsEntitiesContext context = new EarringsEntitiesContext())
            {
                context.USERSCREDENTIALS.Add(new USERSCREDENTIAL() {USERSID = 1, USERNAME = "User5" });
                context.SaveChanges();
            }
        }
    
    }
}
