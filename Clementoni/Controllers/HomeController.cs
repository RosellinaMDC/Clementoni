using Clementoni.Interfaces;
using Clementoni.Models;
using Clementoni.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Clementoni.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly IPersonaService<PersonaItaliaService> _personaItaliaService;
        private readonly IPersonaService<PersonaFranciaService> _personaFranciaService;

        List<PersonaViewModel> lista = new List<PersonaViewModel>();


        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, IPersonaService<PersonaItaliaService> personaItaliaService, IPersonaService<PersonaFranciaService> personaFranciaService)
        {
            _logger = logger;
            _httpClient = httpClient;
            _personaItaliaService = personaItaliaService;
            _personaFranciaService = personaFranciaService; 

        }

        public async Task<IActionResult> Index()
        {
            //_logger.LogInformation("Hai aperto la home");
            //var response = await _httpClient.GetAsync($"http://google.it");
            //var callcenter = _personaItaliaService.AggiungiPrefisso("+393459873922");
            //_logger.LogInformation($"{callcenter}");
            //var callcentre = _personaFranciaService.AggiungiPrefisso("+33069875640");
            //_logger.LogInformation($"{callcentre}");

            

            return View();
        }

        public IActionResult _FormPersona()
        {
            return PartialView();
        }

        public async Task< IActionResult> _ListPersona()
        {
            var response = await _httpClient.GetAsync($"https://localhost:7058/api/Person");
            _ = response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();    
            var persone = JsonConvert.DeserializeObject<List<PersonaViewModel>>(responseString);

            return PartialView(persone);
        }
        
        [HttpPost]


        public async Task<IActionResult> AddPerson(PersonaViewModel personaViewModel)
        {
            var url = "https://localhost:7058/api/Person";
            var personaJson = JsonConvert.SerializeObject(personaViewModel);

            var data = new StringContent(personaJson.ToString(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, data);

            return  RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var url = $"https://localhost:7058/api/Person/{id}";
           

            var response = await _httpClient.DeleteAsync(url);


            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //GET
        public async Task<IActionResult> Edit(PersonaViewModel persona, int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7058/api/Person" + id);

            var responseString = await response.Content.ReadAsStringAsync();

            var persone = JsonConvert.DeserializeObject<PersonaViewModel>(responseString);

            return PartialView(persone);

        }

        public async Task<IActionResult> Update(PersonaViewModel persona, int id)
        {

            var personaJson = JsonConvert.SerializeObject(persona);
            var data = new StringContent(personaJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("https://localhost:7058/api/Person" + id, data);

            return RedirectToAction("Index");
        }
    }
    
}
