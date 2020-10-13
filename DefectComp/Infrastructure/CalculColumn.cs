using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Infrastructure
{
    public static class CalculColumn
    {
        // на входе дата, считаем сколько дней прошло с указанной даты в днях и месяцах
        public static string GetPeriodDays(DateTime ? date)
        {
            string retVal = "-";
            if (date != null)
            {
                var dd = DateTime.Today.Subtract((DateTime)date).Days;
                int mm = dd / 30;
                retVal = dd.ToString() + " дн.";
                if (mm != 0)
                {
                    retVal += "~" + mm.ToString() + " м.";
                }
            }
            return retVal;
        }
    }
}
