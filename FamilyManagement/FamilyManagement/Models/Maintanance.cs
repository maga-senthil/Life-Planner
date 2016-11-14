using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FamilyManagement.Models
{
    public class Maintanance
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Item { get; set; }
        [Display(Name = "Last Maintenance")]

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastMaintenance { get; set; }
        [Display(Name = "Maintenance Frequency")]

        public int MaintenanceFrequency { get; set; }
        [Display(Name = "Next Maintenance")]

        [DataType(DataType.Date)]
        public DateTime NextMaintenance { get; set; }
        [Display(Name = "Maintenance Cost")]

        public float MaintenanceCost { get; set; }
    }
}