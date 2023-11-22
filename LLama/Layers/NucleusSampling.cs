using LLama.Entities;
using LLama.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLama.Layers
{
    public class NucleusSampling
    {
        private static int SampleTopp(ProbabilitiesIndex[] probabilitiesIndexes, float[] probabilities, int vocabSize, float topp, long rngSeed)
        {
            for (int i = 0; i < vocabSize; i++)
            {
                probabilitiesIndexes[i].Index = i;
                probabilitiesIndexes[i].Probability = probabilities[i];
            }

            Array.Sort(probabilitiesIndexes, ProbabilitiesIndexExtensions.ProbabilitiesComparer);

            float cumulativeProbability = 0.0f;

            int lastIdx = 0;
            for (int i = 0; i < vocabSize; i++)
            {
                cumulativeProbability += probabilitiesIndexes[i].Probability;
                if (cumulativeProbability > topp)
                {
                    lastIdx = i;
                    break;
                }
            }

            float randomProbability = RandomF32(rngSeed) * cumulativeProbability;

            float cdf = 0.0f;
            
            for (int i = 0; i <= lastIdx; i++)
            {
                cdf += probabilitiesIndexes[i].Probability;
                if (randomProbability < cdf)
                    return probabilitiesIndexes[i].Index;
            }

            return probabilitiesIndexes[lastIdx].Index;
        }

        private static float RandomF32(long rngSeed)
        {
            return (RandomU32(rngSeed) >>> 8) / 16777216.0f;
        }

        private static int RandomU32(long rngSeed)
        {
            rngSeed ^= rngSeed >> 12;
            rngSeed ^= rngSeed << 25;
            rngSeed ^= rngSeed >> 27;
            return (int)((rngSeed * 0x2545F4914F6CDD1DL) >> 32);
        }
    }
}
