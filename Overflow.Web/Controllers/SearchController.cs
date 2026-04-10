using Microsoft.AspNetCore.Mvc;
using Overflow.Web.Models;
using System.Net.Http.Json;

namespace Overflow.Web.Controllers;

public class SearchController(IHttpClientFactory httpClientFactory) : Controller
{
    private HttpClient GatewayClient => httpClientFactory.CreateClient("gateway");

    public async Task<IActionResult> Index(string? query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            ViewBag.Query = query;
            return View(new List<SearchResultViewModel>());
        }

        try
        {
            var results = await GatewayClient.GetFromJsonAsync<List<SearchResultViewModel>>(
                $"/search?query={Uri.EscapeDataString(query)}") ?? [];
            ViewBag.Query = query;
            return View(results);
        }
        catch
        {
            ViewBag.Query = query;
            return View(new List<SearchResultViewModel>());
        }
    }
}
