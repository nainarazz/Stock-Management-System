using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StockSystem.Models
{
    public class Product
    {
        [Key]
        [Required]
        [RegularExpression("^ref[0-9]{5,5}$", ErrorMessage = "Reference ID must follow pattern ref[5 digits].")]       
        [Display(Name = "Reference")]
        public string ReferenceId { get; set; }
        
        [Display(Name = "Item Name")]
        [Required]
        [StringLength(255)]
        public string ItemName { get; set; }
        
        public Category Category { get; set; }

        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }

        [Display(Name = "Item Unit")]
        [Required]
        public string ItemUnit { get; set; }

        [Display(Name = "Unit Price")]
        [Required]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Stock Level")]
        [Required]
        [Range(1, 9999)]
        public int StockLevel { get; set; }

        [Display(Name = "Shelf Location")]
        [Required]
        public string ShelfLocation { get; set; }

        [Display(Name = "Minimum Level")]
        [Required]
        [Range(1, 20)]
        public short MinimumLevel { get; set; }

    }
}