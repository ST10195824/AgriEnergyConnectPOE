# AgriEnergyConnectPOE


## Account Credentials:

Farmer Account:
```
email: mfrench4444@gmail.com
password: Mike123@
```

Employee Account:
```
email: admin@admin.com
password: Admin123@
```


AgriEnergyConnectPOE is a web application designed to facilitate the management of agricultural products and connect farmers with potential buyers. This README file provides step-by-step instructions for setting up the development environment, building, and running the prototype, as well as an overview of the system's functionalities and user roles.

## Table of Contents


1. [Prerequisites](#prerequisites)
2. [Setting Up the Development Environment from Github](#setting-up-the-development-environment-from-github)
2. [Building and Running the Prototype](#building-and-running-the-project-from-the-project-folder-zip)
2. [System Functionalities](#system-functionalities)
3. [User Roles](#user-roles)

## Prerequisites

Before setting up the development environment, ensure you have the following software installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)


## Setting Up the Development Environment from Github

Follow these steps to set up your development environment:

1. **Clone the Repository**
   ```bash
   git clone https://github.com/ST10195824/AgriEnergyConnectPOE.git
   cd AgriEnergyConnectPOE
   ```

2. **Apply Migrations**
   Open the terminal in your project directory and run:
   ```bash
   dotnet ef database update
   ```

3. **Restore NuGet Packages**
   In the terminal, run:
   ```bash
   dotnet restore
   ```

## Building and Running the Project (from the Project folder ZIP)
Since you have the project with the DB and dependancies already set up you can  simply build and run the Project:

1. **Build the Project**
   In the terminal, run:
   ```bash
   dotnet build
   ```

2. **Run the Application**
   In the terminal, run:
   ```bash
   dotnet run
   ```
   This will start the web server and you can access the application at in the browser window that will be opened automatically
## System Functionalities

AgriEnergyConnectPOE provides the following functionalities:

1. **Farmer's Personal Page**
   - **View Products**: Farmers can view their own products.
   - **Add Products**: Farmers can add new products to their inventory.
   - **View Individual Product**: Farmers can view details of their individual products.

2. **Employee Dashboard**
   - **View Dashboard**: Employees can view a dashboard displaying all farmers and their products.
   - **Add Category**: Employees can add new product categories.
   - **Add Farmer**: Employees can add new farmers to the system.
   - **View Farmer Details**: Employees can view details of individual farmers and their products.

3. **Marketplace**
   - **View Products**: Users can view products available in the marketplace.
   - **View Individual Product**: Users can view details of individual products in the marketplace.

## User Roles

AgriEnergyConnectPOE defines the following user roles with respective permissions:

1. **Farmer**
   - Can view and manage their own products.
   - Can add new products to the marketplace.

2. **Employee**
   - Can access the employee dashboard.
   - Can view and manage all farmers and their products.
   - Can add new categories and farmers.

3. **General User**
   - Can view products available in the marketplace.
   - Can view details of individual products.

## Conclusion

AgriEnergyConnectPOE is a versatile platform designed to streamline the management of agricultural products and enhance connectivity between farmers, employees, and buyers. By following the instructions provided in this README, you should be able to set up the development environment, build, and run the prototype successfully. For any further assistance, please refer to the project's documentation or contact the development team.