using EarringsBusinessLogic.Authentication.Contracts;
using EarringsBusinessLogic.Enums;
using EarringsDbProj;
using System;
using System.Linq;

namespace EarringsBusinessLogic.Authentication
{
    public class UserFactory : IUserFactory
    {
        public AuthenticationResult CreateUser(string email, string username, string password, string token)
        {
            PasswordManager passwordManager = new PasswordManager();
            byte[] encryptedPassword = passwordManager.GetEncryptedPassword(username, password);

            try
            {
                using(EarringsDatabaseEntities ctx = new EarringsDatabaseEntities())
                {
                    UsersCredentials existingRecord = ctx.UsersCredentials.Where(user => user.UserEmail == email || user.Username == username).FirstOrDefault();
                    if(existingRecord != null)
                    {
                        if(existingRecord.UserEmail == email)
                        {
                            return AuthenticationResult.EmailTaken;
                        }
                        else
                        {
                            return AuthenticationResult.UsernameTaken;
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
                    return AuthenticationResult.Success;
                }
            }
            catch(Exception e)
            {
                return AuthenticationResult.Fail;
            }                       
        }
    }
}
