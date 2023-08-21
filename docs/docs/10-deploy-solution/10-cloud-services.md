# Deploy cloud services

1. Change to the `infra` folder in the cloned repository.

    ```bash
    cd GitHub-Metrics-Endpoint/infra
    ```

1. You need to set the Azure location where you want to deploy the solution. The following command lists the available locations.

    ```bash
    az account list-locations --query "[].{Name:name, DisplayName:displayName}" -o table
    ```

1. Run the following command to deploy the GitHub Metrics solution.

    **PowerShell**

    ```powershell
    $env:RESOURCE_GROUP_NAME="<Your_Preferred_Resource_Group_Name>"
    $env:LOCATION_NAME="<Your_Preferred_Location_Name>"
    az group create --name $env:RESOURCE_GROUP_NAME --location $env:LOCATION_NAME
    az deployment group create --resource-group $env:RESOURCE_GROUP_NAME --template-file main.bicep --query properties.outputs
    ```

    **Bash**

    ```bash
    RESOURCE_GROUP_NAME="<Your_Preferred_Resource_Group_Name>"
    LOCATION_NAME="<Your_Preferred_Location_Name>"
    az group create --name $RESOURCE_GROUP_NAME --location $LOCATION_NAME
    az deployment group create --resource-group $RESOURCE_GROUP_NAME --template-file main.bicep --query properties.outputs
    ```

## Azure Function App Endpoint URL

When the deployment completes, the output will display the _reporting_endpoint_url_ and the _reporting_endpoint_key_.

```json
{
  "reporting_endpoint_url": {
    "type": "String",
    "value": "Azure Function App URL: https://function-app-sample.azurewebsites.net"
  },
  "reporting_endpoint_key": {
    "type": "String",
    "value": "Azure Function Host Key: 989asd98a789d7a8d7a98_sample_key"
  }
}
```

1. Update the **github.env** file in the **config** folder with the _reporting_endpoint_url_ and the _reporting_endpoint_key_.

    ```text
    REPORTING_PAT=
    REPORTING_ENDPOINT_URL=https://function-app-sample.azurewebsites.net
    REPORTING_ENDPOINT_KEY=989asd98a789d7a8d7a98_sample_key
    REPORTING_GROUP=
    ```

1. Save the **github.env** file as you will use this file to upload the GitHub metrics secrets to a repo you want to report to the GitHub Metrics application.
1. Send the **github.env** file to a repo owner so they can upload the GitHub metrics secrets to their repo and start reporting to the GitHub Metrics application.
