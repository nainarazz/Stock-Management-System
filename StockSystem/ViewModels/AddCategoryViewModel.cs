using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using StockSystem.Models;

namespace StockSystem.ViewModels
{
    public class AddCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [System.Web.Mvc.Remote("CheckCategoryName", "Category", ErrorMessage = "Category name already exists.")]
        [Display(Name = "Category Name")]
        public string CategoryName{ get; set; }        

        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }

        
        
    }
}