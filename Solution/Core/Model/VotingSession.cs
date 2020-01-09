using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    [DataContract(IsReference = true)]
    public class VotingSession
    {
        public VotingSession()
        {
            Candidates = new HashSet<Candidate>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public ICollection<Candidate> Candidates { get; set; }
    }
}
