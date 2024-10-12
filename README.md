dotnet new webapi -n learndotnet
dotnet build
dotnet run
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 2.2.2

mysql
STEP 1 - C:\Program Files\MySQL\mysql-5.7.44-winx64\bin
STEP 2 - open cmd as admin and start server by typing > mysqld
STEP 3 - open a new cmd in same path as admin and type > mysql -u root -p


dotnet tool install --global dotnet-ef --version 2.2.6
dotnet tool list --global


dotnet ef migrations add InitialCreate

# What Happens When You Run This Command
    Migration File Creation: When you run the command, Entity Framework Core scans your DbContext and the associated model classes (like your GameModel) to determine what changes need to be made to the database schema.

    Code Generation: It generates a new C# file in the Migrations folder of your project. This file contains the instructions (C# code) for how to create or modify the database schema to match your model classes.

    Tracking Changes: Entity Framework keeps track of the applied migrations in the database. When you later run dotnet ef database update, it will apply the migrations in the order they were created.

## start the MySQL server (mysqld)
dotnet ef database update

mysql> SHOW DATABASES;
+--------------------+
| Database           |
+--------------------+
| information_schema |
| gamedb             |
| mysql              |
| performance_schema |
| sys                |
+--------------------+
5 rows in set (0.00 sec)

mysql> SHOW TABLES;
+-----------------------+
| Tables_in_gamedb      |
+-----------------------+
| __efmigrationshistory |
| gametable             |
+-----------------------+
2 rows in set (0.00 sec)
