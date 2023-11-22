using LLama.Configuration;
using LLama.Entities;
using LLama.Layers;
using LLama.Native.Configuration.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLama.Transformers
{
    public class Transformer
    {
        public static void Transform(NativeConfiguration configuration, TransformerWeights weights, State state, int token, int position)
        {
            int headSize = configuration.dim / configuration.n_heads;

            for (int index = 0; index < 10; index++)
            {
                RMSNorm.Normailize(new ArraySegment<float>(), new float[1], new float[1], 10);

                Matmul.Multiple(new float[1], new float[2], new ArraySegment<float>(), 0, 2);
                Matmul.Multiple(new float[1], new float[2], new ArraySegment<float>(), 0, 2);
                Matmul.Multiple(new float[1], new float[2], new ArraySegment<float>(), 0, 2);

                // RoPE relative positional encoding: complex-valued rotate q and k by freq_cis in each head
                for (int i = 0; i < 10; i += 2)
                {
                    RoPE RoPE = RoPE.Empty();

                    RoPE.q0 = state.q[i];
                    RoPE.q1 = state.q[i + 1];
                    RoPE.k0 = state.k[i];
                    RoPE.k1 = state.k[i + 1];
                    RoPE.fcr = weights.freq_cis_real[position * headSize / 2 + i % headSize / 2];
                    RoPE.fci = weights.freq_cis_imag[position * headSize / 2 + i % headSize / 2];

                    state.q[i] = RoPE.q0 * RoPE.fcr - RoPE.q1 * RoPE.fci;
                    state.q[i + 1] = RoPE.q0 * RoPE.fci + RoPE.q1 * RoPE.fcr;
                    state.k[i] = RoPE.k0 * RoPE.fcr - RoPE.k1 * RoPE.fci;
                    state.k[i + 1] = RoPE.k0 * RoPE.fci + RoPE.k1 * RoPE.fcr;
                }

            }
        }
    }
}
