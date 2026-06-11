using Microsoft.EntityFrameworkCore;
using Proyecto.Models;

namespace Proyecto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Emprendimiento> Emprendimientos { get; set; } = null!;
        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<Pedido> Pedidos { get; set; } = null!;
        public DbSet<DetallePedido> DetallesPedido { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Emprendimientos)
                .WithOne(e => e.Usuario)
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Pedidos)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Emprendimiento>()
                .HasMany(e => e.Productos)
                .WithOne(p => p.Emprendimiento)
                .HasForeignKey(p => p.EmprendimientoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Productos)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Detalles)
                .WithOne(d => d.Pedido)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Producto>()
                .HasMany(p => p.DetallesPedido)
                .WithOne(d => d.Producto)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Categoria>()
                .HasIndex(c => c.Nombre)
                .IsUnique(false);

            modelBuilder.Entity<Producto>()
                .HasIndex(p => p.Nombre)
                .IsUnique(false);
        }

        public static async Task SeedData(AppDbContext context)
        {
            if (await context.Usuarios.AnyAsync()) return;

            var passwordHasher = new Microsoft.AspNetCore.Identity.PasswordHasher<Usuario>();

            // 1. Usuarios
            var admin = new Usuario
            {
                Nombre = "Admin",
                Email = "admin@emprendemarket.com",
                EsCliente = true,
                EsEmprendedor = true,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };
            admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin123!");

            var juan = new Usuario
            {
                Nombre = "Juan Perez",
                Email = "juan@gmail.com",
                EsCliente = true,
                EsEmprendedor = false,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };
            juan.PasswordHash = passwordHasher.HashPassword(juan, "Juan123!");

            var maria = new Usuario
            {
                Nombre = "Maria Garcia",
                Email = "maria@emprendedora.com",
                EsCliente = true,
                EsEmprendedor = true,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };
            maria.PasswordHash = passwordHasher.HashPassword(maria, "Maria123!");

            await context.Usuarios.AddRangeAsync(admin, juan, maria);
            await context.SaveChangesAsync();

            // 2. Categorías
            var catArtesania = new Categoria { Nombre = "Artesanías", Descripcion = "Productos hechos a mano", Activo = true };
            var catComida = new Categoria { Nombre = "Comida", Descripcion = "Alimentos y postres caseros", Activo = true };
            var catRopa = new Categoria { Nombre = "Ropa", Descripcion = "Prendas de vestir y accesorios", Activo = true };

            await context.Categorias.AddRangeAsync(catArtesania, catComida, catRopa);
            await context.SaveChangesAsync();

            // 3. Emprendimientos
            var empMaria = new Emprendimiento
            {
                Nombre = "Tejidos Maria",
                Descripcion = "Ropa tejida a mano con lana de alta calidad",
                UsuarioId = maria.Id,
                Activo = true
            };

            var empAdmin = new Emprendimiento
            {
                Nombre = "Dulces Tentaciones",
                Descripcion = "Los mejores postres de la ciudad",
                UsuarioId = admin.Id,
                Activo = true
            };

            await context.Emprendimientos.AddRangeAsync(empMaria, empAdmin);
            await context.SaveChangesAsync();

            // 4. Productos
            var prodSaco = new Producto
            {
                Nombre = "Saco de Lana",
                Descripcion = "Saco tejido a mano color azul",
                Precio = 25.50m,
                Stock = 10,
                CategoriaId = catRopa.Id,
                EmprendimientoId = empMaria.Id,
                Activo = true
            };

            var prodBufanda = new Producto
            {
                Nombre = "Bufanda Infinita",
                Descripcion = "Bufanda suave color gris",
                Precio = 12.00m,
                Stock = 20,
                CategoriaId = catRopa.Id,
                EmprendimientoId = empMaria.Id,
                Activo = true
            };

            var prodTorta = new Producto
            {
                Nombre = "Torta de Chocolate",
                Descripcion = "Torta húmeda de chocolate para 10 personas",
                Precio = 18.00m,
                Stock = 5,
                CategoriaId = catComida.Id,
                EmprendimientoId = empAdmin.Id,
                Activo = true
            };

            await context.Productos.AddRangeAsync(prodSaco, prodBufanda, prodTorta);
            await context.SaveChangesAsync();

            // 5. Pedidos y Detalles
            var pedido1 = new Pedido
            {
                UsuarioId = juan.Id,
                FechaPedido = DateTime.UtcNow,
                Total = 37.50m,
                Activo = true
            };

            await context.Pedidos.AddAsync(pedido1);
            await context.SaveChangesAsync();

            var detalle1 = new DetallePedido
            {
                PedidoId = pedido1.Id,
                ProductoId = prodSaco.Id,
                Cantidad = 1,
                PrecioUnitario = 25.50m,
                Activo = true
            };

            var detalle2 = new DetallePedido
            {
                PedidoId = pedido1.Id,
                ProductoId = prodBufanda.Id,
                Cantidad = 1,
                PrecioUnitario = 12.00m,
                Activo = true
            };

            await context.DetallesPedido.AddRangeAsync(detalle1, detalle2);
            await context.SaveChangesAsync();
        }
    }
}