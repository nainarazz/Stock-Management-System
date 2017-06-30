using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using StockSystem.ViewModels;

namespace StockSystem.Models
{
    public class CheckCategoryName : ValidationAttribute
    {
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {            
            var category = (Category)validationContext.ObjectInstance;

            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                var categoryNameExistsInDb = _context.Category.Any(c => c.CategoryName == category.CategoryName);

                if(category.Id == 0)
                {
                    return categoryNameExistsInDb
                   ? new ValidationResult("This name already exists.")
                   : ValidationResult.Success;
                }

                else
                {                                       
                    return ValidationResult.Success;
                }
                

            }




        }
    }
}