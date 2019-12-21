using System;
using System.Collections.Generic;
using System.Text;

namespace NotFSharp.Extensions
{
     public static class DoubleExtensions
     {
          public static bool CloseEnough(this double d, double u)
          {
               return (Math.Abs(d - u) < 0.01);
          }
     }
}
