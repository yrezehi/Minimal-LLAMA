using LLama.Entities;
using LLama.Layers;
using LLama.Native.Configuration.Native;

namespace LLama.Transformers
{
    public class Transformer
    {
        public static void Transform(ConfigurationLoader configuration, TransformerWeights weights, State state, int token, int position)
        {
            int headSize = configuration.dim / configuration.n_heads;

            Array.Copy(weights.token_embedding_table, token * configuration.dim, state.x, 0, configuration.dim);

            for (int index = 0; index < configuration.n_layers; index++)
            {
                RMSNorm.Normailize(weights.rms_att_weight[(index * configuration.dim)..], state.xb, state.x, configuration.dim);

                Matmul.Multiple(state.q, state.xb, weights.wq[(index * configuration.dim * configuration.dim)..], configuration.dim, configuration.dim);
                Matmul.Multiple(state.k, state.xb, weights.wk[(index * configuration.dim * configuration.dim)..], configuration.dim, configuration.dim);
                Matmul.Multiple(state.q, state.xb, weights.wv[(index * configuration.dim * configuration.dim)..], configuration.dim, configuration.dim);

                // RoPE relative positional encoding: complex-valued rotate q and k by freq_cis in each head
                for (int currentIndex = 0; currentIndex < configuration.n_layers; currentIndex += 2)
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

                        score /= (float) Math.Sqrt(headSize);

                        state.att[currentIndex + attOffset] = score;
                    }

                    Softmax.Normailize(state.att, attOffset, position + 1);

                    int xbOffset = head * headSize;

                    for (int currentIndex = xbOffset; currentIndex < xbOffset + headSize; currentIndex++)
                        state.xb[currentIndex] = 0f;

                    for(int currentPosition = 0; currentPosition <= position; currentPosition++)
                    {
                        int vOffset = loff + currentPosition * configuration.dim + head * headSize;

                        float attention = state.att[currentPosition + attOffset];

                        for (int position = 0; position < headSize; position++)
                        {
                            state.xb[position + xbOffset] += attention * state.value_cache[position + vOffset];
                        }
                    }
                });

                Matmul.Multiple(state.xb2, state.xb, weights.wo[(index * configuration.dim * configuration.dim)..], configuration.dim, configuration.dim);

                ResidualConnectioncs.Skip(state.x, state.xb2, configuration.dim);

                RMSNorm.Normailize(weights.rms_ffn_weight[(index * configuration.dim)..], state.xb, state.x, configuration.dim);

                
                Matmul.Multiple(state.hb, state.xb, weights.w1[(index * configuration.dim * configuration.hidden_dim)..], configuration.dim, configuration.hidden_dim);
                Matmul.Multiple(state.hb2, state.xb, weights.w3[(index * configuration.dim * configuration.hidden_dim)..], configuration.dim, configuration.hidden_dim);

                for(int currentIndex = 0; currentIndex < configuration.hidden_dim; currentIndex++)
                {
                    state.hb[currentIndex] *= (1.0f / (1.0f + (float)Math.Exp(-state.hb[currentIndex])));
                }

                for (int currentIndex = 0; currentIndex < configuration.hidden_dim; currentIndex++)
                {
                    state.hb[currentIndex] *= state.hb2[currentIndex];
                }

                Matmul.Multiple(state.xb, state.hb, weights.w2[(index * configuration.dim * configuration.hidden_dim)..], configuration.hidden_dim, configuration.dim);

                ResidualConnectioncs.Skip(state.x, state.xb, configuration.dim);
            }

            RMSNorm.Normailize(state.x, state.x, weights.rms_final_weight, configuration.dim);

            Matmul.Multiple(state.logits, state.x, weights.wcls, configuration.dim, configuration.vocab_size);
        }
    }
}
