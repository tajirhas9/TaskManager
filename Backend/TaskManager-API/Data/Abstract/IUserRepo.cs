using System;
using TaskManager.Model;

namespace TaskManager.Data.Abstract {
    public interface IUserRepo : IEntityBaseRepo<User> {
        bool IsUsernameUnique(string username);
        bool IsEmailUnique(string email);
    }
}
