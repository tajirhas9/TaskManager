using System;
using TaskManager.Data.Abstract;
using TaskManager.Model;

namespace TaskManager.Data.Repositories {
    public class UserRepo : EntityBaseRepo<User>, IUserRepo {
        public UserRepo(TaskManagerContext context): base(context) {}

        public bool IsEmailUnique(string email) {
            var user = this.GetSingle(u => u.Email == email);
            return user == null;
        }

        public bool IsUsernameUnique(string username) {
            var user = this.GetSingle(u => u.Username == username);
            return user == null;
        }
    }
}
