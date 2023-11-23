using LLama.Configuration;
using LLama.Entities;
using System.Runtime.InteropServices;

namespace LLama.Extensions
{
    public static class StateExtensions
    {
        public static State Populate(this State state, BinConfiguration configuration)
        {
            state.x = new float[configuration.dim];
            state.xb = new float[configuration.dim];
            state.xb2 = new float[configuration.dim];
            state.hb = new float[configuration.hidden_dim];
            state.hb2 = new float[configuration.hidden_dim];
            state.q = new float[configuration.dim];
            state.k = new float[configuration.dim];
            state.v = new float[configuration.dim];
            state.att = new float[configuration.n_heads * configuration.seq_len];
            state.logits = new float[configuration.vocab_size];
            state.probindex = new ProbabilitiesIndex[configuration.vocab_size];
            state.key_cache = new float[configuration.n_layers * configuration.seq_len * configuration.dim];
            state.value_cache = new float[configuration.n_layers * configuration.seq_len * configuration.dim];
        }
    }
}
