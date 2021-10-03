#Lego Catalog
## Angular application with .Net Core API backend
 Refer to [Udemy Course: Build an app with ASPNet Core and Angualr from Scratch]("https://www.udemy.com/course/build-an-app-with-aspnet-core-and-angular-from-scratch/").  Section 19 describs how to publish application.

Utilizes [Brick Link]("https://bricklink.com")  lego database downloaded from  Lego part database found at [Rebrickable.com]("https://rebrickable.com/downloads")

### Build and run locally
To build and run locally, do the following steps.  Files are coppied to the api/wwwroot folder.
``
1. In console go to ``LegoCatalog.ng.client\LegoCatalog-ng-client``
2. Verify startup.cs ``Configure(IApplicationBuilder app, IWebHostEnvironment env)`` Has the following lines
    ```
    app.UseDefaultFiles();
    app.UseStaticFiles(); 
    ```
3. In angular.json set config setting:
    > "outputPath": "../../legocatalog.dotnet.api/legocatalog.api/wwwroot/assets"

4. In the ng.client directory, from command line, install angular-devkit (if not installed), and build application from command line.
    >[npm install --save-dev @angular-devkit/build-angular]
    >ng build
5. From command line in the api directory ``(legocatalog.dotnet.api/legocatalog.api)`` run
    > dotnet watch run
6. Browse to
    > https://localhost:5001

### Deploy
To deploy to Azure, do the following: 
1. Build Angular in prod mode. In ./LegoCatalog-ng-client type:
   > ng build --prod
2. Publish dotnet application.  In ./LegoCatalog.API type:
   > dotnet publish -c Release -o ../../publish  
3. Verify in publish/wwwroot/main.js: 
    >const environment = {
      production: true,
      serverUrl: 'https://xxxx.azurewebsites.net/api'
    };
3. In VS Code w/ Azure extensions installed, right click 'publish' folder and select 'deploy'
