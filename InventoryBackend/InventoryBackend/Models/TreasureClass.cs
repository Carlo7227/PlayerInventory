using Microsoft.Extensions.Caching.Memory;
using NotFSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection;
using System.Text;

namespace NotFSharp
{
     public class TreasureClass
     {
          public string Name;
          public int Picks;
          public int? Group;
          public int? Level;
          public int? Unique;
          public int? Set;
          public int? Rare;
          public int? Magic;
          public int? NoDrop;
          public string Item1;
          public int? Prob1;
          public string Item2;
          public int? Prob2;
          public string Item3;
          public int? Prob3;
          public string Item4;
          public int? Prob4;
          public string Item5;
          public int? Prob5;
          public string Item6;
          public int? Prob6;
          public string Item7;
          public int? Prob7;
          public string Item8;
          public int? Prob8;
          public string Item9;
          public int? Prob9;
          public string Item10;
          public int? Prob10;
          public int TotalProbability;

          private string[] _items;
          public string[] Items => _items;

          private int[] _probabilities;
          public int[] Probabilities => _probabilities;

          // // Not Used
          //public readonly int SumItems;
          //public readonly decimal DropChance;
          //public readonly int Term;

          public TreasureClass(
               string name,
               int picks,
               int? group = null,
               int? level = null,
               int? unique = null,
               int? set = null,
               int? rare = null,
               int? magic = null,
               int? noDrop = null,
               string[] items = null,
               int[] probabilities = null)
          {
               Name = name;
               Group = group;
               Level = level;
               Picks = picks;
               Unique = unique;
               Set = set;
               Rare = rare;
               Magic = magic;
               NoDrop = noDrop;

               _items = items ?? new string[0];
               _probabilities = probabilities ?? new int[0];

               if (_probabilities.Length > _items.Length)
               {
                    _probabilities = _probabilities.Take(_items.Length)
                         .ToArray();
               }

               for(int i = 0; i < Math.Min(_items.Length, 10); i++)
               {
                    var type = this.GetType();

                    type.GetField($"Item{i + 1}")
                         .SetValue(this, _items[i]);

                    type.GetField($"Prob{i + 1}")
                         .SetValue(this, _probabilities[i]);
               }

               TotalProbability = _probabilities.Sum();
          }
     }
}
