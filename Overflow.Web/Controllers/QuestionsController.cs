using Microsoft.AspNetCore.Mvc;
using Overflow.Web.Models;
using System.Net.Http.Json;

namespace Overflow.Web.Controllers;

public class QuestionsController(IHttpClientFactory httpClientFactory) : Controller
{
    private HttpClient GatewayClient => httpClientFactory.CreateClient("gateway");

    public async Task<IActionResult> Index(string? tag)
    {
        try
        {
            var url = "/questions" + (tag is not null ? $"?tag={Uri.EscapeDataString(tag)}" : string.Empty);
            var questions = await GatewayClient.GetFromJsonAsync<List<QuestionViewModel>>(url) ?? [];
            ViewBag.SelectedTag = tag;
            return View(questions);
        }
        catch
        {
            ViewBag.SelectedTag = tag;
            return View(new List<QuestionViewModel>());
        }
    }

    public async Task<IActionResult> Details(string id)
    {
        try
        {
            var question = await GatewayClient.GetFromJsonAsync<QuestionViewModel>($"/questions/{id}");
            if (question is null) return NotFound();
            return View(question);
        }
        catch
        {
            return NotFound();
        }
    }
}
