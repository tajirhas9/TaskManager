﻿using System;
namespace TaskManager.API.ViewModels {
    public class AuthData {
        public string Token { get; set; }
        public long TokenExpirationTime { get; set; }
        public string Id { get; set; }
    }
}
