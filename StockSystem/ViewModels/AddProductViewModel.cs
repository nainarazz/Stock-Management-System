using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using StockSystem.Models;

namespace StockSystem.ViewModels
{
    public class AddProductViewModel
    {
        public IEnumerable<Category> Category { get; set; }

        [Required]
        [System.Web.Mvc.Remote("CheckReferenceId", "Product", ErrorMessage = "Reference Id already exists.")]        
        [RegularExpression("^ref[0-9]{5,5}$", ErrorMessage = "Reference ID must follow pattern ref[5 digits].")]
        [Display(Name = "Reference")]
        public string ReferenceId { get; set; }

        [Display(Name = "Item Name")]
        [Required]
        [StringLength(255)]
        public string ItemName { get; set; }                

        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }

        [Display(Name = "Item Unit")]
        [Required]
        public string ItemUnit { get; set; }

        [Display(Name = "Unit Price")]
        [Required]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Stock Level")]
        [Required]        
        public int? StockLevel { get; set; }

        [Display(Name = "Shelf Location")]
        [Required]
        public string ShelfLocation { get; set; }

        [Display(Name = "Minimum Level")]
        [Required]
        [Range(1, 20)]
        public short? MinimumLevel { get; set; }
               
        public AddProductViewModel()
        {
            
        }

        public AddProductViewModel(Product Product)
        {
            ReferenceId = Product.ReferenceId;
            ItemName = Product.ItemName;
            ItemUnit = Product.ItemUnit;
            UnitPrice = Product.UnitPrice;
            CategoryId = Product.CategoryId;
            StockLevel = Product.StockLevel;
            ShelfLocation = Product.ShelfLocation;
            MinimumLevel = Product.MinimumLevel;
            
         
        }               

    }
}