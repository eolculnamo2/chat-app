using System;
using ChatApp.Models;

namespace ChatApp.DAO
{

  public interface IUserDAO
  {
    void RegisterUser(UserRegistration userDetails);
    UserRegistration GetUserByUsername(string username);
  }

  public class UserDAO : IUserDAO
  {
    public void RegisterUser(UserRegistration userDetails)
    {
      using (var context = new ChatContext())
      {
        try
        {
          context.UserRegistrations.Add(userDetails);
          context.SaveChanges();
        }
        catch (Exception e)
        {
          Console.WriteLine(e);
        }
      }
    }

    public UserRegistration GetUserByUsername(string username)
    {
      using (var context = new ChatContext())
      {
        try
        {
          return context.UserRegistrations.Find(username);
        }
        catch (Exception e)
        {
          Console.WriteLine(e);
          return null;
        }
      }
    }
  }
}