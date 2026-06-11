# EmprendeMarket

EmprendeMarket es una plataforma web desarrollada en **ASP.NET Core MVC** diseñada para potenciar el comercio local, permitiendo a emprendedores gestionar sus negocios y a clientes descubrir y adquirir productos artesanales y locales.

## 🚀 Arquitectura del Proyecto

El proyecto sigue el patrón de diseño **Model-View-Controller (MVC)**, asegurando una separación clara de responsabilidades:

- **Models**: Definición de entidades de negocio y lógica de datos.
- **Views**: Interfaces de usuario dinámicas desarrolladas con Razor Pages.
- **Controllers**: Lógica de control que gestiona las peticiones y coordina la respuesta.
- **Data**: Contexto de base de datos (`AppDbContext`) y configuraciones de Entity Framework Core.
- **Services**: Capa de servicios para la lógica de negocio y autenticación.
- **ViewModels**: Modelos de datos optimizados para la comunicación entre controladores y vistas.

## 🛠️ Tecnologías Utilizadas

- **Framework**: .NET 8.0 (ASP.NET Core MVC)
- **Base de Datos**: PostgreSQL
- **ORM**: Entity Framework Core
- **Frontend**: Bootstrap 5, Razor, HTML5, CSS3, JavaScript
- **Autenticación**: ASP.NET Core Identity (Hashing de contraseñas personalizado)

## 📋 Requisitos Previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- Herramientas de EF Core (`dotnet ef`)

## ⚙️ Configuración e Instalación

1. **Clonar el repositorio**:
   ```bash
   git clone <url-del-repositorio>
   ```

2. **Configurar la base de datos**:
   Actualiza la cadena de conexión en `appsettings.json` con tus credenciales de PostgreSQL:
   ```json
   "DefaultConnection": "Host=localhost;Database=EmprendeMarketDB;Username=tu_usuario;Password=tu_password"
   ```

3. **Aplicar Migraciones**:
   Ejecuta el siguiente comando para crear las tablas en tu base de datos:
   ```bash
   dotnet ef database update
   ```

4. **Ejecutar el proyecto**:
   ```bash
   dotnet run
   ```
   El sistema sembrará automáticamente datos de prueba en el primer inicio.

## 🔒 Seguridad

El proyecto incluye medidas de seguridad como:
- Hasheo de contraseñas mediante `PasswordHasher`.
- Protección contra ataques CSRF mediante `ValidateAntiForgeryToken`.
- Manejo de sesiones seguras.
- Exclusión de archivos sensibles en el control de versiones (`.gitignore`).

---
*Desarrollado para el Proyecto de Integración Web - 6to Semestre.*
