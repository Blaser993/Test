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
    [Route("api/[controller]/[action]")]
    public class DataController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly PostContext _dbContext;

        public DataController(IServiceProvider serviceProvider, PostContext dbContext)
        {
            _serviceProvider = serviceProvider;
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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

                    // Inserisce i Dati nel Database
                    foreach (var post in posts)
                    {
                        var existingPost = _dbContext.Posts.Find(post.UserId);

                        if (existingPost == null)
                        {
                            _dbContext.Posts.Add(post);
                        }
                        else
                        {
                            // L'entità è già presente, aggiorna i suoi dati
                            _dbContext.Entry(existingPost).CurrentValues.SetValues(post);
                        }
                    }

                    _dbContext.SaveChanges();
                }

                return Ok("Dati inseriti con successo");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                return BadRequest($"Errore: {ex.Message}");
            }
        }
    }
}
