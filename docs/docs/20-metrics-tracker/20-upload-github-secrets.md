# Upload GitHub secrets

For each repo you want to track, upload the GitHub secrets by following these steps.

1. Install the [GitHub CLI](https://github.com/cli/cli#installation).
1. Open a command prompt and change to the `deploy` folder of the cloned repo.
1. Authenticate with the GitHub CLI

    ```bash
    gh auth login
    ```

1. Set the GitHub repo secrets by running the following command. Be sure to replace the **<GITHUB_REPO_URL>** with your target GitHub repo URL.

    ```bash
    gh secret set --env-file github.env --repo <GITHUB_REPO_URL>
    ```
