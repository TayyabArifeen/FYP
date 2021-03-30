﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProjectFY.Models
{
    public class AppDbContext:DbContext
    {

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }


        public virtual DbSet<UserAccount> UserAccounts { get; set; }

    }
}
