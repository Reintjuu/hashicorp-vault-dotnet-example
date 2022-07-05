# .NET/HashiCorp Vault example

Example project for reading/creating configuration data which will be automatically reloaded on change.

- [.NET/HashiCorp Vault example](#nethashicorp-vault-example)
- [Running with Tye](#running-with-tye)
    - [Installation](#installation)
    - [Usage](#usage)
- [Running with Docker](#running-with-docker)
- [Using the sample project](#using-the-sample-project)

# Running with Tye

Using Tye to run services and manage their service discovery, using `Microsoft.Tye.Extensions.Configuration`.

## Installation

`dotnet tool install -g Microsoft.Tye --version "0.11.0-alpha.22111.1"`

## Usage

1. Start the file watching build service.
   `tye run --watch`
2. Navigate to http://localhost:8000.
3. Open the http backend and navigate to its Swagger UI.

# Running with Docker

1. Run the following.
   `docker-compose up -d`
2. Access the API via http://localhost:5000/swagger

# Using the sample project

1. Option 1: Navigate to the Swagger UI and `POST` a `DefaultConfiguration` value to the Vault through
   the `DefaultConfiguration` endpoint.
2. Option 2: Create a configuration value at `"secret/backend/DefaultConfiguration/SecretValue": "42069"` using the
   Vault CLI or the Vault UI (with token password: `root`).
3. `GET` and/or `POST` data using the endpoints in the `DefaultConfigurationController` or `ConfigurationController`.
   Also, watch the `backend` log the `DefaultConfiguration` value using the `OptionsMonitorLogger` with
   its `IOptionsMonitor`.