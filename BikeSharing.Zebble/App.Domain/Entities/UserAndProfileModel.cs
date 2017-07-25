﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserAndProfileModel
    {
        public int UserId { get; set; }

        public int ProfileId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Skype { get; set; }

        public int TenantId { get; set; }
    }
}