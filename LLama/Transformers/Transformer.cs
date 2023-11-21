using LLama.Configuration;
using LLama.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLama.Transformers
{
    public class Transformer
    {
        public static void Transform(int token, int position)
        {

            for (int index = 0; index < 10; index++)
            {
                RMSNorm.Normailize(new ArraySegment<float>(), new float[1], new float[1], 10);

                Matmul.Multiple(new float[1], new float[2], new ArraySegment<float>(), 0, 2);
                Matmul.Multiple(new float[1], new float[2], new ArraySegment<float>(), 0, 2);
                Matmul.Multiple(new float[1], new float[2], new ArraySegment<float>(), 0, 2);

                // RoPE relative positional encoding: complex-valued rotate q and k by freq_cis in each head
                for (int i = 0; i < 10; i += 2)
                {
          
                }

            }
        }
    }
}
