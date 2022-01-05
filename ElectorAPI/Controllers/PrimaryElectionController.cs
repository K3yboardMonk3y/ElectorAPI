using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ElectorAPI.Services;
using ElectorAPI.Models;

namespace ElectorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrimaryElectionController : ControllerBase
    {

        private readonly ILogger<PrimaryElectionController> _logger;
        private readonly IPrimaryCountService _primaryCountService;

        public PrimaryElectionController(ILogger<PrimaryElectionController> logger, IPrimaryCountService primaryCountService)
        {
            _logger = logger;
            _primaryCountService = primaryCountService;
        }

        [HttpGet]
        public IEnumerable<Candidate> Get()
        {
            _primaryCountService.parseJson();
            _primaryCountService.setPrimaryWinners();
            List<Candidate> winners = new List<Candidate>();

            PrimaryDataSet data = _primaryCountService.getPrimaryDataSet();
            foreach(StateResult state in data.StateResults){
                foreach(CountyResult county in state.CountyResults){
                    foreach(KeyValuePair<string,PartyResult> result in county.PartyResults){
                        Candidate winner = result.Value.WinningCandidate;
                        winner.State = state.Name;
                        winner.County=county.Name;
                        winner.Party = result.Key;
                        winners.Add(winner);
                    }
                }
            }
            return winners;
        }
    }
}
