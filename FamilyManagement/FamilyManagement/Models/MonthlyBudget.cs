using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FamilyManagement.Models
{
    public class MonthlyBudget
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Categorys")]
        public int CategoryId { get; set; }
        public Category Categorys { get; set; }
        public string BudgetMonth { get; set; }
        public float BudgetAmount { get; set; }
        public float ActualAmount { get; set; }
        public float Difference { get; set; }
    }
}