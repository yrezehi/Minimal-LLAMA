namespace LLama.Entities
{
    public class RoPE
    {
        public float q0;
        public float q1;

        public float k0;
        public float k1;

        public float fcr;
        public float fci;

        private RoPE() { }

        public static RoPE Empty() =>
            new RoPE();
    }
}
