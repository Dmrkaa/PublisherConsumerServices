using AutoMapper;
using ServiceConsumer.Models;
using System;
using System.Collections.Generic;

namespace ServiceConsumer.Dtos
{
    public class UserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
    }
}
