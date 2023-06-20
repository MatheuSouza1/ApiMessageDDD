﻿using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Column("USR_CPF")]
        public string cpf { get; set; }
        [Column("USR_TYPE")]
        public UserType? userType { get; set; }
    }
}