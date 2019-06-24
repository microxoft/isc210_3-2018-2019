using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using isc210_3_2018_2019_rpg_webapi.Models;
using Swashbuckle.Swagger.Annotations;

namespace isc210_3_2018_2019_rpg_webapi.Controllers
{
    public class ScoresController : ApiController
    {
        Entities entities = new Entities();
        // GET api/scores
        [SwaggerOperation("GetAll")]
        public IEnumerable<string> Get()
        {
            return entities.Scores.Take(5).Select(s => s.PlayerName);
        }

        // GET api/scores/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public Scores Get(int id)
        {
            return entities.Scores.Where(s => s.IdScore == id).FirstOrDefault();
        }

        // POST api/scores
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public void Post([FromBody]Scores score)
        {
            Scores newScore = new Scores();
            newScore.IdScore = entities.Scores.OrderByDescending(s => s.IdScore).Take(1).FirstOrDefault().IdScore + 1;
            newScore.PlayerName = score.PlayerName;
            newScore.Score = score.Score;

            entities.Scores.Add(newScore);
            entities.SaveChanges();
        }

        // PUT api/scores/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public string Put(Scores score)
        {
            try
            {
                Scores newScore = new Scores();
                newScore.IdScore = entities.Scores.OrderByDescending(s => s.IdScore).Take(1).FirstOrDefault().IdScore + 1;
                newScore.PlayerName = score.PlayerName;
                newScore.Score = score.Score;

                entities.Scores.Add(newScore);
                entities.SaveChanges();

                return string.Empty;
            }
            catch (Exception ex)
            {
                return score.PlayerName + " // " + score.Score + " // " + ex.Message;
            }
            
        }

        // DELETE api/scores/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Delete(int id)
        {
            Scores oldScore = entities.Scores.Where(s => s.IdScore == id).FirstOrDefault();
            if (oldScore == null)
                return;

            entities.Scores.Remove(oldScore);
            entities.SaveChanges();
        }
    }
}
