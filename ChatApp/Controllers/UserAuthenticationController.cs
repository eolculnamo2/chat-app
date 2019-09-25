using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.DAO;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers {
    [Route ("api/[controller]")]
    public class UserAuthenticationController : ControllerBase {
        private readonly IUserDAO userDAO;
        public UserAuthenticationController (IUserDAO userDAO) {
            this.userDAO = userDAO;
        }

        // POST api/userauthentication
        [HttpPost ("register")]
        public bool Post ([FromBody] LoginCredentials credentials) {
            try {
                userDAO.RegisterUser (credentials);
                return true;
            } catch (Exception e) {
                Console.WriteLine (e);
                return false;
            }
        }
    }
}