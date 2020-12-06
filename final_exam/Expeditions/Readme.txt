Packages that have been installed:

dotnet add package Bricelam.EntityFrameworkCore.Pluralizer
dotnet add package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation --version 3.1.10
dotnet add package Microsoft.EntityFrameworkCore --version 3.1.10
dotnet add package Microsoft.EntityFrameworkCore.Design --version 3.1.10
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 3.1.10
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 3.1.10
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 3.1.4

To restore libraries, etc. run 

dotnet restore

Other changes made to this project from the default template:

- add Data folder with SQL scripts
- add .jpg image to images folder in wwwroot
- modify Views/Home/Index.cshtml to add image and some text
- remove Views/Home/Privacy.cshtml
- modify Views/Shared/_Layout.cshtml to remove links to Privacy
- modify Controllers/HomeController.cs to remove Privacy action method
- modify wwwroot/js/site.js to add some starting JavaScript code
- add this Readme.txt file