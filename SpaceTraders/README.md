# Setup Dotnet Secrets

If the `UserSecretsId` node does not exist in the `SpaceTraders.csproj` then run `dotnet user-secrets init`.

After you have confirmed the user-secret exists. Set up your secret with your apikey by running this command.
`dotnet user-secrets set "SpaceTraders:ApiKey" "<YOUR_KEY_HERE>"`

# API Generation
You may be able to generate teh api and related models using the `Open API Generator CLI`. However, I have been unable
to get the generator to work when including the model validations. This is due to it using a fully qualified name somewhere
that that will not work for the ValidationResults. I am not sure why this is a problem.

On another note you cannot generate an api client that uses `System.Text.Json` instead of `Neutonsoft.Json` which is not ideal.

Command Used:
```powershell
openapi-generator-cli generate -i https://stoplight.io/api/v1/projects/spacetraders/spacetraders/nodes/reference/SpaceTraders.json -g csharp-netcore --additional-properties=apiName="SpaceTradersApi" --additional-properties=targetFramework="net7.0" --additional-properties=packageName="SpaceTraders.Api" --additional-properties=validatable="true" --additional-properties=nullableReferenceTypes="true" --additional-properties=library="genericHost"  
```