# Dotnet Onion Architecture Template

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![VsCode](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)

[![.NET](https://github.com/mekuwamoto/dotnet-template-onion/actions/workflows/dotnet.yaml/badge.svg)](https://github.com/mekuwamoto/dotnet-template-onion/actions/workflows/dotnet.yaml)

## Overview

Onion Architecture is based on the inversion of control principle. Onion Architecture is comprised of multiple concentric layers interfacing each other towards the core that represents the domain. The architecture does not depend on the data layer as in classic multi-tier architectures, but on the actual domain models.

This project has the following layers:

-   Domain Layer
-   Application Layer
-   Infrastructure Layer
-   Persistent Layer
-   Presentation Layer

## Prerequisites
#### .NET 6
Ensure that you have the correct dotnet core SDK installed on your system.

https://dotnet.microsoft.com/download/dotnet/6.0

This is the version used by this template. In case of the necessity of a newer or a new version, you can do it manually

## Technologies Used

-   [Dotnet Core 6](https://learn.microsoft.com/en-us/dotnet/core/compatibility/6.0)
-   [Asp Net Core 6](https://learn.microsoft.com/pt-br/aspnet/core/release-notes/aspnetcore-6.0?view=aspnetcore-7.0)
-   [Entity Framework Core 6](https://learn.microsoft.com/en-us/ef/core/)
-   [Automapper](https://automapper.org/)
-   [FluentResults](https://github.com/altmann/FluentResults)
-   [Mediatr](https://github.com/jbogard/MediatR)
-   [XUnit](https://xunit.net/)
-   [FluentAssertions](https://fluentassertions.com)
-   [Microsoft SQL Server](https://learn.microsoft.com/en-us/sql/sql-server/?view=sql-server-ver16)

## Features available in this project

This whiteapp contains the following features:

-   [x] Application is implemented on Onion architecture
-   [x] RESTful API
-   [x] Entityframework Core
-   [x] Expection handling
-   [x] Automapper
-   [x] Unit testing via xUnit
-   [x] Versioning
-   [x] Swagger UI
-   [x] CQRS Pattern

## Structure of the template

```
📦 
├─ .gitignore
├─ Onion.Template.sln
├─ README.md
├─ src
│  ├─ Onion.Template.Api
│  │  ├─ Controllers
│  │  │  ├─ Commom
│  │  │  │  ├─ BaseController.cs
│  │  │  │  └─ ErrorsController.cs
│  │  │  ├─ Todos
│  │  │  │  └─ TodoController.cs
│  │  │  └─ User
│  │  │     └─ UserController.cs
│  │  ├─ Middlewares
│  │  │  └─ MiddlewaresConfiguration.cs
│  │  ├─ Onion.Template.Api.csproj
│  │  ├─ Program.cs
│  │  ├─ Properties
│  │  │  └─ launchSettings.json
│  │  ├─ Services
│  │  │  └─ DependencyInjection.cs
│  │  ├─ appsettings.Development.json
│  │  └─ appsettings.json
│  ├─ Onion.Template.Application
│  │  ├─ Commom
│  │  │  ├─ Attributes
│  │  │  │  └─ InjectionAttributes.cs
│  │  │  ├─ Enums
│  │  │  │  └─ DI.cs
│  │  │  ├─ Interfaces
│  │  │  │  ├─ Authentication
│  │  │  │  │  ├─ IJwtTokenGenerator.cs
│  │  │  │  │  ├─ IPwdHasher.cs
│  │  │  │  │  └─ IUserAccessor.cs
│  │  │  │  └─ Repositories
│  │  │  │     ├─ IRepository.cs
│  │  │  │     ├─ ITodoRepository.cs
│  │  │  │     └─ IUserRepository.cs
│  │  │  └─ Settings
│  │  │     └─ HashSettings.cs
│  │  ├─ DependencyInjection.cs
│  │  ├─ Onion.Template.Application.csproj
│  │  ├─ Todos
│  │  │  ├─ Commands
│  │  │  │  ├─ CompleteTodoItem
│  │  │  │  │  └─ CompleteTodoItem.cs
│  │  │  │  ├─ CreateTodo
│  │  │  │  │  └─ CreateTodoCommand.cs
│  │  │  │  ├─ DeleteTodo
│  │  │  │  │  └─ DeleteTodo.cs
│  │  │  │  └─ RenameTodoTitle
│  │  │  │     └─ RenameTodoTitle.cs
│  │  │  ├─ Profiles
│  │  │  │  └─ TodoMappingProfile.cs
│  │  │  ├─ Queries
│  │  │  │  ├─ GetSingleTodo
│  │  │  │  │  └─ GetSingleTodo.cs
│  │  │  │  └─ GetTodos
│  │  │  │     └─ GetTodos.cs
│  │  │  ├─ Requests
│  │  │  │  ├─ CreateTodoRequest.cs
│  │  │  │  └─ EditTodoRequest.cs
│  │  │  └─ Response
│  │  │     ├─ NotFoundTodoError.cs
│  │  │     ├─ TaskAlreadyCompletedError.cs
│  │  │     └─ TodoResponse.cs
│  │  └─ Users
│  │     ├─ Commands
│  │     │  ├─ Login
│  │     │  │  └─ Login.cs
│  │     │  └─ Register
│  │     │     └─ Register.cs
│  │     ├─ Requests
│  │     │  ├─ LoginRequest.cs
│  │     │  └─ RegisterUserRequest.cs
│  │     └─ Response
│  │        ├─ Errors
│  │        │  ├─ DuplicateEmailError.cs
│  │        │  └─ LoginError.cs
│  │        └─ Successful
│  │           └─ UserTokenResponse.cs
│  ├─ Onion.Template.Domain
│  │  ├─ Commom
│  │  │  └─ CurrentUser.cs
│  │  ├─ Entities
│  │  │  ├─ BaseEntity.cs
│  │  │  ├─ Todo.cs
│  │  │  └─ User.cs
│  │  └─ Onion.Template.Domain.csproj
│  ├─ Onion.Template.Infrastructure
│  │  ├─ Authentication
│  │  │  ├─ JwtTokenGenerator.cs
│  │  │  ├─ PwdHasher.cs
│  │  │  └─ UserAccessor.cs
│  │  ├─ DependencyInjection.cs
│  │  ├─ Onion.Template.Infrastructure.csproj
│  │  └─ Settings
│  │     └─ JwtSettings.cs
│  └─ Onion.Template.Persistence
│     ├─ Contexts
│     │  └─ UserContext.cs
│     ├─ DependencyInjection.cs
│     ├─ Interfaces
│     │  └─ IUserContext.cs
│     ├─ Mappings
│     │  ├─ TodoMapping.cs
│     │  └─ UserMapping.cs
│     ├─ Migrations
│     │  ├─ 20230612010543_InitialMigration.Designer.cs
│     │  ├─ 20230612010543_InitialMigration.cs
│     │  ├─ 20230612143821_AddConstraints.Designer.cs
│     │  ├─ 20230612143821_AddConstraints.cs
│     │  ├─ 20230628010732_UpdatedUserMapping.Designer.cs
│     │  ├─ 20230628010732_UpdatedUserMapping.cs
│     │  ├─ 20230705005150_CreatedTodoMapping.Designer.cs
│     │  ├─ 20230705005150_CreatedTodoMapping.cs
│     │  ├─ 20230709232714_TodoSoftDelete.Designer.cs
│     │  ├─ 20230709232714_TodoSoftDelete.cs
│     │  └─ UserContextModelSnapshot.cs
│     ├─ Onion.Template.Persistence.csproj
│     └─ Repositories
│        ├─ TodoRepository.cs
│        └─ UserRepository.cs
└─ tests
   └─ UnitTests
      ├─ Onion.Template.Api.UnitTests
      │  ├─ Onion.Template.Api.UnitTests.csproj
      │  └─ Usings.cs
      ├─ Onion.Template.Application.UnitTests
      │  ├─ Onion.Template.Application.UnitTests.csproj
      │  ├─ Todos
      │  │  ├─ Commands
      │  │  │  ├─ CompleteTodoItem
      │  │  │  │  └─ CompleteTodoItemTests.cs
      │  │  │  ├─ CreateTodo
      │  │  │  │  └─ CreateTodoCommand.cs
      │  │  │  ├─ DeleteTodo
      │  │  │  │  └─ DeleteTodoTests.cs
      │  │  │  └─ RenameTodoTitle
      │  │  │     └─ RenameTodoTitleTests.cs
      │  │  └─ Factories
      │  │     └─ TodoFactory.cs
      │  ├─ Users
      │  │  ├─ Commands
      │  │  │  ├─ Login
      │  │  │  │  └─ LoginTests.cs
      │  │  │  └─ Register
      │  │  │     └─ RegisterTests.cs
      │  │  └─ Fixtures
      │  │     └─ UsersFixtures.cs
      │  └─ Usings.cs
      ├─ Onion.Template.Domain.UnitTests
      │  ├─ Commom
      │  │  └─ CurrentUserTests.cs
      │  ├─ Entities
      │  │  ├─ TodoTests.cs
      │  │  └─ UserTests.cs
      │  ├─ Onion.Template.Domain.UnitTests.csproj
      │  └─ Usings.cs
      ├─ Onion.Template.Infrastructure.UnitTests
      │  ├─ Onion.Template.Infrastructure.UnitTests.csproj
      │  └─ Usings.cs
      └─ Onion.Template.Persistence.UnitTests
         ├─ Onion.Template.Persistence.UnitTests.csproj
         └─ Usings.cs
```


## Licence

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/Amitpnk/Onion-architecture-ASP.NET-Core/blob/develop/LICENSE)
