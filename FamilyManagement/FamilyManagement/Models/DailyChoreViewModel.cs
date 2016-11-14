using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FamilyManagement.Models
{
    public class DailyChoreViewModel
    {
        public string Chores { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public double Amount { get; set; }
        public int FamilyId { get; set; }
        public int ChoreId { get; set; }
        [Display(Name = "Day")]

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ChoreDay { get; set; }
        public bool Done { get; set; }
        public IEnumerable<Family> familydetail { get; set; }
        public IEnumerable<Chore> choredetail { get; set; }
        public IEnumerable<DailyChore> dailychoredetail { get; set; }

        public DailyChore DailyChoreView { get; set; }

    }
}