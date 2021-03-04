# AnyStats-5204_PassionProject-n01442097
## AnyStats
The app allows users to create statistics both private and public and the information would be represented in charts.

### FEATURES
1. Users can create, update, delete and view statistics 
2. Users can view stats in 5 different beautiful charts
3. User Registration - Login
4. Any user can view public stats and logged in users can view the 
Stats that they created for private use.
5. Pagination 

### Technologies
.net, MVC, Entity Framework, JS and ChartJS


### How to run the project
1. Clone the repository
2. Verify the database name in Web.config file
the connectionStrings should look similar this, if you encounter any errors change the name & AttachDbFilename
```
<connectionStrings>
	  <add name="AnyStats" connectionString="Data Source=(localdb)\MSSQLLocalDB; Integrated Security=True; MultipleActiveResultSets=True; AttachDbFilename=|DataDirectory|AnyStats03- 03-2021.mdf"providerName="System.Data.SqlClient" />
</connectionStrings> 
```
3. Clean, Rebuild project to avoid roslyn error.
4. Verify the App_Data folder is created in the file explorer where the solution exists.
5. In Tools > Nuget Package Manager > Package Manager Console enter following commands
  
  enable-migrations
  
  add-migration {migration_name}
  
  update-database

### References
Varsity:- https://github.com/christinebittle/varsity_mvp

