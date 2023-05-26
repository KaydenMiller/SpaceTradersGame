# Setup Dotnet Secrets
If the `UserSecretsId` node does not exist in the `SpaceTraders.csproj` then run `dotnet user-secrets init`.

After you have confirmed the user-secret exists. Set up your secret with your apikey by running this command.
`dotnet user-secrets set "SpaceTraders:ApiKey" "<YOUR_KEY_HERE>"`