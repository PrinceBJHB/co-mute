﻿using CoMute.Model.Interface;
using System.Collections.Generic;
using System;

namespace CoMute.Model.Models
{
    public class User : IUser
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public List<CarPool> JoinedCarpools { get; set; }        
    }
}