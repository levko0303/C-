using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lab5.Models
{
    public class Validation
    {
        public class PhoneAttribute : ValidationAttribute
        {
            public override bool IsValid(object Value)
            {
                string value = Value as string;

                if ((!value.StartsWith("+380") || value.Length != 13) || (value.Any(char.IsLetter)))
                {
                    ErrorMessage = "The format of phone is incorrect";
                    return false;
                }
                else
                    return true; ;
            }
        }

        public class NameAttribute : ValidationAttribute
        {
            public override bool IsValid(object Value)
            {
                string value = Value as string;
                if (value.Any(char.IsDigit))
                {
                    ErrorMessage = "Name is incorrect";
                    return false;
                }
                return true;
            }
        }

        public class IbanAttribute : ValidationAttribute
        {
            public override bool IsValid(object Value)
            {
                string value = Value as string;
                bool check1 = true;
                bool check2 = true;
                string code = value[0..2];
                string digit = value[2..];
                if (value.Length == 29)
                {
                    foreach (char ch in code)
                    {
                        if (!char.IsLetter(ch))
                            check1 = false;
                    }

                    foreach (char ch in digit)
                    {
                        if (!char.IsNumber(ch))
                            check2 = false;
                    }
                }
                else
                    check1 = check2 = false;
                if (check1 == false || check2 == false)
                {
                    ErrorMessage = "Iban is incorrect";
                    return false;
                }
                return true;
            }
        }

        public class DateAttribute : ValidationAttribute
        {
            public override bool IsValid(object Value)
            {
                string value = Value as string;
                try
                {
                    DateTime v = DateTime.Parse(value);
                    return true;
                }

                catch (Exception e)
                {
                    ErrorMessage = e.Message;
                    return false;
                }
            }
        }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public class DateTimeNotLessThan : ValidationAttribute, IClientValidatable
        {
            private const string DefaultErrorMessage = "{0} can't be less than {1}";

            public string OtherProperty { get; private set; }
            private string OtherPropertyName { get; set; }

            public DateTimeNotLessThan(string otherProperty, string otherPropertyName)
                : base(DefaultErrorMessage)
            {
                if (string.IsNullOrEmpty(otherProperty))
                {
                    throw new ArgumentNullException("otherProperty");
                }

                OtherProperty = otherProperty;
                OtherPropertyName = otherPropertyName;
            }

            public override string FormatErrorMessage(string name)
            {
                return string.Format(ErrorMessageString, name, OtherPropertyName);
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                try
                {

                    if (value != null)
                    {
                        var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(OtherProperty);
                        var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

                        DateTime dtThis = Convert.ToDateTime(value);
                        DateTime dtOther = Convert.ToDateTime(otherPropertyValue);

                        if (dtThis < dtOther)
                        {
                            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                        }
                    }
                    return ValidationResult.Success;
                }
                catch
                {
                    return new ValidationResult("Can't compare dates. There is a mistake somewhere");
                }
            }

            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
                                                          ModelMetadata metadata,
                                                          ControllerContext context)
            {
                var clientValidationRule = new ModelClientValidationRule()
                {
                    ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                    ValidationType = "notlessthan"//
                };

                clientValidationRule.ValidationParameters.Add("otherproperty", OtherProperty);

                return new[] { clientValidationRule };
            }
        }
        public class EmailAttribute : ValidationAttribute
        {
            public override bool IsValid(object Value)
            {
                string value = Value as string;
                bool check = System.Text.RegularExpressions.Regex.IsMatch(value, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                if (!check)
                {
                    ErrorMessage = "Wrong email";
                    return false;
                }
                return true;
            }

        }
    }
}
