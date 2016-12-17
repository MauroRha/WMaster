namespace WMaster.Tool
{
    using System;

    /// <summary>
    /// Randomize class
    /// </summary>
    public static class WMRandom
    {
        private static Random _instance;
        private static Random Instance
        {
            get
            {
                if (WMRandom._instance == null)
                { WMRandom._instance = new Random(Guid.NewGuid().GetHashCode()); }
                return WMRandom._instance;
            }
        }

        /// <summary>
        /// Get next random value between 0 include to <paramref name="upperLimit"/> include.
        /// <remarks><para>By default <paramref name="upperLimit"/> is 100 representing percentage.</para></remarks>
        /// </summary>
        /// <param name="upperLimit">Maximum value + 1 of random result.</param>
        /// <returns>Random value between 0 include to <paramref name="upperLimit"/> include.</returns>
        public static int Next(int upperLimit = 100)
        {
            return WMRandom.Instance.Next(upperLimit + 1);
        }

        /// <summary>
        /// Get next random value between 0 include to <paramref name="upperLimit"/> include.
        /// </summary>
        /// <param name="upperLimit">Maximum value + 1 of random result.</param>
        /// <returns>Random value between 0 include to <paramref name="upperLimit"/> include.</returns>
        public static double Next(double upperLimit)
        {
            return WMRandom.Instance.NextDouble() * (upperLimit + 1);
        }

        /// <summary>
        /// Get next random value between <paramref name="lowerLimit"/> include to <paramref name="upperLimit"/> include.
        /// </summary>
        /// <param name="lowerLimit">Minimum value for fandom number.</param>
        /// <param name="upperLimit">Maximum value for fandom number.</param>
        /// <returns>Random value between <paramref name="lowerLimit"/> include to <paramref name="upperLimit"/> include. </returns>
        public static int Next(int lowerLimit, int upperLimit)
        {
            return WMRandom.Instance.Next(lowerLimit, upperLimit + 1);
        }

        /// <summary>
        /// Get next random value between 0 to 100 representing a percentage.
        /// </summary>
        /// <param name="upperLimit">Maximum value + 1 of random result.</param>
        /// <returns>Random value between 0 to 100.</returns>
        public static double Percentage()
        {
            return (double)WMRandom.Instance.Next(0, 101) / 100;
        }

        /// <summary>
        /// Returns true luck percent of the time.
        /// <remarks><para>WMRandom.Percent(20) will return true 20% of the time.</para></remarks>
        /// <param name="luck">Maximum value + 1 of random result.</param>
        /// <returns><b>True</b> luck% of the time.</returns>
        public static bool Percent(int luck)
        {
            return (1 + WMRandom.Instance.Next(100)) < luck;
        }

        /// <summary>
        /// Returns true luck percent of the time.
        /// <remarks><para>WMRandom.Percent(20.005) will return true 20.005% of the time.</para></remarks>
        /// <remarks><para>`J` added percent allowing double input up to 3 decimal</para></remarks>
        /// </summary>
        /// <param name="luck">Maximum value + 1 of random result.</param>
        /// <returns><b>True</b> luck% of the time.</returns>
        public static bool Percent(double luck)
        {
            return (1 + WMRandom.Instance.Next(100000)) < (luck * 1000.0);
        }

        // <summary>
        // Returns a number randomly distributed between min and max.
        // <remarks><para>if min > max, then returns number in the range 0 to 100 in order to replicate how the girl stat generation works.</para></remarks>
        // </summary>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <param name="range">if min > max, then returns number in the range 0 to <paramref name="range"/></param>
        /// <returns>Number randomly distributed between min and max</returns>
        public static int InRange(int min, int max, int range = 100)
        {
            int diff = max - min;
            if (min < 0 && max > 0)
            { diff++; }

            if (diff == 0)
            { return max; }
            if (diff < 0)
            { return WMRandom.Instance.Next(range + 1); }

            return min + WMRandom.Instance.Next(diff);
        }

        /// <summary>
        /// TODO : Commentaire to add
        /// <remarks><para>`J` trying to add a bell curve</para></remarks>
        /// <remarks><para>`J` added - not sure how well it will work, I'm not too good at math</para></remarks>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Bell(int min, int max)
        {
            double bmin, bmax, bmid, blow, bhii, test;

            if (min == max)
            { return max; }

            bmin = Math.Min(min, max);
            bmax = Math.Max(min, max);

            bmax++; // to correct random+1
            if (bmin < 0)
            { bmin--; }

            bmid = (bmin + bmax) / 2.0;
            blow = bmid - bmin;
            bhii = bmax - bmid;
            test = WMRandom.InRange((int)bmin, (int)bmax);

            if (test < bmid)
            { test += WMRandom.Next(bhii); }
            else if (test >= bmid)
            { test -= WMRandom.Next(blow); }

            if (test < min)
            { return min; }
            if (test > max)
            { return max; }
            return (int)Math.Round(test);
        }
    }
}
