using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
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

            modelBuilder.Entity<Producto>()
                .HasIndex(p => p.Nombre);

            modelBuilder.Entity<Categoria>()
                .HasIndex(c => c.Nombre);
        }

        // =========================
        // SEED DATA + GENERADOR MASIVO
        // =========================
        public static async Task SeedData(AppDbContext context)
        {
            // 🔥 CAMBIO IMPORTANTE: ahora se controla por PEDIDOS
            if (await context.Pedidos.AnyAsync())
                return;

            var hasher = new PasswordHasher<Usuario>();

            // =====================
            // USUARIOS
            // =====================
            var admin = new Usuario
            {
                Nombre = "Admin",
                Email = "admin@emprendemarket.com",
                EsCliente = true,
                EsEmprendedor = true,
                EsAdministrador = true,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };
            admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");

            var juan = new Usuario
            {
                Nombre = "Juan Perez",
                Email = "juan@gmail.com",
                EsCliente = true,
                EsEmprendedor = false,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };
            juan.PasswordHash = hasher.HashPassword(juan, "Juan123!");

            var maria = new Usuario
            {
                Nombre = "Maria Garcia",
                Email = "maria@emprendedora.com",
                EsCliente = true,
                EsEmprendedor = true,
                Activo = true,
                FechaCreacion = DateTime.UtcNow
            };
            maria.PasswordHash = hasher.HashPassword(maria, "Maria123!");

            await context.Usuarios.AddRangeAsync(admin, juan, maria);
            await context.SaveChangesAsync();

            // =====================
            // CATEGORÍAS
            // =====================
            var cat1 = new Categoria { Nombre = "Artesanías", Activo = true };
            var cat2 = new Categoria { Nombre = "Comida", Activo = true };
            var cat3 = new Categoria { Nombre = "Ropa", Activo = true };

            await context.Categorias.AddRangeAsync(cat1, cat2, cat3);
            await context.SaveChangesAsync();

            // =====================
            // EMPRENDIMIENTOS
            // =====================
            var emp1 = new Emprendimiento
            {
                Nombre = "Tejidos Maria",
                UsuarioId = maria.Id,
                Activo = true
            };

            var emp2 = new Emprendimiento
            {
                Nombre = "Dulces Tentaciones",
                UsuarioId = admin.Id,
                Activo = true
            };

            await context.Emprendimientos.AddRangeAsync(emp1, emp2);
            await context.SaveChangesAsync();

            // =====================
            // PRODUCTOS
            // =====================
            var p1 = new Producto
            {
                Nombre = "Saco Lana",
                Precio = 25.50m,
                Stock = 10,
                CategoriaId = cat3.Id,
                EmprendimientoId = emp1.Id,
                Activo = true
            };

            var p2 = new Producto
            {
                Nombre = "Bufanda",
                Precio = 12.00m,
                Stock = 20,
                CategoriaId = cat3.Id,
                EmprendimientoId = emp1.Id,
                Activo = true
            };

            var p3 = new Producto
            {
                Nombre = "Torta Chocolate",
                Precio = 18.00m,
                Stock = 5,
                CategoriaId = cat2.Id,
                EmprendimientoId = emp2.Id,
                Activo = true
            };

            await context.Productos.AddRangeAsync(p1, p2, p3);
            await context.SaveChangesAsync();

            // =====================
            // 🔥 GENERAR 500.000 PEDIDOS
            // =====================

            var usuarioIds = await context.Usuarios
                .Select(u => u.Id)
                .ToListAsync();

            var productos = await context.Productos
                .Select(p => new { p.Id, p.Precio })
                .ToListAsync();

            var random = new Random();

            int totalPedidos = 500_000;
            int batchSize = 5000;

            var batch = new List<Pedido>();

            context.ChangeTracker.AutoDetectChangesEnabled = false;

            for (int i = 0; i < totalPedidos; i++)
            {
                var userId = usuarioIds[random.Next(usuarioIds.Count)];
                var fecha = DateTime.UtcNow.AddDays(-random.Next(1, 365));

                var pedido = new Pedido
                {
                    UsuarioId = userId,
                    FechaPedido = fecha,
                    FechaCreacion = fecha,
                    Activo = true,
                    Detalles = new List<DetallePedido>()
                };

                int items = random.Next(1, 4);
                decimal total = 0;

                for (int j = 0; j < items; j++)
                {
                    var prod = productos[random.Next(productos.Count)];
                    int cantidad = random.Next(1, 3);

                    pedido.Detalles.Add(new DetallePedido
                    {
                        ProductoId = prod.Id,
                        Cantidad = cantidad,
                        PrecioUnitario = prod.Precio,
                        FechaCreacion = fecha,
                        Activo = true
                    });

                    total += cantidad * prod.Precio;
                }

                pedido.Total = total;
                batch.Add(pedido);

                if (batch.Count >= batchSize)
                {
                    context.Pedidos.AddRange(batch);
                    await context.SaveChangesAsync();
                    batch.Clear();

                    Console.WriteLine($"Insertados: {i + 1}");
                }
            }

            if (batch.Any())
            {
                context.Pedidos.AddRange(batch);
                await context.SaveChangesAsync();
            }

            context.ChangeTracker.AutoDetectChangesEnabled = true;
        }
    }
}