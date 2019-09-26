using System;
using ChatApp.Models;
using ChatApp.DAO;
using BC = BCrypt.Net;

namespace ChatApp.Services
{
  public interface IUserAuthenticationService
  {
    bool RegisterUser(UserRegistration userDetails);
    UserRegistration LoginUser(LoginCredentials loginCredentials);
  }
  public class UserAuthenticationService : IUserAuthenticationService

  {
    private readonly IUserDAO userDAO;
    public UserAuthenticationService(IUserDAO userDAO)
    {
      this.userDAO = userDAO;
    }

    public bool RegisterUser(UserRegistration userDetails)
    {

      try
      {
        userDetails.Password = BC.BCrypt.HashPassword(userDetails.Password);
        userDAO.RegisterUser(userDetails);
        return true;
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return false;
      }

    }

    public UserRegistration LoginUser(LoginCredentials loginCredentials)
    {
      UserRegistration user = userDAO.GetUserByUsername(loginCredentials.Username);
      bool verified = BC.BCrypt.Verify(loginCredentials.Password, user.Password);
      if (verified)
      {
        return user;
      }
      return null;
    }
  }
}