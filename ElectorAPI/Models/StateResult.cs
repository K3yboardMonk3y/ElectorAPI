using System.Collections.Generic;

namespace ElectorAPI.Models{
    public class StateResult{
        private string name ="";
        public StateResult(string name){
            this.name = name;
        }

        public string Name {get{return name;}}

        public List<CountyResult> CountyResults {get;set;} = new List<CountyResult>();
    }

}