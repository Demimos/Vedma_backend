using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vedma_backend.Entity;

namespace Vedma_backend
{
    public class ApplicationContext : DbContext
    {
        public DbSet<CharSheet> CharSheets { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
