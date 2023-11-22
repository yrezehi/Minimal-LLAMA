using LLama.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLama.Extensions
{
    public static class ProbabilitiesIndexExtensions
    {
        public static int ProbabilitiesComparer(ProbabilitiesIndex firstIndex, ProbabilitiesIndex secondIndex)
        {
            if (firstIndex.Probability > secondIndex.Probability)
                return -1;
            else if (firstIndex.Probability < secondIndex.Probability)
                return 1;
            return 0;
        }
    }
}
