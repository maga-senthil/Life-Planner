using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilyManagement.Models
{
    public class FamilyViewModel
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public double Amount { get; set; }
        public IEnumerable<Family> FamilyList { get; set; }
        public Family  FamilyView { get; set; }
    }
}