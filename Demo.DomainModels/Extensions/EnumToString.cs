using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DomainModels.Extensions
{
    public static class EnumToString
    {
        public static String ConvertToString(this Enum eff)
        {
            return Enum.GetName(eff.GetType(), eff);
        }
    }
}
