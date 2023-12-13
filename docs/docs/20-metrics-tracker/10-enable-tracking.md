# Create a Personal Access Token

Create a Personal Access Token that can be **reused** across all the GitHub repos you want to track.

1. Navigate to the GitHub web portal and log in.
1. Select your profile icon in the top right corner.
1. Select **Settings**, then **Developer Settings**, then **Personal Access Tokens**.
1. Select **Fine-grained tokens** and then **Generate new token**.
1. Name the token **GitHub Metrics**.
1. Set the **Expiration**. You probably want to set this to custom and set the date to 1 year in the future.
1. Select **All repositories**, or if you want finer control, select **Only select repositories** and select the repos you want to track.
1. Select **Repository permissions**.
1. Select **Administration** to **Read-only**.
1. Leave the remaining fields with their default values.
1. Select **Generate token**.
1. Copy the token to the clipboard.

## Update the GitHub Secrets environment file

A GitHub secrets environment file simplifies the process of uploading the secrets to one or more GitHub repos.

Use the github.env file you received from the GitHub Metrics solution owner or the one you created in the [Deploy the GitHub Metrics solution](../10-deploy-solution/03-introduction.md) section.

1. Open the **github.env** file in the `config` folder of the cloned repo.
1. Update the **REPORTING_PAT** value with the Personal Access Token you copied to the clipboard in the previous step.
1. Update the **REPORTING_GROUP** field. The group secret is used for consolidated reporting. The group name is arbitrary, for example, use your team name or your GitHub name.
1. The **REPORTING_ENDPOINT_URL** and **REPORTING_ENDPOINT_KEY** values are populated by the GitHub Metrics solution owner.



