# GestionGimnasio (Guia instalacion)

# 🏗️ Guía de Inicio del Proyecto — Full Stack Blazor + C#

Guía paso a paso para configurar una solución .NET con arquitectura en capas usando Blazor WebAssembly, Entity Framework Core y SQL Server.

---

## 📁 Estructura de la Solución

```
Proyecto/
├── Proyecto.Server/          # Host Blazor + controladores de API
├── Proyecto.Client/          # Blazor WebAssembly (frontend, generado automáticamente)
├── Proyecto.Shared/          # DTOs, modelos e interfaces compartidos
├── Proyecto.Servicios/       # Lógica de negocio y servicios de aplicación
├── Proyecto.BD/              # DbContext y configuración de EF Core
│   └── Datos/
│       └── Entity/           # Clases de entidad (tablas de la BD)
└── Proyecto.Repository/      # Repositorios — acceso a datos
```

---

## 📁 Referencias entre Proyectos

| Proyecto | Depende de |
|---|---|
| `Proyecto.BD` | `Proyecto.Shared` |
| `Proyecto.Repository` | `Proyecto.BD`, `Proyecto.Shared` |
| `Proyecto.Servicios` | `Proyecto.Shared` |
| `Proyecto.Client` | `Proyecto.Shared`, `Proyecto.Servicios` |
| `Proyecto.Server` | `Proyecto.BD`, `Proyecto.Repository`, `Proyecto.Shared`, `Proyecto.Client` |

> Para agregar una referencia: click derecho en el proyecto → **Agregar** → **Referencia de proyecto**

---

## 🚀 Paso a Paso

### 1. Crear el Proyecto Principal (Server + Client)

1. **Nuevo proyecto** → seleccionar **Blazor Web App**
2. Nombre: `Proyecto.Server`
3. En opciones de interactividad elegir **WebAssembly** — Visual Studio genera `Proyecto.Client` automáticamente

---

### 2. Crear `Proyecto.Shared`

1. Click derecho en la solución → **Agregar** → **Nuevo Proyecto**
2. Tipo: **Biblioteca de clases (.NET)**
3. Nombre: `Proyecto.Shared`
4. Agregar como referencia en `Proyecto.Client` y en `Proyecto.Server`

---

### 3. Crear `Proyecto.Servicios`

1. Agregar nueva **Biblioteca de clases** → Nombre: `Proyecto.Servicios`
2. Agregar como referencia en `Proyecto.Client`

> Aquí van los servicios HTTP que el Client consume para comunicarse con el Server (ej: `ProductoService`, `AuthService`).

---

### 4. Crear `Proyecto.BD`

1. Agregar nueva **Biblioteca de clases** → Nombre: `Proyecto.BD`
2. Agregar referencia a `Proyecto.Shared`
3. Crear estructura de carpetas:

```
Proyecto.BD/
└── Datos/
    ├── AppDbContext.cs     ← contexto principal
    └── Entity/             ← una clase por cada tabla
```

---

### 5. Crear `Proyecto.Repository`

1. Agregar nueva **Biblioteca de clases** → Nombre: `Proyecto.Repository`
2. Agregar referencias a `Proyecto.BD` y `Proyecto.Shared`

> Aquí van las clases que encapsulan las queries a la base de datos (ej: `ProductoRepository`).

---

### 6. Referencias finales en `Proyecto.Server`

Agregar referencias a:
- `Proyecto.BD`
- `Proyecto.Repository`
- `Proyecto.Shared`
- `Proyecto.Client`

---

## 📦 NuGet Packages

> Para instalar: click derecho en el proyecto → **Administrar paquetes NuGet** → pestaña **Examinar** → buscar e instalar

### `Proyecto.BD`

| Paquete | Descripción |
|---|---|
| `Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore` | Middleware de diagnóstico para errores de EF Core en desarrollo |
| `Microsoft.EntityFrameworkCore.Design` | Herramientas en tiempo de diseño (requerido para migraciones) |
| `Microsoft.EntityFrameworkCore.SqlServer` | Proveedor de SQL Server para EF Core |
| `Microsoft.EntityFrameworkCore.Tools` | Comandos de consola: `add-migration`, `update-database`, etc. |

### `Proyecto.Server`

> Los dos primeros suelen venir instalados por defecto al crear el proyecto Blazor.

