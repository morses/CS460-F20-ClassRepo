# Using Identity for Authorization and User Accounts

1. Set up project with Individual User Accounts &#9989;
2. Walk-through files and folders of the scaffolded project &#9989;
3. A look at the database Identity uses; and migrations &#9989;
4. Our architecture when we use authentication &#9989;
5. Use it: getting basic info for the logged in user &#9989;
6. A tale of 2 databases &#9989;
```
# update auth database using the current migration (when moving to a new database)
dotnet ef database update --context ApplicationDbContext
```
7. Use it: create a user upon registration with first and last names &#9989;
```
# Reverse engineer our models using the correct connection (don’t reverse engineer Identity tables!)
dotnet ef dbcontext scaffold Name=FujiConnection Microsoft.EntityFrameworkCore.SqlServer --context FujiDbContext --context-dir Data --output-dir Models --verbose --force

# Scaffold controller with views using the correct context
dotnet aspnet-codegenerator controller -name FujiUsersController -m FujiUser -dc FujiDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --force
```
8. Use it: per-user operations – let them eat apples &#9989;
9. Some simple configuration changes &#9989;
10. Authorization policies &#9989;
    - per method
    - per class
    - per application
    - "allow all except" or "deny all except" strategies
11. Deploy it to Azure &#9989;
```
# Generate sql script to create or update Identity db on Azure
dotnet ef migrations script --context ApplicationDbContext --output Data\UpdateIdentityAzure.sql
```
12. How to seed users &#9989;
13. How to seed a super-admin role and account
14. How Identity uses cookies to make this work