using NotFSharp.Extensions;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NotFSharp
{
     public class Dropper
     {
          public static double[] CalculateDropProbabilities(int[] probabilities)
          {
               if (probabilities.Length == 0)
               {
                    return new double[0];
               }

               var probSum = probabilities.Sum();

               return probabilities.Select(p => Math.Round(p / (double)probSum, 4, MidpointRounding.ToEven) )
                    .ToArray();
          }

          public static int PickRandom(double[] probabilities)
          {
               Random r = new Random();
               double diceRoll = r.NextDouble();

               double cumulative = 0.0;

               int i;
               for (i = 0; i < probabilities.Length; i++)
               {
                    cumulative += probabilities[i];
                    if (diceRoll < cumulative || i == probabilities.Length - 1)
                    {
                         break;
                    }
               }

               return i;
          }
     }
}
