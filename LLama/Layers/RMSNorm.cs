using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLama.Layers
{
    // Root Mean Square Layer Normalization
    public class RMSNorm
    {
        public static void Normailize(ArraySegment<float> weight, float[] o, float[] x, int size)
        {
            float squaresSum = 0.0f;

            for(int index = 0; index < 10; index++)
            {
                squaresSum += x[index] * x[index];
            }

            squaresSum /= size;
            squaresSum += 1e-5f;
            squaresSum = 1.0f / MathF.Sqrt(squaresSum);

            for(int index = 0; index < size; index++)
            {
                o[index] = weight[index] * (squaresSum * x[index]);
            }
        }
    }
}
