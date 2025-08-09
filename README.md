# Order Management System (CQRS) – Learning Project

A modular Order Management API built with ASP.NET Core 8 and EF Core. The solution demonstrates Clean Architecture principles with a CQRS-style application layer, ASP.NET Identity integration, JWT authentication/authorization, auditable base entities, and comprehensive object mapping strategies.

**🚧 This is a learning project and is still under construction.**

## Solution structure

```text
OrdersManagement.sln
├─ OrdersManagement.Domain            # Domain model (entities, value objects, exceptions, repositories)
│  ├─ Entities/
│  │  ├─ Order Module/               # Order, OrderItem, OrderStatus
│  │  ├─ Product Module/             # Product, Category, ProductVariant
│  │  ├─ Inventory Module/           # Warehouse, InventoryItem
│  │  ├─ PaymentModule/              # PaymentInfo, PaymentTransaction, Refund
│  │  ├─ Shipping Module/            # Shipment, DeliveryAgent, ShippingInfo
│  │  ├─ Reporting Module/           # InventoryReport, SalesReport, OrderPerformance
│  │  └─ Shared/                     # Address (owned value object)
│  ├─ Repositories/                  # Repository abstractions
│  └─ Exceptions/
│
├─ OrdersManagement.Application       # Application layer (CQRS, validators, services)
│  ├─ Resturants/                    # Restaurant Commands, Queries, DTOs, Handlers
│  │  ├─ Commands/                   # CreateResturant, UpdateResturant, DeleteResturant
│  │  └─ Queries/                    # GetAllResturants, GetResturantById
│  ├─ Dishes/                        # Dish Commands, Queries, DTOs, Handlers
│  │  ├─ Commands/                   # CreateDish, DeleteAllForResturant
│  │  └─ Queries/                    # GetAllForResturant, GetDishByIdForResturant
│  ├─ Users/                         # User Commands, Queries, DTOs, Handlers
│  │  └─ Commands/                   # AssignUserRole, UnAssignUserRole, UpdateUserDetails
│  ├─ Helpers/                       # Cross-cutting concern abstractions
│  │  ├─ ILoggerHelper<T>           # Logging abstraction with adapter
│  │  ├─ IObjectMapper              # Mapping abstraction (AutoMapper/Mapster)
│  │  └─ IUserContext               # Current user context
│  └─ Extensions/                    # DI registration for the application layer
│
├─ OrdersManagement.Infrastructure    # EF Core, Identity, authz, repositories, migrations, seeders
│  ├─ Presistance/                    # ApplicationDbContext (int-key Identity, owned types)
│  ├─ Authorization/                  # Policies, requirements, claims principal factory
│  ├─ Repositories/                   # EF Core repository implementations
│  └─ Extensions/                     # DI registration for infrastructure
│
└─ OrdersManagement.Presentaion       # ASP.NET Core API (controllers, middleware, configuration)
   ├─ Controllers/                    # REST endpoints
   ├─ Middlewares/                    # Error handling, transaction
   └─ Extensions/                     # Web host builder extensions
```

## Architecture & patterns

- **CQRS-style application layer**: Commands/Queries with MediatR handlers; validation with FluentValidation.
- **Domain model first**: Entities, value objects, and domain exceptions live in `OrdersManagement.Domain`.
- **EF Core**: Code-first with repositories; `BaseEntity` provides `Id`, `CreatedAt`, `UpdatedAt`.
- **Owned value objects**: `Address` is owned by `ShippingInfo` and `Warehouse`.
- **ASP.NET Identity (int keys)**: `User : IdentityUser<int>`, `Role : IdentityRole<int>`.
- **JWT authentication & authorization**: Identity + policies/requirements in Infrastructure.
- **Adapters for cross-cutting concerns**:
  - Mapping: `IMapperHelper` with adapters for AutoMapper/Mapster
  - Logging: `ILoggerHelper<T>` wrapping `ILogger<T>`

## Key libraries & technologies

### Core Framework

- **ASP.NET Core 8** - Web API framework with built-in DI
- **Entity Framework Core 8** - ORM with SQL Server provider
- **.NET 8** - Target framework with nullable reference types enabled

### CQRS & Validation

- **MediatR 12.4.1** - Mediator pattern for CQRS commands/queries
- **FluentValidation.AspNetCore 11.3.0** - Request validation pipeline

### Authentication & Authorization

- **ASP.NET Identity** - User management with integer primary keys
- **JWT Bearer Authentication** - Stateless authentication tokens

### Object Mapping

- **AutoMapper 13.0.1** - Convention-based object mapping
- **Mapster 7.4.0** - High-performance object mapping alternative
- **Mapster.DependencyInjection 1.0.1** - DI integration for Mapster

### Logging & Monitoring

- **Serilog 4.2.0** - Structured logging framework
- **Serilog.AspNetCore 8.0.0** - ASP.NET Core integration
- **Serilog.Sinks.Seq 8.0.0** - Centralized log aggregation
- **Microsoft.Extensions.Logging.Abstractions 8.0.0** - Logging abstractions

## Notable implementation details

- `BaseEntity` audited fields are set in `ApplicationDbContext.SaveChanges*()`.
- `Address` is configured as an EF Core owned type.
- Int-based Identity requires updating generics across DbContext, role manager, and claims principal factory.

## Getting started

1) Requirements
- .NET 8 SDK
- SQL Server (localdb or full instance)

2) Configure connection string
- Set `ConnectionStrings:Default` in `OrdersManagement.Presentaion/appsettings.Development.json`.

3) Database migrations
- Add (if needed):
  - dotnet ef migrations add Init --project OrdersManagement.Infrastructure --startup-project OrdersManagement.Presentaion
- Update database:
  - dotnet ef database update --project OrdersManagement.Infrastructure --startup-project OrdersManagement.Presentaion

4) Run
- dotnet run --project OrdersManagement.Presentaion

## Status

- This is a learning project and is still under construction. APIs, modules, and internal abstractions may change.

## Roadmap (short)

- Complete repository layer for all modules
- End-to-end sample flows (orders, payments, shipping)
- Expand tests and add integration tests
- Swagger/OpenAPI polish and sample requests