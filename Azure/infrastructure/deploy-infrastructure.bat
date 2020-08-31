call az group create --name GreeterApi-rg --location westus2
call timeout /t 10 /nobreak
call az deployment group create --resource-group GreeterApi-rg --template-file template.json --parameters @template.parameters.json