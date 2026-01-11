# Inventory Management - MVC (.NET 8) - Single Project

This is a complete ASP.NET Core MVC project (single project) for an Inventory Management system.
It includes:
- Registration & Login (cookie-based auth)
- Product management (CRUD)
- Warehouse management (create/list)
- Stock adjustments and inventory history
- EF Core (SQL Server) DbContext

## How to run (Windows, Visual Studio)

1. Extract this zip to a folder, open `InventoryManagement.sln` in Visual Studio 2022/2023.
2. Open **Package Manager Console** (Tools → NuGet Package Manager → Package Manager Console).
3. Run the commands to create migrations and database:
   ```powershell
   dotnet restore
   Add-Migration Initial
   Update-Database
   ```
   (If `Add-Migration` isn't available, run `dotnet tool install --global dotnet-ef` and `dotnet ef migrations add Initial`)
4. Press **F5** to run. The application will open in your browser.
5. Register a new user (Account → Register), then Login.
6. Go to Products → Add Product, Warehouses → Add Warehouse, Inventory → Adjust stock.

## Notes
- Connection string is in `appsettings.json`. Default uses SQL Express:
  `Server=.\\SQLEXPRESS;Database=InventoryDb;Trusted_Connection=True;`
  Change if needed.
- Passwords are hashed with SHA256 (simple hasher) for demo only. For production use ASP.NET Core Identity.
- If you want, I can run through the exact commands step-by-step.

Generated on: 2025-08-29T17:00:40.839165Z
