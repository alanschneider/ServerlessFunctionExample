# Serverless Function Example

Currently, it only has an example of creating an Azure Function.

**NOTE 1**: To deploy this example, you will need to rename some of the resources used in this example.

**NOTE 2**: The ARM for creating the APIM for the example isn't included yet. You'll need to do that separately and add the Azure function to the example.


## Directory structure

The code is layered into different parts
* The `Core` directory contains shared, common code and interfaces
  * For a more complex application, it might make sense to refactor into separate class libraries, publish them to a NuGet repo, and pull them into the dependent APIs
* The `Azure` directory contains azure-specific code
  * `infrastructure` contains scripts for building and destroying the resource group (and associated resources) used in the example
* The `Test` directory contains unit tests

If there were an AWS example, it would have its own `Aws` directory for AWS specific implementation.


## Create the infrastructure
From the Azure CLI, run

`./Azure/infrastructure/deploy-infrastructure.bat`

## Deploy the app

Change into the `Azure/GreeterApi` and run `dotnet build`.

Then run:

`./deploy-app.bat`

Because the Azure Storage account is used, you will also need to pull connection string from Azure:

`func azure functionapp fetch-app-settings GreeterApi`

## Run and call the API locally

Change into the `Azure/GreeterApi` directory and run `func start`

### GET operation

`curl --location --request GET 'http://localhost:7071/api/v1/Greet'`

### POST operation w/ request body

```
curl --location --request POST 'http://localhost:7071/api/v1/greet' \
--header 'Content-Type: application/json' \
--data-raw '{
    "name": "Alan"
}'
```

## Call the API in Azure (Direct)

To call a serverless function directly, the `code` query parameter must be provided.
### GET operation

```
curl --location --request GET 'https://greeterapi.azurewebsites.net/api/v1/greet?code=<your code here>'
```

### POST operation

```
curl --location --request POST 'https://greeterapi.azurewebsites.net/api/v1/greet?code=<your code here>' \
--header 'Content-Type: application/javascript' \
--data-raw '{
    '\''name'\'':'\''joe'\''
}'
```


## Call the API in Azure (APIM)

To use the following calls, `Ocp-Apim-Subscription-Key` must be provided.

### GET operation

```
curl --location --request GET 'https://greeter.azure-api.net/v1/greet' \
--header 'Ocp-Apim-Subscription-Key: <your key here>' \
--header 'Ocp-Apim-Trace: true'
```

### POST operation w/ request body

```
curl --location --request POST 'https://greeter.azure-api.net/v1/greet' \
--header 'Ocp-Apim-Subscription-Key: <your key here>' \
--header 'Ocp-Apim-Trace: true' \
--header 'Content-Type: application/json' \
--data-raw '{
    "name": "Alan"
}'
```


## Tear down everything
From the Azure CLI, run

`./Azure/infrastructure/destroy-infrastructure.bat`

# See also
[Quickstart: Create a function in Azure that responds to HTTP requests](https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-azure-function-azure-cli?tabs=bash%2Cbrowser&pivots=programming-language-csharp)

[Connect Azure Functions to Azure Storage using command line tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-add-output-binding-storage-queue-cli?tabs=bash%2Cbrowser&pivots=programming-language-csharp)
