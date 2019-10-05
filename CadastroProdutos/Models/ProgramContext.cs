using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroProdutos.Models
{
    public class ProgramContext : IdentityDbContext<Usuario>
    {
        public ProgramContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Usuario>().HasData(
                new Usuario
                {
                    UserName = "admin",
                    Email = "admin@admin.com.br",
                    PasswordHash = "202cb962ac59075b964b07152d234b70"
                }
            );
        }
    }
}
