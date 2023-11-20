using System.Runtime.InteropServices;

namespace LLama.Native.Configuration
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WeightsConfiguration
    {
        public float[] token_embedding_table; // (vocab_size, dim)
        public ArraySegment<float> rms_att_weight; // (layer, dim) rmsnorm weights
        public ArraySegment<float> rms_ffn_weight; // (layer, dim)

        public ArraySegment<float> wq; // (layer, dim, dim)
        public ArraySegment<float> wk; // (layer, dim, dim)
        public ArraySegment<float> wv; // (layer, dim, dim)

        public ArraySegment<float> wo; // (layer, dim, dim)

        public ArraySegment<float> w1; // (layer, hidden_dim, dim)
        public ArraySegment<float> w2; // (layer, dim, hidden_dim)
        public ArraySegment<float> w3; // (layer, hidden_dim, dim)

        public float[] rms_final_weight; // (dim,)

        public float[] freq_cis_real; // (seq_len, head_size/2)

        public float[] freq_cis_imag; // (seq_len, head_size/2)

        public float[] wcls;
    }
}
