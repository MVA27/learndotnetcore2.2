using System.Collections.Generic;
using learndotnet.Models;
using learndotnet.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace learndotnet.Controllers
{
    [Route("games/")]
    [ApiController]
    public class GameController : ControllerBase
    {
        /*
        To make a class controller
            #1 : Class name should end with "Controller" (coz of internal design)
            #2 : Inherit from ControllerBase to get ability of a controller such as sending data with status code etc.
                 (we are using ControllerBase and not Controller as Controller class has support for views, 
                 and we are just using api so views are not required)
            #3 : Add [ApiController] annotation which tells the application that this class is the controller
        */

        private static readonly List<GamesDto> gamesList = new List<GamesDto> {
            new GamesDto { Id = 1, Name = "PUBG" },
            new GamesDto { Id = 2, Name = "BBGMI" }
        };

        /******** BASIC WAY **********
         * 
        [HttpGet] // GET request of route /games handeled by this function
        public List<GamesDto> GetGames() {
            return gamesList;
        }

        [HttpGet("id")]  // or [HttpGet("{id:int}")]
        public GamesDto? GetGame(int id) {
            return gamesList.Find(element => element.Id == id);
        }
        */

        //******** INDUSTRY STANDARD ********

        /*
         *  Dependency Injection :
            Some Objects are already provided in the application (e.g., Logger for logging)
            We can access then through constructor
         */
        //private readonly ILogger<GameController> _logger;
        //public GameController(ILogger<GameController> logger) { // Application will send the Logger object in then constructor 
        //    _logger = logger;
            
        //    // Just for testing
        //    //logger.LogInformation("LogInformation"); // GREEN color shown in cmd
        //    //logger.LogError("LogError");            // RED color shown in cmd
        //    // use SeriLog (3rd party) to log into file
        //}

        // we can use "ActionResult" to wrap our response (from Mvc namespace)
        [HttpGet] // GET request of route /games handeled by this function
        public ActionResult<List<GamesDto>> GetGames()
        {
            return Ok(gamesList); // Now we return data with status code as well (this is a method from ControllerBase)
        }

        [HttpGet("{id}", Name="GetGame")]  // we can Name the routs so that they can be referenced in the code
        public ActionResult<GamesDto> GetGame(int id)
        {
            if (id == 0) return BadRequest(); // 400

            GamesDto game = gamesList.Find(element => element.Id == id);
            if (game == null) return NotFound(); //404

            return Ok(game); // 200
        }

        [HttpPost]
        public ActionResult<GamesDto> PostGame([FromBody] GamesDto response) {

            // ModelState object
            // ModelState

            gamesList.Add(response);

            // This will return the route of newely added item in the location attriute of response header
            return CreatedAtRoute("GetName", new { id = response.Id }, response); // 201

        }

        // PUT -> Complete update (commonly used)
        [HttpPut("{id}")]
        public IActionResult PutGame(int id, [FromBody] GamesDto response) {

            if (id != response.Id) return BadRequest();

            var game = gamesList.Find(element => element.Id == id);

            if (game == null) return NotFound();

            game.Name = response.Name;

            return NoContent(); 

        }

        // PATCH -> Partial update
        // This can we used when there are a lot of properties and out of which only a few needs to be updated
        // Need to install 3rd Party NuGate library, and its a bit weird


        [HttpDelete("{id}")]
        public IActionResult DeleteGame(int id) {  // return type can be 'IActionResult' in case you dont want to return any data
        
            if (id == 0) return BadRequest();

            var game = gamesList.Find(element => element.Id == id);
            if(game == null) return NotFound();

            gamesList.Remove(game);
            return NoContent();
        }

    }
}

