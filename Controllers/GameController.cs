using System.Collections.Generic;
using learndotnet.Models;
using learndotnet.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using learndotnet.Data;
using System.Linq; // Required for LINQ extensions like ToList()


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
        private readonly ILogger<GameController> _logger;
        private readonly ApplicationDbContext _db;

        public GameController(ILogger<GameController> logger, ApplicationDbContext db) { // Application will send the Logger object in then constructor 
           _logger = logger;
           _db = db;

           //Just for testing
           logger.LogInformation("LogInformation"); // GREEN color shown in cmd
           logger.LogError("LogError");            // RED color shown in cmd
           //use SeriLog (3rd party) to log into file
        }

        // we can use "ActionResult" to wrap our response (from Mvc namespace)
        [HttpGet] // GET request of route /games handeled by this function
        public ActionResult<List<GamesDto>> GetGames()
        {
            return Ok(_db.GameTable.ToList()); // Now we return data with status code as well (this is a method from ControllerBase)
        }

        [HttpGet("{id}", Name="GetGame")]  // we can Name the routs so that they can be referenced in the code
        public ActionResult<GamesDto> GetGame(int id)
        {
            if (id == 0) return BadRequest(); // 400

            //GamesDto game = gamesList.Find(element => element.Id == id);
            var game = _db.GameTable.FirstOrDefault(element => element.Id == id);
            if (game == null) return NotFound(); //404

            return Ok(game); // 200
        }

        [HttpPost]
        public ActionResult<GamesDto> PostGame([FromBody] GamesDto response) {

            // ModelState object
            if (response == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 if the model is invalid
            }

            GameModel game = new GameModel { 
                // Id auto generated at db
                Name = response.Name, 
                Device = response.Device,
                HashKey = this.GetHashCode() + "" // Privae key generted on server 
            };

            _db.GameTable.Add(game);
            _db.SaveChanges();

            //response object is sent to te client as it is, hence upate the id else it will be 0
            response.Id = game.Id;

            // This will return the route of newely added item in the location attriute of response header
            return CreatedAtRoute(
                                    "GetGame",              // Name of the route for location header
                                    new { id = game.Id },   // Id to add to the route -> https://yourapi.com/api/games/{id}
                                    response                // send back the data client sent in body
                                  ); // 201

            /*
            Q1] How does game var get Id value
            Entity Framework:
                When you create a new GameModel instance and add it to the _db.GameTable, the database generates the Id (assuming it’s set as an auto-incrementing identity column).

            Saving Changes:
                When you call _db.SaveChanges(), Entity Framework sends the insert command to the database, which generates the Id.

            Retrieving the Generated Id:
                After the save operation, the game object will have its Id property automatically populated with the value generated by the database.
            
            Q2] why not return CreatedAtRoute("GetGame",  id , response); How is Anonymous object helpful
            Suppose you have the following route defined for getting a game:
            [HttpGet("{id}/{categoryId}", Name = "GetGame")]
            public ActionResult<GamesDto> GetGame(int id, int categoryId)
            {
                // Implementation here
            }

            Fom POST you can return multipe values: 
            return CreatedAtRoute(
                                    "GetGame", 
                                    new { id = game.Id, categoryId = game.CategoryId },
                                    response
                                  )

            Given this route definition and assuming you have:
            > game.Id equals 5
            > game.CategoryId equals 10

            The final route generated would look like:
            GET /api/games/5/10

            */

        }

        // PUT -> Complete update (commonly used)
        [HttpPut("{id}")]
        public IActionResult PutGame(int id, [FromBody] GamesDto response) {

            if (id != response.Id) return BadRequest();

            // Find the existing game in the database
            GameModel existingGame = _db.GameTable.Find(id);
            if (existingGame == null) 
            {
                return NotFound(); // Return 404 if the game is not found
            }

            existingGame.Name = response.Name;
            existingGame.Device = response.Device;
            existingGame.HashKey = this.GetHashCode() + ""; // Optionally update

            _db.GameTable.Update(existingGame);
            _db.SaveChanges();

            return NoContent(); 
        }

        // PATCH -> Partial update
        // This can we used when there are a lot of properties and out of which only a few needs to be updated
        // Need to install 3rd Party NuGate library, and its a bit weird


        [HttpDelete("{id}")]
        public IActionResult DeleteGame(int id) {  // return type can be 'IActionResult' in case you dont want to return any data
        
            if (id == 0) return BadRequest();

            var game = _db.GameTable.FirstOrDefault(element => element.Id == id);
            if(game == null) return NotFound();

            _db.GameTable.Remove(game);
            _db.SaveChanges();
            
            return NoContent();
        }

    }
}

