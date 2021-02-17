using System;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Services.Abstract;
using TaskManager.API.ViewModels;
using TaskManager.Data.Abstract;
using TaskManager.Model;

namespace TaskManager.Controllers {
    public class AuthController : ControllerBase {
        IAuthService AuthService;
        IUserRepo UserRepo;
        public AuthController(IAuthService authService, IUserRepo userRepository) {
            this.AuthService = authService;
            this.UserRepo = userRepository;
        }


        [HttpPost("login")]
        public ActionResult<AuthData> Post([FromBody] LoginViewModel model) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = UserRepo.GetSingle(u => u.Email == model.Email);

            if (user == null) {
                return BadRequest(new { email = "no user with this email" });
            }

            var passwordValid = AuthService.VerifyPassword(model.Password, user.Password);
            if (!passwordValid) {
                return BadRequest(new { password = "invalid password" });
            }

            return AuthService.GetAuthData(user.Id);
        }

        [HttpPost("register")]
        public ActionResult<AuthData> Post([FromBody] RegisterViewModel model) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var emailUniq = UserRepo.IsEmailUnique(model.Email);
            if (!emailUniq) return BadRequest(new { email = "user with this email already exists" });
            var usernameUniq = UserRepo.IsUsernameUnique(model.Username);
            if (!usernameUniq) return BadRequest(new { username = "user with this email already exists" });

            var id = Guid.NewGuid().ToString();
            var user = new User {
                Id = id,
                Username = model.Username,
                Email = model.Email,
                Password = AuthService.HashPassword(model.Password)
            };
            UserRepo.Add(user);
            UserRepo.Commit();

            return AuthService.GetAuthData(id);
        }
    }
}
