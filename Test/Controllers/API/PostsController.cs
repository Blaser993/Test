using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Test.Database;
using Test.Models;

namespace Test.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private PostContext _myDb;

        public PostsController(PostContext myDb)
        {
            _myDb = myDb;
        }

        [HttpGet]
        public async Task<IActionResult> GetPost()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Fai una richiesta HTTP GET all'URL
                    HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");

                    // Verifica se la richiesta è andata a buon fine (status code 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Leggi il contenuto della risposta come stringa JSON
                        string jsonContent = await response.Content.ReadAsStringAsync();

                        // Deserializza la stringa JSON in una lista di oggetti Post
                        List<Post>? posts = JsonConvert.DeserializeObject<List<Post>>(jsonContent);

                        // Restituisci la lista di oggetti Post come risultato OK
                        return Ok(posts);
                    }
                    else
                    {
                        // Se la richiesta non è andata a buon fine, restituisci un codice di errore
                        return StatusCode((int)response.StatusCode, "Errore nella richiesta HTTP");
                    }
                }
                catch (Exception ex)
                {
                    // Gestisci eventuali eccezioni durante la richiesta
                    return StatusCode(500, $"Errore interno del server: {ex.Message}");
                }
            }
        }

    }
}
