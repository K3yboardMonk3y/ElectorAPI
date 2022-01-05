using System.Collections.Generic;

namespace ElectorAPI.Models{
    public class Candidate{
        private string name = "";
        private int votes = 0;
        public Candidate(string name, int votes){
            this.name = name;
            this.votes = votes;
        }

        public string Name{get{return name;}}
        public int Votes{get{return votes;}}
        public string Party {get;set;}
        public string County {get;set;}
        public string State {get;set;}

        public override string ToString()
        {
            return $"{Name} wins the {Party} primary in {State},{County}!";
        }
    }
}