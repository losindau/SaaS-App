# Electrical Maintenance SaaS Application

## Description

A SaaS application allows electrical maintenance companies to register and use it for inventory, truck, and staff management capabilities. It enables tracking of electrical items in warehouses and trucks, while also providing SMS notifications for item usage, restocking, and damage reports. <br>

## Demo

Here is a working demo: http://group5.ueh46.com/

You'll need these accounts to test the features of the application

<table>
    <thead>
        <th>Tenant</th>
        <th>Role</th>
        <th>Username</th>
        <th>Password</th>
    </thead>
    <tr>
        <td>1</td>
        <td>Manager</td>
        <td>managera@gmail.com</td>
        <td>managerA@1234?</td>
    </tr>
    <tr>
        <td>1</td>
        <td>Staff</td>
        <td>staffc@gmail.com</td>
        <td>staffC@1234?</td>
    </tr>
    <tr>
        <td>2</td>
        <td>Manager</td>
        <td>managerb@gmail.com</td>
        <td>managerB@1234?</td>
    </tr>
    <tr>
        <td>2</td>
        <td>Staff</td>
        <td>staffb@gmail.com</td>
        <td>staffB@1234?</td>
    </tr>
</table>

## Table of Contents

- [Key Features](#key-features)
- [Technologies Used](#technologies-used)
- [Examples of use](#examples-of-use)
- [Credits](#credits)

## Key Features

- Inventory Management: Track electrical items in the warehouse with categorization.
- Truck Management: Monitor and manage electrical items in trucks for efficient maintenance tasks.
- Staff Management: Assign trucks, track performance, and maintain staff records.

## Technologies Used

I built this project in order to gain experience with frameworks for web development. These are technologies, tools, and patterns that I have learned when building this project:
- ASP.NET Core Web API and ASP.NET Core MVC
- Entity Framework Core (Code-First approach) with SQL Server
- Authentication and Authorization with Identity Framework and JSON Web Token
- Basics of REST APIs, Dependency Injection, and Multi-tenant architecture with Row-Level Security using Global Query Filter
- Deployed on AWS EC2 and RDS
- Using Twilio to send SMS notifications

## Examples of use

Remember this is just an overview of the application, please access the demo link above and try it yourself for a better experiment

### Manager features

#### Employees and trucks management:

- Create new staff, assign a truck to staff

![image](https://github.com/losindau/InventoryManagementApp/assets/89368649/2cac71da-fcd3-430c-bcb6-13892125313b)

- Create a new truck

![image](https://github.com/losindau/InventoryManagementApp/assets/89368649/4a2a8c59-7983-474e-a3b0-0e7d32b4899a)

#### Stock items and equipment management:

- Create new stock items

![image](https://github.com/losindau/InventoryManagementApp/assets/89368649/44367efd-ce90-464a-b3ff-acb393aeed2f)

- Create new equipment

![image](https://github.com/losindau/InventoryManagementApp/assets/89368649/a7d7d390-491c-41b4-8a34-c195e1a0f079)

#### Usage, restock, and damage logs:

- Receive notifications whenever a log has been created by the staff

![image](https://github.com/losindau/InventoryManagementApp/assets/89368649/af6f8aa1-1927-4f87-8033-349bed36efb3)

### Staff features

#### Truck

- Track the number of items and equipment in the truck

![image](https://github.com/losindau/InventoryManagementApp/assets/89368649/b2d89034-b613-44d7-b2ac-a8f843b57332)

#### Usage, restock, and damage logs:

- Create a new log

![image](https://github.com/losindau/InventoryManagementApp/assets/89368649/87e97a8f-3022-43c3-a8dd-eee548d46a74)

## Credits

### Collaboration

<a href="https://github.com/losindau/InventoryManagementApp/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=losindau/InventoryManagementApp" />
</a>

### Third-party assets

- [Sneat – Free Responsive Bootstrap 5 HTML5 Admin Template](https://themewagon.com/themes/free-responsive-bootstrap-5-html5-admin-template-sneat/)

### Tutorials

- [ASP.NET Web API Tutorial 2022](https://www.youtube.com/playlist?list=PL82C6-O4XrHdiS10BLh23x71ve9mQCln0)
- [ASP.NET Core MVC 2022 .NET 6](https://www.youtube.com/playlist?list=PL82C6-O4XrHde_urqhKJHH-HTUfTK6siO)
- [WebAPI-NET5 #8 Secure API with Json Web Token (JWT)](https://www.youtube.com/watch?v=AQwS4-5YV4o)
- [Multi-tenancy - EF Core](https://learn.microsoft.com/en-us/ef/core/miscellaneous/multitenancy)
