using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using StockSystem.Models;

namespace StockSystem.ViewModels
{
    public class AddCategoryViewModel
    {        
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName{ get; set; }        
        
        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }

        public static string tempCategoryName = "";

        public AddCategoryViewModel()
        {
            Id = 0;
        }

        public AddCategoryViewModel(Category category)
        {
            Id = category.Id;
            CategoryName = category.CategoryName;
            CategoryDescription = category.CategoryDescription;
        }
        
    }
}