using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;



/// <summary>
/// The following process builds a host process for the WeberLabAPIService to enable calling XML API examples hosted by Harvard Catalyst Profiles (https://connects.catalyst.harvard.edu/). 
/// Each example is written to the Console as text output. 
/// To execute each example, enable/disable the string endpoint variable with in the WeberLabAPService below. For more information, code examples in different languages and documentation please
/// visit the Weber Lab Postman.com repository
///     https://www.postman.com/weberlab/workspace/weber-lab-public-apis
/// </summary>

var host = new HostBuilder()
    .ConfigureServices(services =>
    {
        services.AddHttpClient();
        services.AddTransient<WeberLabAPIService>();
    })
    .Build();

try
{
    var weberLabAPIService = host.Services.GetRequiredService<WeberLabAPIService>();
    var getAPIData = await weberLabAPIService.GetAPIDataAsync();

    Console.WriteLine(getAPIData);

   
}
catch (Exception ex)
{
    host.Services.GetRequiredService<ILogger<Program>>()
        .LogError(ex, $"Unable to call API. {ex.Message}");
}

/// <summary>
///  The following class has 6 XML API endpoints that return Researcher data from Harvard Catalyst Profiles.  
/// </summary>
public class WeberLabAPIService
{

    // viewas/xml 
    // Click link below to fork this endpoint request to your local Postman repository
    // https://www.postman.com/weberlab/workspace/bfcff89f-b931-4f3a-8b7d-999f631b54f3/request/14271032-00acc2af-f5c4-4f82-81f4-7a8dacd4bc34
    private string endpoint = "https://connects.catalyst.harvard.edu/profiles/display/Person/32213/viewas/xml";

    // RESTful Single PersonID query
    // Click link below to fork this endpoint request to your local Postman repository
    // https://www.postman.com/weberlab/workspace/bfcff89f-b931-4f3a-8b7d-999f631b54f3/request/14271032-fce0ff8c-d5f9-40f4-be67-b38d64bd1ef7
    // private string endpoint = "https://connects.catalyst.harvard.edu/API/Profiles/Public/ProfilesDataAPI/getPeople/xml/PersonIDs/32231/columns/all";

    // RESTful List of PersonIDs query
    // Click link below to fork this endpoint request to your local Postman repository
    //https://www.postman.com/weberlab/workspace/bfcff89f-b931-4f3a-8b7d-999f631b54f3/request/14271032-aaac3d58-823b-448c-bd20-4109ce86ce9f
    // private string endpoint = "https://connects.catalyst.harvard.edu/API/Profiles/Public/ProfilesDataAPI/getPeople/xml/PersonIDs/45195,32231,55724/columns/all";

    // RESTful Institution query
    // Click link below to fork this endpoint request to your local Postman repository
    //https://www.postman.com/weberlab/workspace/bfcff89f-b931-4f3a-8b7d-999f631b54f3/request/14271032-a3354e25-4627-48a2-9411-479d831ba687
    // private string endpoint = "https://connects.catalyst.harvard.edu/API/Profiles/Public/ProfilesDataAPI/getpeople/Institution/beth%20israel%20deaconess%20medical%20center/columns/all";

    // RESTful Department within Institution query
    // Click link below to fork this endpoint request to your local Postman repository
    //https://www.postman.com/weberlab/workspace/bfcff89f-b931-4f3a-8b7d-999f631b54f3/request/14271032-9b88cc5a-2067-41b2-a6ce-3a5562c579c1
    // private  string endpoint = "https://connects.catalyst.harvard.edu/API/Profiles/Public/ProfilesDataAPI/getpeople/Institution/beth%20israel%20deaconess%20medical%20center/department/Neurosurgery/columns/all";

    // RESTful Institution for people who research a given MeSH Concept (Keyword)
    // Click link below to fork this endpoint request to your local Postman repository
    //https://www.postman.com/weberlab/workspace/bfcff89f-b931-4f3a-8b7d-999f631b54f3/request/14271032-8486589c-9288-419c-9f3d-f2b0ad8830f0
    // private string endpoint = "https://connects.catalyst.harvard.edu/API/Profiles/Public/ProfilesDataAPI/getpeople/Institution/beth%20israel%20deaconess%20medical%20center/keyword/Myocardial Infarction/columns/all";


    private readonly IHttpClientFactory _httpClientFactory;

    public WeberLabAPIService(IHttpClientFactory httpClientFactory) =>
        _httpClientFactory = httpClientFactory;

    public async Task<string?> GetAPIDataAsync()
    {
        var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
           endpoint)
        {
            Headers =
            {
                { "Accept", "*/*" }             
            }            
        };

        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        httpResponseMessage.EnsureSuccessStatusCode();

        var data = await httpResponseMessage.Content.ReadAsStringAsync();

        return data;
    }
}
