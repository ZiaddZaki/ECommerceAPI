# ECommerce REST API

A fully functional E-Commerce backend REST API built with ASP.NET Core, following **N-Tier Architecture** and clean code principles.

---

## Tech Stack

- **ASP.NET Core** (.NET 10)
- **Entity Framework Core** — ORM
- **SQL Server** — Database
- **Microsoft Identity** — User management
- **JWT Authentication** — Token-based auth
- **Policy-Based Authorization** — Role-based access control
- **Fluent Validation** — Input validation
- **N-Tier Architecture** — API / BLL / DAL / Common
- **Repository Pattern + Unit of Work**
- **Result Pattern** — Unified API responses

---

##  Project Structure

```
ECommerceAPI/
??? ECommerceAPI.API          # Controllers, Program.cs, Settings
??? ECommerceAPI.BLL          # Business Logic, Managers, DTOs, Validators
??? ECommerceAPI.DAL          # Models, DbContext, Repositories, Migrations
??? ECommerceAPI.Common       # Result Pattern, Pagination, Shared classes
```

---

## Setup & Run

### Prerequisites
- .NET 10 SDK
- SQL Server

### Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-username/ECommerceAPI.git
   cd ECommerceAPI
   ```

2. **Configure the database connection**

   In `ECommerceAPI.API/appsettings.json`, update the connection string:
   ```json
   "ConnectionStrings": {
     "EcommerceApi": "Server=YOUR_SERVER;Database=EcommerceApi;Trusted_Connection=true;TrustServerCertificate=true"
   }
   ```

3. **Configure JWT Settings**
   ```json
   "JwtSettings": {
     "Issuer": "EcommerceApi",
     "Audience": "EcommerceApi",
     "DurationInMinutes": 600,
     "SecretKey": "YOUR_BASE64_SECRET_KEY"
   }
   ```

4. **Apply Migrations**
   ```bash
   dotnet ef database update --project ECommerceAPI.DAL --startup-project ECommerceAPI.API
   ```

5. **Run the project**
   ```bash
   dotnet run --project ECommerceAPI.API
   ```

6. **Create an Admin role** via `POST /api/Role` then assign it to your user manually or via the Role endpoint.

---

## API Endpoints

### Auth
| Method | Endpoint           | Access | Description |
|--------|----------|-------- |--------|
| POST | `/api/auth/register` | Public | Register new user |
| POST | `/api/auth/login`    | Public | Login and get JWT token |

### Products
| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| GET | `/api/products` | Public | Get all products |
| GET | `/api/products/{id}` | Public | Get product by ID |
| GET | `/api/products/pagination?pageNumber=&pageSize=&categoryId=&name=` | Public | Get products with filter & pagination |
| POST | `/api/products` | Admin | Create product |
| PUT | `/api/products/{id}` | Admin | Update product |
| DELETE | `/api/products/{id}` | Admin | Delete product |
| POST | `/api/products/{id}/image` | Admin | Set product image |

### Categories
| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| GET | `/api/categories` | Admin | Get all categories |
| GET | `/api/categories/{id}` | Admin | Get category by ID |
| POST | `/api/categories` | Admin | Create category |
| PUT | `/api/categories/{id}` | Admin | Update category |
| DELETE | `/api/categories/{id}` | Admin | Delete category |
| POST | `/api/categories/{id}/image` | Admin | Set category image |

### Cart
| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| GET | `/api/cart` | User | Get user cart |
| POST | `/api/cart` | User | Add product to cart |
| DELETE | `/api/cart/{productId}` | User | Remove product from cart |

### Orders
| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| POST | `/api/order` | User | Place order from cart |
| GET | `/api/order` | User | Get order history |
| GET | `/api/order/{id}` | User | Get order details |

### Images
| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| POST | `/api/images/upload` | Admin | Upload image and get URL |

---

## Authentication

All protected endpoints require a **Bearer Token** in the Authorization header:

```
Authorization: Bearer YOUR_JWT_TOKEN
```

**Roles:**
- `Admin` — Full access to Products, Categories, Images
- `User` — Access to Cart and Orders

---

## Postman Testing Video

> **[Watch the full API testing walkthrough here](https://drive.google.com/file/d/1IDZiWwbnbrBy9uU7nTkSzuX49UOQ-5TU/view?usp=drivesdk)**
---

## Author

**Ziad** — ITI
