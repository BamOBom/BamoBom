using System;
using System.Collections.Generic;
using System.Text;

namespace Common.PersianDateTime
{
    public static class ToPersianDateTime
    {
        public static DateTime GetPersianDateTime(DateTime value)
        {
            var persianDateTime1 = new PersianDateTime(value);
            return persianDateTime1;
        }
       

    }
    
}
