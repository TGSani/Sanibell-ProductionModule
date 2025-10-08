# Production & Planning Web App
### Description

The **Production & Planning Web App** is designed to streamline the creation, management, and processing of work orders.  
Where previously these processes were manual, this application provides an efficient workflow for planning and production activities from a central dashboard.

## Features
- QR-code scanning
- Role-based dashboard

## Future Features
- Automatic production order generation from ERP
- Automated staff & warehouse updates
- Expanded admin controls

## Tech Stack
- [ASP.NET Core Razor Pages](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-9.0)
- [SQL Everywhere](https://www.sap.com/products/data-cloud/sql-anywhere.html)
- [Dapper](https://www.learndapper.com/)

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)  
- Visual Studio 2022 or VS Code  
- SQL Everywhere installed and configured

### Installation
1. Clone the repository:
```
git clone https://github.com/TGSani/Sanibell-ProductionModule.git
cd Sanibell-ProductionModule
```
2. Configure the database connection in `appsettings.json`
3. Build and run the project  
```
dotnet run
```
4. Open `http://localhost:{port}` in your browser to see the application.

## Usage
1. Navigate to the application’s **home page**.  
2. Select your name from the user list.  
3. You will be redirected to the **login page** .  
4. Scan your **QR code** using the external QR scanner connected to your workstation.  
5. Upon successful verification, you will be redirected to the **dashboard**.
6. Click a title to navigate to the corresponding page.

> :warning: These pages are currently under development.