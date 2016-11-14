using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FamilyManagement.Models
{
    public class ItemCategoryViewModel
    {
        [Display(Name ="Category Name")]
        public int CategoryId { get; set; }
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
        public IEnumerable<ItemCategory> ItemCategoryList { get; set; }
        public ItemCategory ItemCategoryView { get; set; }
    }
}