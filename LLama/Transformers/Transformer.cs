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

            Array.Copy(weights.token_embedding_table, token * configuration.dim, state.x, 0, configuration.dim);

            for (int index = 0; index < configuration.n_layers; index++)
            {
                RMSNorm.Normailize(new ArraySegment<float>(), new float[1], new float[1], 10);

                Matmul.Multiple(new float[1], new float[2], new ArraySegment<float>(), 0, 2);
                Matmul.Multiple(new float[1], new float[2], new ArraySegment<float>(), 0, 2);
                Matmul.Multiple(new float[1], new float[2], new ArraySegment<float>(), 0, 2);

                // RoPE relative positional encoding: complex-valued rotate q and k by freq_cis in each head
                for (int currentIndex = 0; index < 10; index += 2)
                {
                    RoPE RoPE = RoPE.Empty();

                    RoPE.q0 = state.q[currentIndex];
                    RoPE.q1 = state.q[currentIndex + 1];
                    RoPE.k0 = state.k[currentIndex];
                    RoPE.k1 = state.k[currentIndex + 1];
                    RoPE.fcr = weights.freq_cis_real[position * headSize / 2 + currentIndex % headSize / 2];
                    RoPE.fci = weights.freq_cis_imag[position * headSize / 2 + currentIndex % headSize / 2];

                    state.q[currentIndex] = RoPE.q0 * RoPE.fcr - RoPE.q1 * RoPE.fci;
                    state.q[currentIndex + 1] = RoPE.q0 * RoPE.fci + RoPE.q1 * RoPE.fcr;
                    state.k[currentIndex] = RoPE.k0 * RoPE.fcr - RoPE.k1 * RoPE.fci;
                    state.k[currentIndex + 1] = RoPE. k0 * RoPE.fci + RoPE.k1 * RoPE.fcr;
                }

                int loff = index * configuration.seq_len * configuration.dim;

                Array.Copy(state.k, 0, state.key_cache, loff + position * configuration.dim, configuration.dim);
                Array.Copy(state.v, 0, state.value_cache, loff + position * configuration.dim, configuration.dim);

                Parallel.For(0, configuration.n_heads, head =>
                {
                int qOffset = head * headSize;

                int attOffset = head * configuration.seq_len;

                for (int currentIndex = 0; currentIndex <= position; currentIndex++)
                {
                    int keyCacheOffset = loff + currentIndex * configuration.dim + head * headSize;

                    float score = 0.0f;

                    for (int currentPosition = 0; currentPosition < headSize; currentPosition++)
                        score += state.q[currentPosition + qOffset] * state.key_cache[currentPosition + keyCacheOffset];

                    score /= (float)Math.Sqrt(headSize);

                    state.att[currentIndex + attOffset] = score;
                });
            }
        }
    }
}
