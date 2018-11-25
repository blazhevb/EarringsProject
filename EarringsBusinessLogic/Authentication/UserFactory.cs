using EarringsBusinessLogic.Authentication.Contracts;
using EarringsDbProj;
using System;
using System.Linq;

namespace EarringsBusinessLogic.Authentication
{
    public class UserFactory : IUserFactory
    {
        public string CreateUser(string email, string username, string password)
        {
            PasswordManager passwordManager = new PasswordManager();
            byte[] encryptedPassword = passwordManager.GetEncryptedPassword(username, password);
            Token t = new Token();
            string token = t.GetToken();

            using(EarringsDatabaseEntities ctx = new EarringsDatabaseEntities())
            {
                UsersCredentials existingRecord = ctx.UsersCredentials.Where(user => user.UserEmail == email || user.Username == username).FirstOrDefault();
                if(existingRecord != null)
                {
                    if(existingRecord.UserEmail == email)
                    {
                        return "There is already a user with this email.";
                    }
                    else
                    {
                        return "This username is already taken.";
                    }
                }

                UsersCredentials credentials = new UsersCredentials()
                {
                    UserEmail = email,
                    Username = username,
                    UsernamePassword = encryptedPassword,
                    UserRegistrationDate = DateTime.Now,
                    UserToken = token
                };

                ctx.UsersCredentials.Add(credentials);
                ctx.SaveChanges();
            }

            return null;
        }
    }
}
