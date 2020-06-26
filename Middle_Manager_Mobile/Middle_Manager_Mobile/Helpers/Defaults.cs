using System;
using System.Collections.Generic;
using System.Text;

namespace Middle_Manager_Mobile.Helpers
{
    public static class Defaults
    {
        public static DateTime DefaultDOB => new DateTime(2000, 01, 01);

        public static DateTime DefaultTomorrow => DateTime.UtcNow.AddDays(1);

        public static DateTime NextMonth => DateTime.UtcNow.AddMonths(1);

    }
}
