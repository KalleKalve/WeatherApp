# TASK 2 Setup Guide

This quick setup guide will help you get the project up and running.

## Pre-requisites

- Visual Studio 2022
- .NET 6 SDK
- SQL Server
- Node.js (version 14.x or above)

## Setup Steps

### 1. Open the Solution

- Launch **Visual Studio 2022**.
- Navigate to `File` > `Open` > `Project/Solution`, and select the `.sln` file.

### 2. Prepare the Environment

- **Update Node.js**: Ensure you have the latest version of Node.js installed. Upgrade if your version is below 14.x.
- **Install Dependencies**:
  - Navigate to the Terminal within the `WeatherApp.MVC` directory.
  - Execute `npm install` to install necessary npm packages.
- **Build the Solution**:
  - Use `Build` > `Build Solution` in Visual Studio.
  - Install a self-signed certificate for HTTPS if required.

### 3. Set Up the Database

- Open the **Package Manager Console** through `Tools` > `NuGet Package Manager` > `Package Manager Console`.
- Select `WeatherApp.Database` as default project.
- Execute `update-database` to create and update the database schema.

### 4. Run the Application

- Start the application by pressing `Ctrl + F5` (without debugging) or `F5` (with debugging).

### 5. Populate Data

- Allow some time for the application to populate data. Initially, the UI graph will display only seeded values.
- The Api used for current temperature data collection, sadly updates info every 15 minutes.

## Additional Notes

- Verify the installation of the .NET 6 SDK and SQL Server on your machine.
- Check the `appsettings.json` file to ensure correct database connection strings are set and verify the city names configured in the application.
