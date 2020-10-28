Steps Taken
===========

Prior to class:

1. Create starting mvc project, solution etc. using the dotnet command line
2. Modify Home/Index.cshtml
	a. Add image to wwwroot/images
	b. Create reporting form
3. Add a new controller called HornetsController
	a. Add Thanks() and Sighting() action methods
	b. Set up redirection so we can implement GET-POST-Redirect to GET pattern
4. Add empty sql script files to a Data folder

TODO

1. Define our database model in the up script, write seed and down scripts
2. Create the database and run up and seed scripts
3. Get the connection string and put it in appsettings.json
4. Scaffold Model classes and DbContext class with dotnet ef dbcontext command
5. Add the db context class to our services in startup.cs
6. Use model and dbcontext classes in the controller and views to build out functionality

7. Add map (after class, you'll need a key for Bing Maps)