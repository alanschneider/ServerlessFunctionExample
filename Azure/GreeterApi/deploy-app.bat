call az functionapp create --resource-group GreeterApi-rg --consumption-plan-location westus2 --runtime dotnet --functions-version 3 --name GreeterApi --storage-account greeterapisa
call timeout /t 10 /nobreak
call func azure functionapp publish GreeterApi
call func azure functionapp fetch-app-settings GreeterApi