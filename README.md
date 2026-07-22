# Utlånssystem

A web app for keeping track of lab equipment loans, built with ASP.NET Core MVC.

## What it does

This app helps manage borrowing and returning of a wide range of items - electronics, books, chargers, tools, and more - between students. It keeps track of:

- What items exist, and what type they are
- Which ones are available to borrow
- Who currently has an item on loan
- A student can have up to 2 active loans at a time

## Login and access control

The app uses ASP.NET Core Identity for authentication. Registration and login are handled through Identity's built-in pages.

**Only an Admin can add, edit, or delete devices.** Everyone else (including users who aren't logged in) can still browse the device catalog, see available items, and register/return loans.

### Admin account

```
Email:    admin@utlanssystem.no
Password: Admin123!
```

To log in as admin:
1. Go to `/Identity/Account/Register` and register with the email above (only needed once)
2. Restart the app once, so the seeding logic can detect the new user and assign the Admin role

Once logged in as Admin, "Legg til enhet" / edit / delete links become available on the Devices pages.

## Built with

- ASP.NET Core MVC
- EF Core + SQLite (Code First, migrations)
- ASP.NET Core Identity for login/roles

## Running it locally

```
dotnet restore
dotnet ef database update
dotnet run
```

## Project structure

- `Models/` - Device, Student, Loan, and SeedData
- `Data/` - the EF Core database context (inherits from IdentityDbContext)
- `Controllers/` + `Views/` - one pair per resource: Devices, Students, Loans, Home
- `Areas/Identity/` - login, registration, and account management pages (Identity)

