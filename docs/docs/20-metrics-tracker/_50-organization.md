# Create a GitHub Organization Personal Access Token

1. Navigate to the GitHub web portal and login.
1. Select your profile icon in the top right corner.
1. Select **Settings**, then **Developer Settings**, then **Personal Access Tokens**.
1. Select **Fine-grained tokens** and then **Generate new token**.
1. Name the token **GitHub Metrics ORGANIZATION_NAME** and add the name of the GitHub organization you want to track.
1. Set the **Expiration**. You probably want to set this to custom and set the date to 1 year in the future.
1. Select the **Organization** you want to track from the **Resource owner** dropdown list.
1. Select **All repositories**, or if you want finer control, select ***Only select repositories*** and select the repos you want to track.
1. Select **Repository permissions**.
1. Select **Administration** to **Read-only**.
1. Leave the remain fields with their default values.
1. Select **Generate token**.
1. Copy the token to the clipboard.