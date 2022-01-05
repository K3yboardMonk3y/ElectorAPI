using System.Collections.Generic;

namespace ElectorAPI.Models{
    public class PrimaryDataSet{
        public PrimaryDataSet(){

        }

        public List<StateResult> StateResults {get;set;} = new List<StateResult>();
    }
}