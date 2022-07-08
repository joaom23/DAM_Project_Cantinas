using DAM_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AdminCantina> AdminsCantinas { get; set; }
        public DbSet<Prato> Pratos { get; set; }
        public DbSet<PratoDia> PratosDia { get; set; }
        public DbSet<Cantina> Cantinas { get; set; }
        public DbSet<Localizacao> Localizacoes { get; set; }
        public DbSet<Suspensões> Suspensões { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
    }
}
