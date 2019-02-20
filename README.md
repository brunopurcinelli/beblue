# Challenge BeBlue Rest API

The Challenge Beblue API is a open-source project written in .NET Core

The goal of this project is implement the most common used technologies and share with the technica community the best way to develop the Web Rest API with .NET Core


# Technologies implemented:
<ul>
  <li>ASP.NET Core 2.2 (with .NET Core 2.2)</li>
  <li>ASP.NET WebApi Core</li>
  <li>ASP.NET Identity Core</li>
  <li>Entity Framework Core 2.2</li>
  <li>.NET Core Native DI</li>
  <li>AutoMapper</li>
  <li>FluentValidator</li>
  <li>MediatR</li>
  <li>Swagger UI</li>
  <li>Docker</li>
  <li>Docker Compose</li>
</ul>

# Architecture:
<ul>
  <li>Full architecture with responsibility separation concerns, SOLID and Clean Code</li>
  <li>Domain Driven Design (Layers and Domain Model Pattern)</li>
  <li>Domain Events</li>
  <li>Domain Notification</li>
  <li>CQRS (Imediate Consistency)</li>
  <li>Event Sourcing</li>
  <li>Unit of Work</li>
  <li>Repository and Generic Repository</li>
</ul>

# How to use:
You will need the latest Visual Studio 2017 and the latest .NET Core SDK.
Please check if you have installed the same runtime version (SDK) described in global.json
The latest SDK and tools can be downloaded from https://dot.net/core.
Also you can run the BeBlueAPI in Visual Studio Code (Windows, Linux or MacOS).

To know more about how to setup your enviroment visit the Microsoft .NET Download Guide

You can install BeBlueAPI using docker through the docker hub for the image <a href="https://cloud.docker.com/repository/docker/brunopurcinelli/beblueapiwebapi">brunopurcinelli/beblueapiwebapi</a>
running the <code>docker run brunopurcinelli/beblueapiwebapi:latest </code> from the windows cmd or MacOs or Linux terminal or by running the docker-compose.yml file where the database will be created to use the project.

To use the docker image, you will need a container for the Database in SQL Server 2017 or to use another database you must update the 
<a href="https://github.com/brunopurcinelli/beblue/blob/master/BeBlueApi/BeBlueApi.WebApi/appsettings.json">appsettings.json</a> file of the BeBlueApi.WebApi project by changing its connection string.
  
When compiling the project, open it in the browser using the <a href="https://localhost/swagger">https://localhost/swagger</a> link where it will display all the end points and ViewModels needed to run the API.

# End Points API
<h3>1. Account - Identity Server</h3>
  <ul>
    <li>POST - Login User</li>
    <li>POST - Register User</li>
  </ul>
<h3>2. DiscMusic - Disc Music Table</h3>
<ul>
  <li>GET - Get list with pagination and number of records</li>
  <li>GET - Get one record of Identificator</li>
</ul>
<h3>3. Sales - Making sales with cashback</h3>
<ul>
  <li>GET - Get list with pagination and number of records</li>
  <li>GET - Get one record of Identificator</li>
  <li>POST - Register new sales to get the cashback of client</li>
</ul>
  
  <strong style="forecolor:red;">Note:</strong> In order to realize new sales the user must be <strong>registered and logged</strong> in the API because it needs Authorization to execute this action.
  
  Thanks.
