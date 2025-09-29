using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_questoes.Models;
using Microsoft.EntityFrameworkCore;

namespace api_questoes.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Questao> Questoes { get; set; }
    }
}