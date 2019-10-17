using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SamuraiApp.Domain;
using System.Configuration;

namespace SamuraiApp.Data
{
    public class SamuraiContext : DbContext
    {
        public static readonly LoggerFactory MyConsoleLoggerFactory = new LoggerFactory(new []
        {
            new ConsoleLoggerProvider((category, level)
                => category == DbLoggerCategory.Database.Command.Name 
                   && level == LogLevel.Information,true)

        });

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["WPFDatabase"].ToString();
            optionsBuilder
                .UseLoggerFactory(MyConsoleLoggerFactory)
                .UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>()
                .HasKey((s => new {s.SamuraiId, s.BattleId}));
        }
    }
}
