using API.Utilities.Handlers;
using Client.Contracts;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories;

public class GeneralRepository<Entity, TId> : IRepository<Entity, TId>
        where Entity : class
{
    private readonly string request;
    private readonly HttpContextAccessor contextAccessor;
    private HttpClient httpClient;

    //constructor
    public GeneralRepository(string request)
    {
        this.request = request;
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7007/api/")
        };
        //contextAccessor = new HttpContextAccessor();
        // Ini yg bawah skip dulu
        //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", contextAccessor.HttpContext?.Session.GetString("JWToken"));
    }

    public async Task<ResponseOKHandler<Entity>> Delete(TId id)
    {
        ResponseOKHandler<Entity> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
        using (var response = httpClient.DeleteAsync(request + id).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<Entity>>(apiResponse);
        }
        return entityVM;
    }

    public async Task<ResponseOKHandler<IEnumerable<Entity>>> Get()
    {
        ResponseOKHandler<IEnumerable<Entity>> entityVM = null;
        using (var response = await httpClient.GetAsync(request))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<IEnumerable<Entity>>>(apiResponse);
        }
        return entityVM;
    }

    public Task<ResponseOKHandler<Entity>> Get(TId id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseOKHandler<Entity>> Post(Entity entity)
    {
        ResponseOKHandler<Entity> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        using (var response = httpClient.PostAsync(request, content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseOKHandler<Entity>>(apiResponse);
        }
        return entityVM;
    }

    public Task<ResponseOKHandler<Entity>> Put(TId id, Entity entity)
    {
        throw new NotImplementedException();
    }
}