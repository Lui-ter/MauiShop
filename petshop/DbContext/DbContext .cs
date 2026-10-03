using Microsoft.EntityFrameworkCore;

namespace petshop.DataAcces
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Client> Clients { get; set; }


        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("ShopPet");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category(1, "Electricos"),
                new Category(2, "Computadoras"),
                new Category(3, "Telefonos Moviles"),
                new Category(4, "Dispositivos de Escritorio"),
                new Category(5, "Microfonos y Audio"),
                new Category(6, "Artefactos del Hogar"),
                new Category(7, "Juguetes y Juegos")
            );


            modelBuilder.Entity<Product>().HasData(
                new Product(1, "Radio Digital", "Es una radio de banda ancha", 100, 1),
                new Product(2, "Reloj electrónico", "Reloj digital sumergible", 50, 1),
                new Product(3, "Laptop HP", "Laptop Escritorio", 900, 2),
                new Product(4, "Laptop Acer", "Laptop Gamer", 1200, 2),
                new Product(5, "Macbook Apple", "Gran Capacidad", 1500, 2),
                new Product(6, "Samsung Galaxy", "Smartphone", 1800, 3),
                new Product(7, "Iphone 13", "Es un smartphone", 1500, 3)
            );


            modelBuilder.Entity<Client>().HasData(
                new Client(1, "Alfredo", "Magno", "Calle 123"),
                new Client(2, "Luis", "Sanchez", "Calle 2"),
                new Client(3, "Kevin", "Llandel", "Calle 789")
            );

            base.OnModelCreating(modelBuilder);
        }
    }


    public record Category(int Id, string Nombre);

    public record Product(
        int Id,
        string Nombre,
        string Descripcion,
        decimal Precio,
        int CategoryId)
    {
        public Category? Category { get; set; }
    }

    public record Client(
        int Id,
        string Nombre,
        string Apellido,
        string Direccion);
}