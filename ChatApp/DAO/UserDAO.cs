using System;
using ChatApp.Models;

namespace ChatApp.DAO {

    public interface IUserDAO {
        void RegisterUser (LoginCredentials loginCredentials);
    }

    public class UserDAO : IUserDAO {
        public void RegisterUser (LoginCredentials loginCredentials) {
            using (var context = new ChatContext ()) {
                try {
                    context.LoginCredentials.Add (loginCredentials);
                    context.SaveChanges ();
                } catch (Exception e) {
                    Console.WriteLine (e);
                }
            }
        }
    }
}