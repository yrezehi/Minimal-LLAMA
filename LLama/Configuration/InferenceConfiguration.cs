namespace LLama.Configuration
{
	public class InferenceConfiguration
	{
		public float Temperature = 1.0f;
		public float Topp = 0.9f;
		public int Steps = 256;
		public long Seed = DateTime.UtcNow.Ticks;

		public static InferenceConfiguration Create() =>
			new InferenceConfiguration();

	}
}
