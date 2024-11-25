# Online Bank System

## Overview

The **Online Bank System** is a web-based application developed using **ASP.NET Web Forms**. The system allows users to create bank accounts, log in, and manage funds within their accounts. Users can also top up accounts with **Flow** and **Sagicor** using their bank account balance, add money directly into their bank accounts, remove accounts, and verify if these external accounts exist.

This project uses **ASP.NET Identity** for secure user authentication and authorization, ensuring that only registered users can perform banking operations.

---

## Features

- **User Registration & Authentication**: Users can create accounts and log in securely using **ASP.NET Identity**.
- **Bank Account Management**: Users can view their bank balance, add money to their account, and check their transaction history.
- **Top Up Flow & Sagicor Accounts**: Users can top up external **Flow** and **Sagicor** accounts using funds from their bank account.
- **Remove Flow & Sagicor Accounts**: Users can remove their Flow and Sagicor accounts from their profile.
- **Account Existence Check**: Users can verify if an external Flow or Sagicor account exists.
- **Admin Panel**: Admins can manage users and monitor transactions.

---

## Technologies Used

- **Frontend**:
  - **Bootstrap 5.2.3**: For responsive design and styling.
  - **jQuery 3.7.0**: For DOM manipulation and AJAX functionality.

- **Backend**:
  - **ASP.NET Web Forms**: Used for building the web application and handling server-side logic.
  - **Entity Framework 6.5.1**: ORM for database interaction.
  - **ASP.NET Identity 2.2.4**: For secure user authentication and authorization.

- **Authentication**:
  - **ASP.NET Identity**: For user authentication, registration, password management, and security.

- **Database**:
  - **SQL Server (LocalDB)**: Stores user data, transaction history, and external account information.

---

## Installation

### Prerequisites

To run this project locally, you will need the following:

- **Visual Studio 2019** or later
- **.NET Framework 4.7.2** (installed with Visual Studio)
- **SQL Server LocalDB** or a configured SQL Server instance

### Steps to Set Up Locally

1. **Clone the repository**:

    ```bash
    git clone https://github.com/JayDbb/Online-Bank-System.git
    cd Online-Bank-System
    ```

2. **Open the project in Visual Studio 2019**:

    - Open `OnlineBankSystem.sln` file in Visual Studio 2019.

3. **Install required NuGet packages**:

    Visual Studio will automatically restore the required NuGet packages listed in `packages.config`. However, you can manually install them using the **NuGet Package Manager** if necessary:

    ```bash
    Install-Package Antlr -Version 3.5.0.2
    Install-Package bootstrap -Version 5.2.3
    Install-Package EntityFramework -Version 6.5.1
    Install-Package jQuery -Version 3.7.0
    Install-Package Microsoft.AspNet.Identity.Core -Version 2.2.4
    Install-Package Microsoft.AspNet.Identity.EntityFramework -Version 2.2.4
    ```

4. **Configure the database connection**:

    - Open `web.config`.
    - Ensure that the connection string points to your **SQL Server LocalDB** or a remote SQL Server instance.

    Example:

    ```xml
    <connectionStrings>
      <add name="OBS" connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OnlineBank;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True;" providerName="System.Data.SqlClient" />
    </connectionStrings>
    ```

    Adjust the `Data Source` if you're using a remote server.

5. **Set up the database**:

    - Ensure that the `OnlineBank` database is created in your SQL Server instance.
    - Use **Entity Framework migrations** or SQL scripts to set up the initial schema.

6. **Run the application**:

    - Press **F5** in Visual Studio to build and run the application locally.
    - Open a browser and navigate to `http://localhost:[PORT]` to view the app.

---

## Usage

### User Operations

1. **Register for an Account**: 
    - New users can register by providing their name, email, and password.
    - Upon successful registration, users can log in to the system.

2. **Login**:
    - Users can log in using their registered email and password.
    - Passwords are securely hashed and stored using **ASP.NET Identity**.

3. **Manage Bank Account**:
    - After logging in, users can view their account balance.
    - Users can add money to their bank account and view transaction history.

4. **Top Up Flow & Sagicor Accounts**:
    - Users can link their **Flow** or **Sagicor** account to the bank system.
    - They can use their bank balance to top up these external accounts.

5. **Remove Flow & Sagicor Accounts**:
    - Users can remove any linked **Flow** or **Sagicor** accounts from their profile.

6. **Check if an Account Exists**:
    - Users can check whether a linked **Flow** or **Sagicor** account exists.

---

