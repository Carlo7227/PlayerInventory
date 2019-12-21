using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.ComTypes;
using Xunit;
using NotFSharp.Extensions;

namespace NotFSharp.Tests
{
     public class DropperTests
     {
          [Theory]
          [MemberData(nameof(DropProbabilityData))]
          public void DropProbabilityTheory(int[] probabilities, double[] expected)
          {
               var actual = Dropper.CalculateDropProbabilities(probabilities);

               Assert.Equal(expected, actual);

               var sum = actual.Sum();
               Assert.True(sum.CloseEnough(1.0));
          }

          [Theory]
          [MemberData(nameof(PickRandomData))]
          public void PickRandomTheory(double[] probabilities)
          {
               var numDrops = 500_000;
               var occurances = new int[probabilities.Length];

               var drops = Enumerable.Range(0, numDrops)
                    .AsParallel()
                    .Select(_ => Dropper.PickRandom(probabilities))
                    .Aggregate(occurances, (o, i) => 
                    {
                         o[i]++;
                         return o;
                    })
                    .Select(o => o / (double)numDrops);

               var zipped = probabilities.Zip(drops);

               var allCloseEnough = zipped
                    .All((t) => t.First.CloseEnough(t.Second));

               Assert.True(allCloseEnough, $"[\n{string.Join("\n\t", string.Join('\n', zipped.Select(t => $"{t.First} => {t.Second} => {t.First - t.Second},")))}\n]");
          }



          public static IEnumerable<object[]> DropProbabilityData =>
               new List<object[]>
               {
                    new object[]
                    {
                         new int[] { 1, 1 },
                         new double[] { 0.5, 0.5 }
                    },
                    new object[]
                    {
                         new int[] { 0, 1, 1 },
                         new double[] { 0.0, 0.5, 0.5 }
                    },
                    new object[]
                    {
                         new int[] { 20, 20 },
                         new double[] { 0.5, 0.5 }
                    },
                    new object[]
                    {
                         new int[] { 3, 3, 3 },
                         new double[] { 0.3333, 0.3333, 0.3333 }
                    },
                    new object[]
                    {
                         new int[] { 11, 21, 16, 21, 2 },
                         new double[] { 0.1549, 0.2958, 0.2254, 0.2958, 0.0282 }
                    },
                    new object[]
                    {
                         new int[] { 2, 1, 6, 3, 15, 8, 1530 },
                         new double[] { 0.0013, 0.0006, 0.0038, 0.0019, 0.0096, 0.0051, 0.9776 }
                    },
                    new object[]
                    {
                         new int[] { 3, 3, 3, 3, 3, 3, 1, 1, 1, 1 },
                         new double[] { 0.1364, 0.1364, 0.1364, 0.1364, 0.1364, 0.1364, 0.0455, 0.0455, 0.0455, 0.0455 }
                    },
               };

          public static IEnumerable<object[]> PickRandomData =>
               new List<object[]>
               {
                    new object[]
                    {
                         new double[] { 0.5, 0.5 }
                    },
                    new object[]
                    {
                         new double[] { 0.5, 0.5 }
                    },
                    new object[]
                    {
                         new double[] { 0.3333, 0.3333, 0.3333 }
                    },
                    new object[]
                    {
                         new double[] { 0.1549, 0.2958, 0.2254, 0.2958, 0.0282 }
                    },
                    new object[]
                    {
                         new double[] { 0.0013, 0.0006, 0.0038, 0.0019, 0.0096, 0.0051, 0.9776 }
                    },
                    new object[]
                    {
                         new double[] { 0.1364, 0.1364, 0.1364, 0.1364, 0.1364, 0.1364, 0.0455, 0.0455, 0.0455, 0.0455 }
                    },
               };
     }
}
