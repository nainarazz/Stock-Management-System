﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using StockSystem.ViewModels;

namespace StockSystem.Models
{
    public class Category
    {        
        public int Id { get; set; }

        [Required]
        [CheckCategoryName]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }        

        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }
        
    }
}