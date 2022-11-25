# GitHub Metrics

## Overview

## Prerequisites

1. Install git client
1. Install the Azure CLI

## Clone the solution

1. Clone the repository

    ```bash
    git clone https://github.com/gloveboxes/GitHub-Report-Endpoint.git
    ```

1. Change to the `infra` folder

    ```bash
    cd GitHub-Report-Endpoint/infra
    ```

## Deploy the solution

```bash
RESOURCE_GROUP_NAME=<Your_Preferred_Resource_Group_Name>
LOCATION_NAME=<Your_Preferred_Location_Name>
az group create --name $RESOURCE_GROUP_NAME --location $LOCATION_NAME
az deployment group create --resource-group $RESOURCE_GROUP_NAME --template-file main.bicep
```

### Azure Function App Endpoint URL

When the deployment completes, the output will include the Azure Function App Endpoint URL. Make a note of the URL as you will need it for the GitHub Metrics Action.

```json
"outputs": {
    "function_endpoint": {
    "type": "String",
    "value": "Azure Function App URL: https://function-app-abc123.azurewebsites.net"
    }
},
```

## Initialize the Azure SQL Database

For now a manual process. This will be automated in the future.

1. Open the Azure Portal
1. Navigate to the Azure SQL Database, by default named `github-metrics`
1. Open the Query Editor
1. Authenticate with the Azure SQL Database using the SQL Admin user and password you used when you deployed the solution.
1. Create the GitHubStats table

    ```sql
    /****** Object:  Table [dbo].[GitHubStats]    Script Date: 25/11/2022 2:59:51 PM ******/
    SET ANSI_NULLS ON
    GO

    SET QUOTED_IDENTIFIER ON
    GO

    CREATE TABLE [dbo].[GitHubStats](
        [repo_id] [bigint] NOT NULL,
        [date] [datetime] NOT NULL,
        [id] [int] IDENTITY(1,1) NOT NULL,
        [repo] [nvarchar](256) NOT NULL,
        [group] [nvarchar](64) NOT NULL,
        [clones] [int] NULL,
        [views] [int] NULL,
        [stars] [int] NULL,
        [forks] [int] NULL,
        [active] [bit] NOT NULL,
     CONSTRAINT [PK_GitHubStats] PRIMARY KEY CLUSTERED
    (
        [repo_id] ASC,
        [date] ASC
    )WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO

    ALTER TABLE [dbo].[GitHubStats] ADD  CONSTRAINT [DF_GitHubStats_active]  DEFAULT ('true') FOR [active]
    GO
    ```

1. Create the GitHubStatsDaily view

    ```sql
    /****** Object:  View [dbo].[GitHubStatsDaily]    Script Date: 25/11/2022 3:00:51 PM ******/
    SET ANSI_NULLS ON
    GO

    SET QUOTED_IDENTIFIER ON
    GO

    CREATE VIEW [dbo].[GitHubStatsDaily]
    AS

    SELECT TOP(100) PERCENT [group] AS team, repo, clones, [stars], forks, [views], date, EOMONTH(date) AS month_ending, DATEFROMPARTS(YEAR(date),MONTH(date),1)AS [month]
    FROM  dbo.GitHubStats
    WHERE active = 'true'
    GO
    ```

## Deploy GitHub Metrics Action

1. Create a GitHub Personal Access Token (PAT) with the `repo` scope

## GitHub Metrics GitHub Action

```action


```

## Power BI Report

## Contributing

This project is open source and welcomes contributions. Please raise an issue or submit a pull request.
