![dotnet5 build and test](https://github.com/suarezafelipe/Hahn.ApplicationProcess.Application/workflows/dotnet5andNodeCI/badge.svg?branch=master)

# Hahn.ApplicationProcess.Application

## To build and run the Backend

### From Visual Studio
Open the `Hahn.ApplicationProcess.Application.sln` file

Make sure the **startup** project is set to: `Hahn.ApplicationProcess.December2020.Web`

Click on `IIS Express`


### From the command line:
`dotnet restore`

`dotnet build --no-restore`

`dotnet run`

### Unit tests and coverage

Most of the domain layer is unit tested. The Controllers are unit tested as well. Current code coverage is 61%

## To build and run the Frontend

`npm i`

`au run`

### Ports

The backend expects that the frontend runs on the default port for Aurelia applications: **`http://localhost:8080`**. If running on another port, you need to update it in the Startup.cs class under the `app.useCors` section.

The frontend expects that the API runs on the default port for dotnet APIs: **`https://localhost:5001`**. If running on another port, you need to update it in the `/config/environment.json` file


## Comments on the delivery

- All requirements were fulfilled according to the specs.
- CI was added to verify that both the front and back end projects build correctly. Additionally unit tests for the backend are also performed.

#### Backend

- All HTTP requests and responses as well as all the exceptions thrown by the back end are automatically logged using Serilog rolling files. This was done with a middleware to avoid cluttering code in the controllers.
- The `Hahn.ApplicationProcess.December2020.Domain` project was taken as the *"Business"* or *"Services"* layer. The idea is to have the entities, models and the core logic of the application under this project. Inversion of control pattern was used to avoid having this layer depend on the `Hahn.ApplicationProcess.December2020.Data` project, so in the end the Domain layer has no dependencies. 
- A middleware was used to recognize the UI locale and give the validation answers on that language. 
- Extension methods were called in the Startup.cs class to leave this important file as readable as possible.


#### Frontend

- A panel to choose/change the language was added to test that internationalisation works as expected.
- `EventAggregator` was used in order to successfully display the frontend validation messages according to the user's language.
- Backend validation errors are shown in the correct locale.
- Bootstrap v5.0.0-beta1 was preferred over v4.x mainly to avoid having to install or workaround the jquery dependency, which is not needed at all.
- `Axios` was preferred over `aurelia-http-fetch` or `aurelia-http-client` because the code is way cleaner - especially when handling errors.
- The UI is responsive.
