using Microsoft.AspNetCore.Mvc;
using Overflow.Web.Models;
using System.Net.Http.Json;

namespace Overflow.Web.Controllers;

public class TagsController(IHttpClientFactory httpClientFactory) : Controller
{
    private HttpClient GatewayClient => httpClientFactory.CreateClient("gateway");

    public async Task<IActionResult> Index()
    {
        try
        {
            var tags = await GatewayClient.GetFromJsonAsync<List<TagViewModel>>("/tags") ?? [];
            return View(tags);
        }
        catch
        {
            return View(new List<TagViewModel>());
        }
    }
}
