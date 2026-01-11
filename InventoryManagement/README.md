# Inventory Management - MVC (.NET 8) - Single Project

This is a complete ASP.NET Core MVC project (single project) for an Inventory Management system.
It includes:
- Registration & Login (cookie-based auth)
- Product management (CRUD)
- Warehouse management (create/list)
- Stock adjustments and inventory history
- EF Core (SQL Server) DbContext

## How to run (Windows, Visual Studio)

1. Open `InventoryManagement.sln` in Visual Studio 2022/2023.
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


