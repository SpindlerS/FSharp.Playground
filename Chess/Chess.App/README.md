# Chess Web Application

## Pre-requisites

* [.Net core sdk](https://www.microsoft.com/net/core#windows)
* [Visual studio 2017](https://www.visualstudio.com/)
* [Nodejs](https://nodejs.org/en/)

**Make sure you have Node version >= latest LTS and NPM >= latest LTS**

## Installation

1. `dotnet restore` in project directory *Chess.App*
2. `npm install protractor rimraf http-server @angular/cli -g`
3. Change to directory *ClientApp* and run `npm install`
4. `npm start` to run client app
5. F5 from [Visual Studio IDE](https://www.visualstudio.com/)

### Database configuration (Sqlite or SqlServer)

This project supports both sql server and sql lite databases

* Run with Sqlite:
    * Project is configured to run with sqlite **by default** and there is an 'Initial' migration already added (see Migrations folder)
    * After changing you models, you can add additional migrations [see docs](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet)

* Run with SqlServer:
    * npm run clean
    * Delete `Migrations` folder
    * Flip the switch in appsettings.json called `useSqLite` to `false`, this should point to use local sql server setup as default instance. (See appsettings.json file for connection string)
    * Run `dotnet ef migrations add "InitialMigrationName"`


## Other commands

### Scaffold Angular components using Angular CLI

Scaffold  | Usage
---       | ---
Component | `ng g component my-new-component`
Directive | `ng g directive my-new-directive`
Pipe      | `ng g pipe my-new-pipe`
Service   | `ng g service my-new-service`
Class     | `ng g class my-new-class`
Guard     | `ng g guard my-new-guard`
Interface | `ng g interface my-new-interface`
Enum      | `ng g enum my-new-enum`
Module    | `ng g module my-module`

### run Angular tests
```bash
cd ClientApp

npm test
```
### Compodoc Angular documentation
* Steps to generate:
  * npm i compodoc -g
  * npm run compodoc
  * cd documentation
  * http-server

### run end-to-end tests
```bash
# make sure you have your server running in another terminal (i.e run "dotnet run" command)
npm run e2e
```

### run Protractor's elementExplorer (for end-to-end)
```bash
npm run webdriver:start
# in another terminal
npm run e2e:live
```

## Compatability
 * This project is supported in everygreen browsers and IE10+
 * IE8 & IE9 aren't supported since Bootstrap 4 is supported in IE10+ [explained here](http://v4-alpha.getbootstrap.com/getting-started/browsers-devices/).

## Azure Deploy
* You can set an environment variable for azure app deployment password
Set-Item -path env:AzureAppPass -value passwordhere
```
From powershell:
./deploy-azure.ps1
```

## Deploy to Azure as App Service
Set-Item -path env:AzureAppPass -value passwordhere
```
From powershell:
./deploy-azure.ps1
```

## Template Features

* [ASP.NET Core](http://www.dot.net/)
* [Entity Framework Core](https://docs.efproject.net/en/latest/)
    * Both Sql Server and Sql lite databases are supported (Check installation instrcutions for more details)
* [Angular](https://angular.io/)
* [Angular CLI](https://cli.angular.io/)
* Secure - with CSP and custom security headers
* [Bootstrap 4](http://v4-alpha.getbootstrap.com/)
* [SignalR](https://github.com/aspnet/SignalR/) (Chat example)
* [SASS](http://sass-lang.com/) support
* [Best practices](https://angular.io/docs/ts/latest/guide/style-guide.html) in file and application organization for Angular.
* [PWA support](https://developers.google.com/web/progressive-web-apps/)
* [SSR (Server side rendering)](https://angular.io/guide/universal) - Coming soon...
* Testing Angular code with [Jasmine](http://jasmine.github.io/) and [Karma](https://karma-runner.github.io/0.13/index.html).
* E2E testing with [Protractor](http://www.protractortest.org).
* [Compodoc](https://compodoc.github.io/compodoc/) for Angular documentation
* Login and Registration functionality using [Asp.Net Identity & JWT](https://docs.asp.net/en/latest/security/authentication/identity.html)
* Token based authentication using [Openiddict](https://github.com/openiddict/openiddict-core)
     * Get public key access using: http://127.0.0.1:5000/.well-known/jwks
* Extensible User/Role identity implementation
* Social logins support with token based authentication, Follow [this](https://github.com/asadsahi/AspNetCoreSpa/wiki/Social-Login-Setup) wiki page to see how it will work.
* [Angular dynamic forms](https://angular.io/docs/ts/latest/cookbook/dynamic-form.html) for reusability and to keep html code DRY.
* [Swagger](http://swagger.io/) as Api explorer (Visit url **http://127.0.0.1:5000/swagger** after running the application). More [details](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
