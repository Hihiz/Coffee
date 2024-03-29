﻿using Coffee.Application.Interfaces;

namespace Coffee.Application.Models
{
    public class StatusAuthResponse : IBaseStatus
    {
        public long? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
