# InstaShare (ASPNet Core)

* This project requires postgreSQL to store the database.

## PostgreSQL Configuration

* Create a user with name `admin_instashare` and password `Insta123!`
* Create a database with name `InstaShareDB`and assign the previous user to it
        * Make sure the PostgreSQL server is running on host `localhost` and port `5432`.
###
    This settings can be changed in the appsetings.json file in the InstaShare.Web.Host project


## Update the database
Create the tables for the application to run.
- Navigate to project `InstaShare.EntityFrameworkCore` and run command `dotnet ef database update`
###
    Make sure you have installed the Entity Framework Tool for dotnet command:
    
    If you need to install it run:    dotnet tool install --global dotnet-ef

## Run the application
From the project `InstaShare.Web.Host` run the command `dotnet run`

###
    Once the application is running you can run or refresh the Angular application

## Default account
* The application creates a `Default` tenant that is gonna be used if no other tenant is configured.
* The application by default creates an Admin user for each tenant:
  * username: `admin`
  * password: `123qwe`

## 