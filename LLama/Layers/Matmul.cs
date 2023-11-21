using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLama.Layers
{
    // Matrix multiplication
    public class Matmul
    {
        public static void Multiple(float[] xout, float[] x, ArraySegment<float> w, int n, int d)
        {
            Parallel.For(0, d, i =>
            {
                float val = 0.0f;
                for (int j = 0; j < n; j++) val += w[i * n + j] * x[j];
                xout[i] = val;
            });
        }
    }
}
