using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SpaParadise.Models
{

    public class ExcludeChar : ValidationAttribute

    {

        private readonly string _chars;


        public ExcludeChar(string chars) : base("{0} @!#/. are invalid character.")




        {

            _chars = chars;

        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)

        {

            if (value != null)

            {

                for (int i = 0; i < _chars.Length; i++)

                {

                    var valueAsString = value.ToString();

                    if (valueAsString.Contains(_chars[i]))

                    {

                        var errorMessage = FormatErrorMessage(validationContext.DisplayName);

                        return new ValidationResult(errorMessage);

                    }

                }

            }

            return ValidationResult.Success;

        }
    }
}