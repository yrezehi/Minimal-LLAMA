namespace LLama.Layers
{
    public class Softmax
    {
        public static void Normailize(float[] x, int xOffset, int size)
        {
            float maxValue = x[xOffset];

            for(int i = 1; i < size; i++)
            {
                if (x[i + xOffset] > maxValue)
                {
                    maxValue = x[i + xOffset];
                }
            }

            float sum = 0.0f;

            for (int index = 0; index < size; index++)
            {
                x[index + xOffset] = (float)Math.Exp(x[index + xOffset] - maxValue);
                sum += x[index + xOffset];
            }

            for(int index = 0; index < size; index++)
            {
                x[index + xOffset] /= sum;
            }
        }
    }
}
