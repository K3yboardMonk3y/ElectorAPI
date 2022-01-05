using System.Collections.Generic;

namespace ElectorAPI.Models{
    public class CountyResult{
        private string name="";
        public CountyResult(string name, Dictionary<string,Candidate> parties){
            this.name=name;
            foreach(KeyValuePair<string,Candidate> party in parties){
                PartyResults.Add(party.Key,new PartyResult(party.Key,party.Value.Name,party.Value.Votes));
            }
        }

        public string Name {get{return name;}}

        public Dictionary<string, PartyResult> PartyResults {get;set;} = new Dictionary<string, PartyResult>();
    }
}