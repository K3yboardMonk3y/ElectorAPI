using System.Collections.Generic;

namespace ElectorAPI.Models{
    public class PartyResult{
        private string name ="";
        public PartyResult(string name, string candidateName, int candidateVotes){
            this.name = name;
            WinningCandidate = new Candidate(candidateName,candidateVotes);
        }

        public string Name{get{return name;}}

        public Candidate WinningCandidate{get;set;}
    }
}