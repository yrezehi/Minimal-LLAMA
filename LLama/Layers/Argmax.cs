namespace LLama.Layers
{
    public static class Argmax
    {
        public static int Maximum(float[] probabilities, int vocabSize)
        {
            int maxIndex = 0;
            float maxProbability = probabilities[0];

            for (int index = 1; index < vocabSize; index++)
            {
                if (probabilities[index] > maxProbability)
                {
                    maxIndex = index;
                    maxProbability = probabilities[index];
                }
            }

            return maxIndex;
        }
    }
}
