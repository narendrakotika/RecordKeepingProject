﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecordKeepingProject.Models
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}