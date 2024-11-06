using examApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace examApi.Controllers
{
    public class EmpDeptController : Controller
    {
        private readonly ILogger<EmpDeptController> _logger;
        public  EmpDeptController(ILogger<EmpDeptController> logger)
        {
            _logger = logger;
        }
       
        public async Task<IActionResult> Index()
        {
            var empdept = await GetEmpDeptAsync();
            return View("Report",empdept);
        }

        private async Task<List<EmpDept>> GetEmpDeptAsync()
        {
           HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7009/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync($"api/Join");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<EmpDept>>(responseData,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            else
            {
                throw new Exception("Failed to retrive empdept from Api");
            }
           
        }
    }
}
