using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlagueApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlaguesController : ControllerBase
    {

        // GET api/<ValuesController>/5
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            // connect to db
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("blague");

            // collection
            var collection = database.GetCollection<BsonDocument>("blague");

            // find all blague
            var numberBlague = 0;
            var data = await collection.Find(new BsonDocument()).ToListAsync();
            foreach (var i in data) numberBlague++;

            // get random blague
            var randomNumber = new Random().Next(0, numberBlague);
            var numberTour = 0;
            foreach (var i in data)
            {
                if (numberTour == randomNumber)
                {
                    var response = new BsonDocument()
                        .Add("question", i.GetElement("blague").Value.ToString())
                        .Add("response", i.GetElement("reponse").Value.ToString());
                    return Ok(response.ToString());
                }
                numberTour++;
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {

            // connect to db
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("blague");

            // collection
            var collection = database.GetCollection<BsonDocument>("blague");

            // find all blague
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var data = collection.Find(filter).FirstOrDefault();

            // get random blague
            try
            {

                var response = new BsonDocument()
                           .Add("question", data.GetElement("blague").Value.ToString())
                           .Add("response", data.GetElement("reponse").Value.ToString());
                return Ok(response.ToString());
            }
            catch (FormatException)
            {
                // send http response code
                return NotFound();
            }
            catch (MongoException)
            {
                // send http response code
                return NotFound();
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post(string blague, string reponse)
        {
            // connect to db
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("blague");

            // collection
            var collection = database.GetCollection<BsonDocument>("blague");

            try
            {
                // insert and create data
                var document = new BsonDocument()
                    .Add("question", blague)
                    .Add("response", reponse);

                await collection.InsertOneAsync(document.ToBsonDocument());

                // send http response code
                return Ok();
            }
            catch (FormatException)
            {
                // send http response code
                return NotFound();
            }
            catch (MongoException)
            {
                // send http response code
                return NotFound();
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            // connect to db
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("blague");

            // collection
            var collection = database.GetCollection<BsonDocument>("blague");

            try
            {
                // delete table from id
                var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
                await collection.DeleteOneAsync(filter);

                // send http response code
                return Ok();
            }
            catch (FormatException)
            {
                // send http response code
                return NotFound();
            }
            catch (MongoException)
            {
                // send http response code
                return NotFound();
            }

        }
    }
}
