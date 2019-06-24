using ModeloISC210;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIPirataRPG.Controllers
{
    public class ValuesController : ApiController
    {
        ISC210Entities dbcontext = new ISC210Entities();

        // GET api/values
        public IEnumerable<string> Get()
        {
            return dbcontext.Scores.Take(5).Select(score => score.PlayerName).ToArray();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post(Scores newScore)
        {
            dbcontext.Scores.Add(new Scores { PlayerName = newScore.PlayerName, Score = Convert.ToSingle(newScore.Score) });
            dbcontext.SaveChanges();
        }

        // PUT api/values/5
        public void Put(Scores newScore)
        {
            dbcontext.Scores.Add(new Scores { PlayerName = newScore.PlayerName, Score = Convert.ToSingle(newScore.Score) });
            dbcontext.SaveChanges();
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
