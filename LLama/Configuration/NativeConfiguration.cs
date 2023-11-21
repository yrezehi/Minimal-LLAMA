using System.Runtime.InteropServices;

namespace LLama.Native.Configuration.Native
{
	[StructLayout(LayoutKind.Sequential)]

	public struct NativeConfiguration
	{
		public int dim;
		public int hidden_dim;
		public int n_layers;
		public int n_heads;
		public int n_kv_heads;
		public int vocab_size;
		public int seq_len;
	}
}
