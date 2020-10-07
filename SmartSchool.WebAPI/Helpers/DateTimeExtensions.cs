using System;

namespace SmartSchool.WebAPI.Helpers
{
    public static class DateTimeExtensions
    {
        public static int GetCurrentAge(this DateTime date)
        {
            var dateCurrent = DateTime.UtcNow;
            int age = dateCurrent.Year - date.Year;

            if (dateCurrent < date.AddYears(age))
                age --;

            return age;
        }
    }
}