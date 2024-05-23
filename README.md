# Random String Generator

## Prerequisites

1. [Install .NET Core SDK](https://dotnet.microsoft.com/download).
2. [Install MySQL Server](https://dev.mysql.com/downloads/mysql/).

## Database Setup

1. Open MySQL Command Line Client or any MySQL client tool of your choice (like MySQL Workbench).
2. Run the following command to create a new database:

    ```sql
    CREATE DATABASE random_string_generator;
    ```

3. Import the database schema:
    - Open a terminal or command prompt.
    - Navigate to the directory where `dump-RandomStringDB-202405231807.sql` is located.
    - Run the following command, replacing `[username]` and `[password]` with your MySQL credentials:

    ```sh
    mysql -u [username] -p random_string_generator < database_export.sql
    ```

## Application Setup

1. Open the solution in Visual Studio.
2. Restore NuGet packages.
3. Update the connection string in `appsettings.json` or wherever your connection string is defined in your WPF application:

    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=random_string_generator;User ID=[your_username];Password=[your_password];"
    }
    ```

4. Build and run the application.


