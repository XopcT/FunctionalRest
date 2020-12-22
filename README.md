# FunctionalRest
Have you ever been bored when writing long and bulky request in HttpClient or RestSharp?
```
var client = new RestClient(endpoint);
client.UseSerializer(() => new NewtonsoftJsonSerializer());
var request = new RestRequest($"/api/v1/{username}/orders", Method.POST);
request.AddHeader("Authorization", accessToken);
request.AddJsonBody(data);
var response = await client.ExecuteAsync<MyResponse>(request, cancellationToken);
if(!response.IsSuccessful)
{
   throw new ...
}
```
They all look the same but are a little bit different which can cause a headache.

## Stop enduring it!
Now you have **FunctionalRest** which makes things easier:
```
new RestClient(endpoint)
   .SendsRequest($"/api/v1/{username}/orders", Method.POST)
   .WithJwtAuthentication(accessToken)
   .WithHeader("your header", "Hello world!")
   .WithJsonBody(data)
   .ExecuteAsync(onError: e => { throw new ... });
```
Much easier, right?
Works with [RestSharp](https://www.nuget.org/packages/FunctionalRest.RestSharp)
Will work with HttpClient soon
Don't hesitate to download **FunctionalRest** [from NuGet!](https://www.nuget.org/packages/FunctionalRest)