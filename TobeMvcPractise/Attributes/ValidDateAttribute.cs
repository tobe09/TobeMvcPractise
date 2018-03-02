using System;
using System.ComponentModel.DataAnnotations;

namespace TobeMvcPractise.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class ValidDateAttribute : ValidationAttribute
    {
        private DateTime? _minDate, _maxDate;

        public ValidDateAttribute(string minDate, string maxDate = null) : base()
        {
            _minDate = ConvertStringToDate(minDate);
            _maxDate = ConvertStringToDate(maxDate) ?? DateTime.Now.Date;
        }

        private DateTime? ConvertStringToDate(string dateStr)
        {
            try
            {
                string[] dateValues = dateStr.Split('/', '-', ' ', ':');
                int day = int.Parse(dateValues[0]);
                int month = int.Parse(dateValues[1]);
                int year = int.Parse(dateValues[2]);

                return new DateTime(year, month, day);
            }
            catch
            {
                return null;
            }
        }

        public override bool IsValid(object value)
        {
            DateTime val = (DateTime)value;
            return val >= _minDate && val <= _maxDate;
        }
    }
}