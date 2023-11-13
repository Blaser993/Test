using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Test.Database;
using Test.Models;

namespace Test.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public DataController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpPost]
        public async Task<IActionResult> InserisciDati()
        {
            try
            {

                // Effettua la richiesta HTTP per ottenere i dati JSON
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/posts");

                    // Converte JSON in lista di oggetti
                    List<Post>? posts = JsonConvert.DeserializeObject<List<Post>>(response);



                    // Inserisci i Dati nel Database
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<PostContext>();

                        // Aggiungi i nuovi post solo se non esistono già
                        foreach (var post in posts)
                        {
                            if (!dbContext.Posts.Any(p => p.Id == post.Id))
                            {
                                dbContext.Posts.Add(post);
                            }
                        }

                        dbContext.SaveChanges();
                    }

                    return Ok("Dati inseriti con successo");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Errore: {ex.Message}");
            }
        }
    }
}
