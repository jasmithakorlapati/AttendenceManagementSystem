using examApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace examApi.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _context;

        public EmployeeController(ILogger<EmployeeController> context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employee= await GetEmployeesAsync(); // Fetch Employee data, not Department
            return View("EmployeeView",employee);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Create()
        {
            Employee employee = new Employee();
            return View("Create", employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await PostDepartmentAsync(employee);

                if (isSuccess)
                {
                    return RedirectToAction("Index");  // Redirect to Index or success page
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to insert department.");
                }
            }

            return View(employee);
        }
        
        private async Task<bool> PostDepartmentAsync(Employee employee)
        {
            HttpClient client = new HttpClient();
            // Set base address and headers
            client.BaseAddress = new Uri("https://localhost:7009/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Serialize the department object to JSON
            var json = System.Text.Json.JsonSerializer.Serialize(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Send the POST request to the Web API
            HttpResponseMessage response = await client.PostAsync("api/Employees", content);

            // Return true if the request was successful
            return response.IsSuccessStatusCode;
        }

        private static async Task<List<Employee>> GetEmployeesAsync()
        {
            HttpClient client = new HttpClient();

            // Set the base address of the API
            client.BaseAddress = new Uri("https://localhost:7009/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Call the API
            HttpResponseMessage response = await client.GetAsync("api/Employees");
            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Deserialize the JSON response to a List<Department>
                var responseData = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<List<Employee>>(responseData,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            else
            {
                throw new Exception("Failed to retrieve departments from API");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //Employee employee=new Employee();
            var employee =  await GetEmployeesAsync(id);
            return View("EditView", employee);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(int id,Employee employee)
        {
            id = employee.EmpId;
            HttpClient client = new HttpClient();

            string apiUrl = $"https://localhost:7009/api/Employees/{id}";
            var jsonContent = JsonConvert.SerializeObject(employee);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // or appropriate action
            }
            else
            {
                ModelState.AddModelError("", "Error updating Employee.");
                return View(employee);
            }
        }
        private static async Task<Employee> GetEmployeesAsync(int id)
        {
            HttpClient client = new HttpClient();

            // Set the base address of the API
            client.BaseAddress = new Uri("https://localhost:7009/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Call the API
            HttpResponseMessage response = await client.GetAsync($"api/Employees/{id}");

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Deserialize the JSON response to a List<Department>
                var responseData = await response.Content.ReadAsStringAsync();
                return (Employee)System.Text.Json.JsonSerializer.Deserialize<Employee>(responseData,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            else
            {
                throw new Exception("Failed to retrieve departments from API");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await GetEmployeesAsync(id);
            return View("Delete",employee);
        }
        public async Task<IActionResult> DeleteById(int id)
        {

            var isSuccess = await DeleteEmployeesAsync(id);

            if (isSuccess)
            {
                return RedirectToAction("Index");  // Redirect to Index or success page
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete Employee.");
            }
            return RedirectToAction("Index");
        }
        private async Task<bool> DeleteEmployeesAsync(int id)
        {
            HttpClient client = new HttpClient();
            // Set the base address of the API
            client.BaseAddress = new Uri("https://localhost:7009/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.DeleteAsync($"api/Employees/{id}");
            return response.IsSuccessStatusCode;
        }

       

    }
}
