using System;
using System.Collections.Generic;
using System.Text;

namespace Web.API.Core.Helpers.Utility
{
    public static class Utility
    {
        internal static decimal ToDecimal(this string strValue)
        {
            decimal d;
            if (decimal.TryParse(strValue, out d))
                return d;
            return 0;

        }

    }
}
