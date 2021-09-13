#Lego Catalog
### Angular application with .Net Core API backend
 Refer to [Udemy Course: Build an app with ASPNet Core and Angualr from Scratch]("https://www.udemy.com/course/build-an-app-with-aspnet-core-and-angular-from-scratch/").  Section 19 describs how to publish application.

Utilizes [Brick Link]("https://bricklink.com")  lego database downloaded from  Lego part database found at [Rebrickable.com]("https://rebrickable.com/downloads")

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

4. Build application from command line
    >[npm install --save-dev @angular-devkit/build-angular]
    >ng build
5. From command line in the api directory ``(legocatalog.dotnet.api/legocatalog.api)`` run
    > dotnet watch run
6. Browse to
    > https://localhost:5001



Coppied 
