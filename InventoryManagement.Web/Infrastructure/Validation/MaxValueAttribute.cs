using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventoryManagement.Web.Infrastructure.Validation
{
    public class MaxValueAttribute : ValidationAttribute
    {
        private readonly int _maxValue;

        public MaxValueAttribute(int maxValue)
        {
            _maxValue = maxValue;
        }

        public override bool IsValid(object value)
        {
            return (int)value <= _maxValue;
        }
    }
}