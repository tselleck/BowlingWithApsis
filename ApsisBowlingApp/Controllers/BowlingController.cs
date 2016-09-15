using ApsisBowlingApp.Models;
using ApsisBowlingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApsisBowlingApp.Controllers
{
    [RoutePrefix("api/bowling")]
    public class BowlingController : ApiController
    {

        private ScoreCalculator _scoreCalculator;

        public BowlingController()
        {
            _scoreCalculator = new ScoreCalculator();
        }

        /// <summary>
        /// api/bowling/getscore
        /// Calculates all frames and setting a score to them and also returning a totalscore
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [Route("getscore")]
        public IHttpActionResult PostScore(ScoreViewModel vm)
        {
            return Ok(_scoreCalculator.Calculate(vm));
        }
    }
}
