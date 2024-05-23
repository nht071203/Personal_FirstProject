using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Personal_FirstProject.Models
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
            
        }
        public DbSet<User> Users {get; set;}
        public DbSet<EmailQueue> EmailQueue {get; set;}
        public DbSet<OTP> OTPs {get; set;}
        public DbSet<ActivityHistory> ActivityHistories {get; set;}

    }
}