# TMS (Tenants Management System) passion project

TMS is a management system is a C# CMS that is used by landlords to manage their tenants and their properties.

## Features
- Feature to add a new , edit or delete a tenant.
- Feature to add , edit or delete a property

## Database

TMS has 4 tables.

- Tenants table captures information about the tenants.
- landlords table captures all payments eg rental , hydro , parking.
- Lease table is a bridging table between a tenant and a landlord.
- properties table captures properties for  the landlord.

## Concepts Used:

- ASP.NET MVC architecture pattern
- Entity Framework Code-First Migrations to represent the database
- LINQ to perform CRUD operations

## How to Run This Project?

- Clone the repository in Visual Studio
- Open the project folder on your computer (e.g. File Explore for Windows Users)
- Create an <App_Data> folder in the main project folder
- Go back to Visual Studio and open Package Manager Console and run the query to build the database on your local server:
- update-database
- The project should set up

## Future features & Improvements
- User Authentication
- Adding tenants photos
- Capturing more data when generating a lease
- Ability to determine how many rooms are in the building and how many are empty
- Better UI Design

