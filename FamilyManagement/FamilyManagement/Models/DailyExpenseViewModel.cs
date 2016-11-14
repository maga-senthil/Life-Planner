using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FamilyManagement.Models
{
    public class DailyExpenseViewModel
    {
        [Display(Name = "Item")]
        public string ItemName { get; set; }
        [Display(Name = "Month")]
        public string BudgetMonth { get; set; }
        [Display(Name = "Budget Amount")]
        public float BudgetAmount { get; set; }
        [Display(Name = "Actual Amount")]
        public float ActualAmount { get; set; }
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Day { get; set; }
        public int CategoryId { get; set; }

        public int ItemCategoryId { get; set; }
        public float Amount { get; set; }
        public float Difference { get; set; }
        [Display(Name = "Total")]
        public float TotalBudget { get; set; }
        public float TotalActual { get; set; }
        public float TotalDifference { get; set; }
        public IEnumerable<MonthlyBudget> budgetdetail { get; set; }
        public IEnumerable<DailyExpense> dailyexpensedetail { get; set; }
        public DailyExpense DailyExpenseView { get; set; }
    }
}