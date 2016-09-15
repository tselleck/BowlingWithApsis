using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApsisBowlingApp.Models
{
    public class FrameViewModel
    {
        public int? First { get; set; }
        public int? Second { get; set; }
        public int? Third { get; set; }
        public int? Score { get; set; }

        /// <summary>
        /// Calculate the sum of First, Second an Third
        /// </summary>
        /// <returns>Number</returns>
        public int Sum()
        {
            var sum = 0;
            if (First.HasValue)
            {
                sum = First.Value;
            }
            if (Second.HasValue)
            {
                sum += Second.Value;
            }
            if (Third.HasValue)
            {
                sum += Third.Value;
            }

            return sum;
        }
    }
}