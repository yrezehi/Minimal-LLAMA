using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLama.Layers
{
    public class ResidualConnectioncs
    {
        public static void Skip(float[] a, float[] b, int size)
        {
            for(int index = 0; index < size; index++)
            {
                a[index] += b[index];
            }
        }
    }
}
