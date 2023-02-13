Steps to run API & SQL Server image in docker container

1. Set docker-compose as start up project then run it for the first time (this process will generate some images of API & SQL Server inside container)
2. Open appsettings.json, uncomment the connection string (No. 3) which performs migration data (With container)
3. Change start up project to TestAPI and then use command line : update-database, to perform EF data migration (package manager console) for SQL Server inside the container
4. After migration data is success, next uncomment the previous connection string (No. 3) in appsettings.json
5. Uncomment the connection string (No. 2) in appsettings.json to be used by API & SQL Server inside the container
6. Run docker-compose again, after it the API swagger will be opened via browser
7. Execute API SampleData/Generate to generate sample data for cars & car locations