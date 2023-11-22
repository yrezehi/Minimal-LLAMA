using LLama.Entities;
using LLama.Memory;
using LLama.Native.Configuration.Native;

namespace LLama.Models
{
    public static class InitialWeights
    {
        public static void Initialize(this MemoryFile file, ref NativeConfiguration configuration, ref TransformerWeights weights, bool shareWeights)
        {
            long offset = 0;

            weights.token_embedding_table = file.Read(ref offset, configuration.vocab_size * configuration.dim);
            weights.rms_att_weight = file.Read(ref offset, configuration.n_layers *  configuration.dim);
            
            weights.wq = file.Read(ref offset, configuration.n_layers * configuration.dim * configuration.dim);
            weights.wk = file.Read(ref offset, configuration.n_layers * configuration.dim * configuration.dim);
            weights.wv = file.Read(ref offset, configuration.n_layers * configuration.dim * configuration.dim);
            weights.wo = file.Read(ref offset, configuration.n_layers * configuration.dim * configuration.dim);
            
            weights.rms_ffn_weight = file.Read(ref offset, configuration.n_layers * configuration.dim * configuration.dim);
           
            weights.w1 = file.Read(ref offset, configuration.n_layers * configuration.dim * configuration.hidden_dim);
            weights.w2 = file.Read(ref offset, configuration.n_layers * configuration.hidden_dim * configuration.dim);
            weights.w3 = file.Read(ref offset, configuration.n_layers * configuration.dim * configuration.hidden_dim);
            
            weights.rms_final_weight = file.Read(ref offset, configuration.dim);

            weights.freq_cis_real = file.Read(ref offset, configuration.seq_len * (configuration.dim/configuration.n_heads) / 2);
            weights.freq_cis_imag = file.Read(ref offset, configuration.seq_len * (configuration.dim / configuration.n_heads) / 2);

            if (shareWeights)
            {
                weights.wcls = weights.token_embedding_table;
            }
        }
    }
}
