using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FamilyManagement.Models
{
    public class CategoryViewModel
    {
        [Display(Name = "Category Name")]

        public string CategoryName { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
        public Category CategoryView { get; set; }
    }
}