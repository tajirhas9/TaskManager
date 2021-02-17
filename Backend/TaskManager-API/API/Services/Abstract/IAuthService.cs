using System;
using TaskManager.API.ViewModels;

namespace TaskManager.API.Services.Abstract {
    public interface IAuthService {
        string HashPassword(string password);
        bool VerifyPassword(string actualPassword, string hashedPassword);
        AuthData GetAuthData(string id);
    }
}
