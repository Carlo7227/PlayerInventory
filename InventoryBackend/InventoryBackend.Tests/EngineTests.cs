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
     public class EngineTests
     {
          [Fact]
          public void GenerateDropsPicksTest()
          {
               var tc = new TreasureClass(
                    name: nameof(GenerateDropsPicksTest),
                    picks: 7,
                    items: new string[] { "item1", "item2" },
                    probabilities: new int[] { 1, 1 });

               var engine = new Engine();

               var drops = engine.GenerateDrops(tc);

               Assert.Equal(6, drops.Length);
          }

          [Theory]
          [InlineData(-3, new string[] { "pickfirst", "picksecond" }, new int[] { 1, 1 }, new string[] { "pickfirst", "picksecond", "pickfirst" })]
          [InlineData(-4, new string[] { "pickfirst", "picksecond" }, new int[] { 1, 2 }, new string[] { "pickfirst", "picksecond", "picksecond", "pickfirst" })]
          [InlineData(-3, new string[] { "pickfirst" }, new int[] { 1 }, new string[] { "pickfirst", "pickfirst", "pickfirst" })]
          [InlineData(-7, new string[] { "pickfirst" }, new int[] { 1 }, new string[] { "pickfirst", "pickfirst", "pickfirst", "pickfirst", "pickfirst", "pickfirst" })]
          public void GenerateDropsNegativePicksTheory(int picks, string[] items, int[] probabilities, string[] expectedItems)
          {
               var tc = new TreasureClass(
                    name: nameof(GenerateDropsPicksTest),
                    picks: picks,
                    items: items,
                    probabilities: probabilities);

               var engine = new Engine();

               var actualItems = engine.GenerateDrops(tc);

               Assert.Equal(expectedItems, actualItems);
          }

          [Theory]
          [MemberData(nameof(NewNoDropData))]
          public void NewNoDropTheory(int totalProbability, int noDrop, int additionalPlayers, int expected)
          {
               var tc = new TreasureClass(
                    name: nameof(NewNoDropTheory),
                    picks: 1,
                    probabilities: new int[] { totalProbability },
                    noDrop: noDrop);

               var engine = new Engine();

               var actual = engine.CalculateNewNoDrop(totalProbability, noDrop, additionalPlayers);

               Assert.Equal(expected, actual);
          }

          public static IEnumerable<object[]> NewNoDropData =>
               new List<object[]>
               {
                    new object[] { 60, 100, 7, 11 },
               };
     }
}
