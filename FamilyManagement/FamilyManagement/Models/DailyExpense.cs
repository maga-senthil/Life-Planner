using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FamilyManagement.Models
{
    public class DailyExpense
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Day { get; set; }

        [ForeignKey("ItemCategorys")]
        public int ItemCategoryId { get; set; }
        public ItemCategory ItemCategorys { get; set; }
        public float Amount { get; set; }
    }
}