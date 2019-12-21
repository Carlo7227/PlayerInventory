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
     public class TreasureClassTests
     {
          [Fact]
          public void ConstructorNullTest()
          {
               var tc = new TreasureClass("", 1, 1, 1, 1, 1, 1, 1, 1, null, null);
               Assert.NotNull(tc.Items);
               Assert.NotNull(tc.Probabilities);
          }

          [Fact]
          public void ConstructorTotalProbabilityTest()
          {
               Random r = new Random();

               var randomPicks = r.Next(0, 100);

               var probabilities = Enumerable.Range(0, randomPicks)
                    .Select(_ => r.Next(0, 100))
                    .ToArray();

               var expected = probabilities.Sum();

               var tc = new TreasureClass(
                    name: nameof(ConstructorTotalProbabilityTest),
                    picks: 1,
                    items: probabilities.Select(p => p.ToString()).ToArray(), 
                    probabilities: probabilities);

               var actual = tc.TotalProbability;

               Assert.Equal(expected, actual);
          }

          [Fact]
          public void ConstructorLimitProbabilityToItemsTest()
          {
               var tc = new TreasureClass(
                    name: nameof(ConstructorTotalProbabilityTest),
                    picks: 1,
                    items: new string[] { "", "" },
                    probabilities: new int[] { 1, 2, 3 });

               var actual = tc.Probabilities.Length;

               Assert.Equal(2, actual);
          }
     }
}
