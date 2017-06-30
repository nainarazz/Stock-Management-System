using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using StockSystem.ViewModels;


namespace StockSystem.Models
{
    public class ReferenceExists: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {          
            var product = (Product)validationContext.ObjectInstance;
            
            using(ApplicationDbContext _context = new ApplicationDbContext())
            {                
                return _context.Product.Any(p => p.ReferenceId == product.ReferenceId)
                  ? new ValidationResult("This reference ID already exists.")
                  : ValidationResult.Success;                             
                
            }

            
            
            
        }
        
    }
}