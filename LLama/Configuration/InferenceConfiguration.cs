namespace LLama.Configuration
{
	public static class InferenceConfiguration
	{
		public static float Temperature = 1.0f;
		public static float Topp = 0.9f;
		public static int Steps = 256;
		public static long Seed = DateTime.UtcNow.Ticks;
	}
}
