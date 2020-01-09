using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class CandidatePerSessionViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Prenume")]
        public string FirstName { get; set; }
        [Display(Name = "Nume")]
        public string LastName { get; set; }
        [Display(Name = "Partid")]
        public string PartyName { get; set; }
        [Display(Name = "Voturi")]
        public string VotePercentage { get; set; }
    }
}