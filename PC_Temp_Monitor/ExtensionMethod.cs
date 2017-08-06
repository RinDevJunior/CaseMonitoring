using System;
using System.Collections.Generic;
using System.Linq;
using OpenHardwareMonitor.Hardware;

namespace PC_Temp_Monitor
{
    public static class ExtensionMethod
    {
        public static bool SelectWhere<T>(this IList<Tuple<SensorType, object>> dictionary,
            Func<Tuple<SensorType, object>, bool> condition,
            Func<Tuple<SensorType, object>, object> selector,
            out IList<T> output)
        {
            if (dictionary.Any(condition))
            {
                output = dictionary.Where(condition).Select(selector).Cast<T>()
                    .ToList();
                return true;
            }
            output = null;
            return false;
        }
    }
}