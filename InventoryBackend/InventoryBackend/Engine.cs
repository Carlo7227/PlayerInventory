using NotFSharp.Models;  
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace NotFSharp
{
     public class Engine
     {
          public int Players;

          public Dictionary<string, TreasureClass> TreasureClasses;

          public Engine()
          {
               TreasureClasses = new Dictionary<string, TreasureClass>();

               var treasureClassExLines = File.ReadAllLines("data/global/excel/TreasureClassEx.txt");
               foreach(var line in treasureClassExLines.Skip(1))
               {
                    var columns = line.Split('\t');

                    if (columns[0] == "")
                    {
                         continue;
                    }

                    var name = columns[0];
                    var group = IntOrNull(columns[1]);
                    var level = IntOrNull(columns[2]);
                    var picks = int.Parse(columns[3]);
                    var unique = IntOrNull(columns[4]);
                    var set = IntOrNull(columns[5]);
                    var rare = IntOrNull(columns[6]);
                    var magic = IntOrNull(columns[7]);
                    var noDrop = IntOrNull(columns[8]);

                    var items = new List<string>();
                    var probabilities = new List<int>();

                    int itemStart = 9;

                    for(int i = 0; i < 10; i++)
                    {
                         var pos = i * 2;

                         if (string.IsNullOrEmpty(columns[itemStart + pos]))
                         {
                              continue;
                         }

                         items.Add(columns[itemStart + pos]);
                         probabilities.Add(IntOrNull(columns[itemStart + pos + 1]) ?? 0);
                    }

                    var tc = new TreasureClass(
                         name: name,
                         group: group,
                         level: level,
                         picks: picks,
                         unique: unique,
                         set: set,
                         rare: rare,
                         magic: magic,
                         noDrop: noDrop,
                         items: items.ToArray(),
                         probabilities: probabilities.ToArray()
                         );
               }
          }

          private int? IntOrNull(string s)
          {
               return int.TryParse(s, out int i)
                    ? i :
                    (int?)null;
          }

          public string[] GenerateDrops(TreasureClass treasureClass)
          {
               var items = treasureClass.Picks < 0
                    ? PickSequentially(treasureClass)
                    : PickRandomly(treasureClass);

               return items
                    .Take(6)
                    .ToArray();
          }

          private IEnumerable<string> PickSequentially(TreasureClass treasureClass)
          {
               int pick = -1;
               int picks = Math.Abs(treasureClass.Picks);
               int take = 0;

               while(picks > 0)
               {
                    if (take == 0)
                    {
                         pick++;
                         if (pick >= treasureClass.Items.Length)
                         {
                              pick = 0;
                         }
                         
                         take = treasureClass.Probabilities[pick];
                    }

                    if (take > 0)
                    {
                         take--;
                         picks--;
                         yield return treasureClass.Items[pick];
                    }
               }
          }

          private IEnumerable<string> PickRandomly(TreasureClass treasureClass)
          {
               var items = new List<string>();

               for (int i = 0; i < treasureClass.Picks; i++)
               {
                    var weights = new List<int>();

                    if (treasureClass.NoDrop.HasValue)
                    {
                         var noDrop = treasureClass.NoDrop.Value;

                         if (Players > 1)
                         {
                              noDrop = CalculateNewNoDrop(
                                   treasureClass.TotalProbability,
                                   treasureClass.NoDrop ?? 0,
                                   Players - 1);
                         }

                         weights.Add(noDrop);
                    }

                    weights.AddRange(treasureClass.Probabilities);

                    var probabilities = Dropper.CalculateDropProbabilities(weights.ToArray());
                    var pick = Dropper.PickRandom(probabilities);

                    // check tc

                    items.Add(treasureClass.Items[pick]);
               }

               return items;
          }

          public int CalculateNewNoDrop(int totalProbability, int noDrop, int additionalPlayers)
          {
               var x = totalProbability;

               var n = 1 + (additionalPlayers / 2);

               var nd = noDrop;

               var d = Math.Pow(
                    (nd / (double)(nd + x)),
                    n
               );

               return (int)Math.Round(x / (1 / d - 1));
          }
     }
}
