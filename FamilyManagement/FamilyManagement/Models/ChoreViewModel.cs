using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilyManagement.Models
{
    public class ChoreViewModel
    {
        public string Chores { get; set; }
        public IEnumerable<Chore> ChoreList { get; set; }
        public Chore ChoreView { get; set; }

    }
}