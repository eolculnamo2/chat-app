using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Services;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
  [Route("api/[controller]")]
  public class UserAuthenticationController : ControllerBase
  {
    private readonly IUserAuthenticationService userAuthenticationService;
    public UserAuthenticationController(IUserAuthenticationService userAuthenticationService)
    {
      this.userAuthenticationService = userAuthenticationService;
    }

    // POST api/userauthentication
    [HttpPost("register")]
    public bool Post([FromBody] UserRegistration credentials)
    {
      return userAuthenticationService.RegisterUser(credentials);
    }

    [HttpPost("login")]
    public UserRegistration Post([FromBody] LoginCredentials credentials)
    {
      var user = userAuthenticationService.LoginUser(credentials);

      if (user == null)
      {
        Response.StatusCode = 403;
        return null;
      }
      return user;
    }
  }
}