| Paquete | Descripción |
|---|---|
| *(instalado por defecto)* | — |
| *(instalado por defecto)* | — |
| `Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore` | Mismo que en BD — muestra errores de EF en la página de excepción |
| `Microsoft.EntityFrameworkCore.Design` | Requerido para que el Server pueda ejecutar las migraciones como startup project |
| `Microsoft.EntityFrameworkCore.SqlServer` | Para registrar el contexto con `UseSqlServer()` en `Program.cs` |
| `Microsoft.EntityFrameworkCore.Tools` | Habilita los comandos de migración desde el Server como startup |

---

## 🗃️ Configurar `AppDbContext`

Dentro de `Proyecto.BD/Datos/`, crear `AppDbContext.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using Proyecto.BD.Datos.Entity;

public class AppDbContext : DbContext
{
    public DbSet<NombreEntidad> NombreEntidades { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}
```

> Agregar un `DbSet<T>` por cada entidad que tenga su tabla en la base de datos.

---

## 🧱 Estructura de una Entidad

Dentro de `Proyecto.BD/Datos/Entity/`, crear una clase por entidad:

```csharp
namespace Proyecto.BD.Datos.Entity;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public bool Activo { get; set; } = true;
}
```

Luego registrarla en `AppDbContext`:

```csharp
public DbSet<Producto> Productos { get; set; }
```

---

## ⚙️ Configuración

### `appsettings.json` (en `Proyecto.Server`)

```json
{
  "ConnectionStrings": {
    "ConnSqlServer": "url base de datos"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

**Ejemplo de cadena de conexión para SQL Server local:**
```
Server=localhost;Database=MiBaseDeDatos;Trusted_Connection=True;TrustServerCertificate=True;
```

**Ejemplo para SQL Server con usuario y contraseña:**
```
Server=localhost;Database=MiBaseDeDatos;User Id=sa;Password=tuPassword;TrustServerCertificate=True;
```

> ⚠️ No subir credenciales reales al repositorio. Usar `appsettings.Development.json` (ya ignorado por `.gitignore` de .NET) o variables de entorno para datos sensibles.

---

### `Proyecto.Server/Program.cs`

Agregar luego de `var builder = WebApplication.CreateBuilder(args);`:

```csharp
string connectionString = builder.Configuration.GetConnectionString("ConnSqlServer")
    ?? throw new InvalidOperationException("No existe la conexión con la base de datos.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
```

---

## 🔄 Migraciones

Ejecutar desde la **Package Manager Console** con la siguiente configuración:

- **Proyecto por defecto** (dropdown): `Proyecto.BD`
- **Proyecto de inicio** (Startup Project): `Proyecto.Server`

```powershell
# Crear la migración inicial
add-migration Inicio

# Aplicar los cambios a la base de datos
Update-Database
```

> Si la base de datos no existe, `Update-Database` la crea automáticamente antes de aplicar la migración.

**Comandos útiles adicionales:**

```powershell
# Listar todas las migraciones aplicadas
Get-Migration

# Revertir a una migración anterior
Update-Database NombreMigracionAnterior

# Eliminar la última migración (si aún no fue aplicada)
Remove-Migration
```

---

## ✅ Checklist de Configuración

- [ ] `Proyecto.Server` creado (Blazor Web App — WebAssembly)
- [ ] `Proyecto.Client` generado automáticamente
- [ ] `Proyecto.Shared` creado y referenciado en `Client` y `Server`
- [ ] `Proyecto.Servicios` creado y referenciado en `Client`
- [ ] `Proyecto.BD` creado con carpeta `Datos/Entity/`
- [ ] `Proyecto.BD` referencia a `Shared`
- [ ] `Proyecto.Repository` creado y referencia a `BD` y `Shared`
- [ ] `Proyecto.Server` referencia a `Client`, `Shared`, `BD` y `Repository`
- [ ] NuGet packages instalados en `BD` (4 paquetes)
- [ ] NuGet packages instalados en `Server` (4 paquetes adicionales)
- [ ] `AppDbContext` creado en `Proyecto.BD/Datos/`
- [ ] Entidades creadas en `Proyecto.BD/Datos/Entity/` y registradas en el contexto
- [ ] Cadena de conexión configurada en `appsettings.json`
- [ ] `AppDbContext` registrado en `Program.cs`
- [ ] Migración inicial creada y base de datos actualizada
