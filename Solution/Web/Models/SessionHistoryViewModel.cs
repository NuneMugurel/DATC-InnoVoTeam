using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SessionHistoryViewModel
    {
        [Display(Name = "Nume")]
        public string SessionName { get; set; }
        [Display(Name = "Data inceperii")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Data sfarsirii")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Castigator")]
        public string Winner { get; set; }
        [Display(Name = "Voturi")]
        public string WinnerVotesPercentage { get; set; }
        [Display(Name = "Numar de voturi")]
        public int NumVotes { get; set; }
    }
}