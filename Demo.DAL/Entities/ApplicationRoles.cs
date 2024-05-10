﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
    public class ApplicationRoles : IdentityRole
    {
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }

}
