@description('Name of the solution which is used to generate a short unique hash used in all resources.')
@minLength(6)
@maxLength(50)
param AppName string

@description('The administrator username of the SQL logical server.')
param SqlAdministratorLogin string

@description('The administrator password of the SQL logical server.')
@secure()
param SqlAdministratorLoginPassword string

@description('Location for all resources.')
param location string = resourceGroup().location

@description('Azure SQL GitHub metrics database name.')
param sqlDBName string = 'github-metrics'

var app_name = toLower(AppName)
var hash = uniqueString(app_name, resourceGroup().id)

// storage accounts must be between 3 and 24 characters in length and use numbers and lower-case letters only
var storageAccountName = 'storage${hash}'
var hostingPlanName = 'app-plan-${hash}'
var appInsightsName = 'app-insights-${hash}'
var functionAppName = 'function-app-${hash}'
var sqlServerName = 'azure-sql-${hash}'

module resources './resources.bicep' = {
  name: 'resources'
  params: {
    sqlServerName: sqlServerName
    sqlDBName: sqlDBName
    location: location
    sqlAdministratorLogin: SqlAdministratorLogin
    sqlAdministratorLoginPassword: SqlAdministratorLoginPassword
    storageAccountName: storageAccountName
    appInsightsName: appInsightsName
    hostingPlanName: hostingPlanName
    functionAppName: functionAppName
  }
}

output function_endpoint string = 'Azure Function App URL: https://${functionAppName}.azurewebsites.net'
output function_key string = 'Azure Function Host Key: ${resources.outputs.defaultHostKey}'
