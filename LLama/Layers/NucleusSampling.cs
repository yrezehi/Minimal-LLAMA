using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLama.Layers
{
    public class NucleusSampling
    {
        private static int SampleTopp(float[] probabilities, int configVocabSize, float topp, ProbabilitiesIndex[] probabilitiesIndexes)
        {
            for (int i = 0; i < configVocabSize; i++)
            {
                probindex[i].Index = i;
                probindex[i].Prob = probabilities[i];
            }

            Array.Sort(probindex, Compare);

            float cumulativeProb = 0.0f;
            int lastIdx = 0;
            for (int i = 0; i < configVocabSize; i++)
            {
                cumulativeProb += probindex[i].Prob;
                if (cumulativeProb > topp)
                {
                    lastIdx = i;
                    break;
                }
            }

            float r = RandomF32() * cumulativeProb;
            float cdf = 0.0f;
            for (int i = 0; i <= lastIdx; i++)
            {
                cdf += probindex[i].Prob;
                if (r < cdf) return probindex[i].Index;
            }

            return probindex[lastIdx].Index;
        }
    }
}
