using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FamilyManagement.Models
{
    public class MonthlyBudgetViewModel
    {
        [Display(Name = "Category Name")]
        public int CategoryId { get; set; }
        public Category Categorys { get; set; }
        [Display(Name = "Month")]
        public string BudgetMonth { get; set; }
        [Display(Name = "Budget Amount")]
        public float BudgetAmount { get; set; }
        [Display(Name = "Actual Amount")]
        public float ActualAmount { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
        public IEnumerable<MonthlyBudget> BudgetList { get; set; }
        public MonthlyBudget MonthlyBudgetView { get; set; }
    }
}