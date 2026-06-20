
# Goal

Goal is an **ASP.NET Core Web API** for an e-commerce store, built with **.NET 8** following Clean Architecture, split across 3 separate projects (Core / Data / API).

## 🏗️ Project Structure

```
Goal/
├── Goal.Core/      # Core layer: Models, DTOs, Interfaces, Specifications, Helpers
├── Goal.Data/      # Data layer: DbContext, Repositories, Services, Migrations
└── Goal/           # API layer: Controllers, Program.cs
```

### Goal.Core
- **Models**: Product, Category, Brand, Discount, Order, OrderItem, CartItem, Customer, Address, Stock, WishList...
- **DTOs**: one for each operation (Add/Update Product, Register, Login, Cart, Search...)
- **Interfaces**: Services + Repositories + UnitOfWork (Repository Pattern & Unit of Work)
- **Specifications**: implementing the Specification Pattern for filtering/searching
- **Helpers**: JWT, Cloudinary Settings, Email Service, Ordering

### Goal.Data
- `AppDbContext` (Entity Framework Core)
- Repositories + Unit of Work
- Services (Product, Cart, Auth, Category, Brand, Discount, Image, Order)
- Interceptors (Soft Delete, Discount Price)
- Migrations + Jobs (recurring discount updates)

### Goal (API)
- Controllers: Product, Cart, Brand, Category, Discount, User
- `Program.cs` for service registration and middleware configuration

## ⚙️ Tech Stack

| Technology | Purpose |
|---|---|
| ASP.NET Core 8 Web API | Building the API |
| Entity Framework Core 8 (SQL Server) | Data access |
| ASP.NET Core Identity | User and role management |
| JWT Bearer Authentication | Authentication |
| Google & Facebook Authentication | Social login |
| AutoMapper | Mapping Models to DTOs |
| Hangfire (SQL Server Storage) | Scheduled background jobs (auto-updating discounts every minute) |
| Stripe.net | Online payments |
| CloudinaryDotNet | Image upload and storage |
| Swagger / Swashbuckle | API documentation |
| dotenv.net | Environment variable management |

## ✨ Key Features

- Clean Architecture (Repository + Unit of Work + Specification Pattern)
- JWT authentication plus Google/Facebook social login
- Mandatory email confirmation on registration (`RequireConfirmedEmail`)
- Automatic discount updates via a recurring Hangfire job (every minute)
- Image upload via Cloudinary
- Payments via Stripe
- Soft delete via EF Core Interceptors

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server
- Cloudinary and Stripe accounts (for their respective features)

### Steps

1. Clone the repository:
   ```bash
   git clone https://github.com/abdelrahman437/Goal.git
   cd Goal
   ```

2. Configure `appsettings.json` (or environment variables via `.env`) with:
   - `ConnectionStrings:Goals` → SQL Server connection string
   - `JWT:Key` / `JWT:Issuer` / `JWT:Audience`
   - `CloudinarySettings` → Cloudinary account credentials
   - Stripe, Google/Facebook Auth, and email service credentials

3. Apply migrations to create the database:
   ```bash
   dotnet ef database update --project Goal.Data --startup-project Goal
   ```

4. Run the project:
   ```bash
   dotnet run --project Goal
   ```

5. Open the API documentation via Swagger at:
   ```
   https://localhost:<port>/swagger
   ```

## 📂 Main Endpoints (Controllers)

- `ProductController` — Product management
- `CategoryController` — Category management
- `BrandController` — Brand management
- `DiscountController` — Discount management
- `CartController` — Shopping cart
- `UserController` — Registration, login, authentication

## 👤 Author

[abdelrahman437](https://github.com/abdelrahman437)



