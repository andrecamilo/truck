using Microsoft.EntityFrameworkCore;
using Truck.Infra.Database.Entities;

namespace Truck.Infra.Database
{
    public class TruckContext : DbContext
    {
        public DbSet<ColorOption> Color { get; set; }
        public DbSet<Entities.Truck> Truck { get; set; }

        public TruckContext(DbContextOptions<TruckContext> options) : base(options) {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // Configura a chave primária da entidade Caminhão que não pode ser
            //  determinada por padrão.
            modelBuilder.Entity<Truck.Infra.Database.Entities.Truck>().HasKey(p => p.Chassis);

            // Ignore explicitamente o campo "TypeDesc" já que ele é uma propriedade computada
            modelBuilder.Entity<Truck.Infra.Database.Entities.Truck>().Ignore(p => p.ModelDesc);

            // Inicializa o conjunto de cores padrões do banco de dados.
            modelBuilder.Entity<ColorOption>().HasData(
                new ColorOption { Id = 1, Name = "Vermelho", Red = 0xFF, Green = 0x00, Blue = 0x00 },
                new ColorOption { Id = 2, Name = "Azul", Red = 0x00, Green = 0x00, Blue = 0xFF },
                new ColorOption { Id = 3, Name = "Verde", Red = 0x00, Green = 0xFF, Blue = 0x00 },
                new ColorOption { Id = 4, Name = "Amarelo", Red = 0xFF, Green = 0xFF, Blue = 0x06 },
                new ColorOption { Id = 5, Name = "Laranja", Red = 0xFF, Green = 0x99, Blue = 0x33 },
                new ColorOption { Id = 6, Name = "Cinza", Red = 0xA9, Green = 0xA9, Blue = 0xA9 });
        }
    }
}
