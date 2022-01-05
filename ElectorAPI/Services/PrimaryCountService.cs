using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using ElectorAPI.Models;

namespace ElectorAPI.Services{
    public interface IPrimaryCountService{
        void parseJson();
        void setPrimaryWinners();
        PrimaryDataSet getPrimaryDataSet();
    }

    public class PrimaryCountService: IPrimaryCountService{
        List<Dictionary<string,Dictionary<string,Dictionary<string,Dictionary<string,int>>>>> rawData = new List<Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, int>>>>>();
        PrimaryDataSet data = new PrimaryDataSet();
        public void parseJson(){
            using (StreamReader r = new StreamReader("VotesTemplate.json")){ //using block automatically diposes stream
                string json = r.ReadToEnd();
                rawData = JsonConvert.DeserializeObject<List<Dictionary<string,Dictionary<string,Dictionary<string,Dictionary<string,int>>>>>>(json);
            }
        }

        public void setPrimaryWinners(){
            int state = 0;
            int county = 0;
            string countyName = "";
            foreach(Dictionary<string,Dictionary<string,Dictionary<string,Dictionary<string,int>>>> dataSet in rawData)
            foreach(KeyValuePair<string,Dictionary<string,Dictionary<string,Dictionary<string,int>>>> stateX in dataSet){
                var counties = stateX.Value;
                data.StateResults.Add(new StateResult(stateX.Key));
                List<CountyResult> partyResults = new List<CountyResult>();
                foreach(KeyValuePair<string,Dictionary<string,Dictionary<string,int>>> countyX in counties){
                    var parties = countyX.Value;
                    countyName = countyX.Key;
                    Dictionary<string,Candidate> winningCandidate = new Dictionary<string, Candidate>();
                    foreach(KeyValuePair<string,Dictionary<string,int>> partyX in parties){
                        var candidates = partyX.Value;
                        Candidate winner = getWinner(candidates,partyX.Key);
                        winningCandidate.Add(partyX.Key,winner);
                    }
                    CountyResult countyResult = new CountyResult(countyName,winningCandidate);
                    partyResults.Add(countyResult);
                }
                data.StateResults[state].CountyResults=partyResults;
                state++;
            }
        }

        public Candidate getWinner(Dictionary<string,int> candidates, string party){
            int voteCount =0;
            string name = "";
            foreach(KeyValuePair<string,int> candidateX in candidates){
                if(voteCount<candidateX.Value){
                    voteCount=candidateX.Value;
                    name = candidateX.Key;
                }
            }

            return new Candidate(name,voteCount);
        }

        public PrimaryDataSet getPrimaryDataSet(){
            return data;
        }
    }
}