using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Data.Model
{
    [DataContract(IsReference = true)]
    public class Candidate
    {
        public Candidate()
        {
            VotingSessions = new HashSet<VotingSession>();
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string PartyName { get; set; }
        [DataMember]
        public ICollection<VotingSession> VotingSessions { get; set; }
    }
}
