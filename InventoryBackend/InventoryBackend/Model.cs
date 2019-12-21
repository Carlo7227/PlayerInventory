using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotFSharp
{
     public class DiabloContext : DbContext
     {
          public DbSet<TreasureClass> TreasureClasses { get; set; }

          protected override void OnConfiguring(DbContextOptionsBuilder options)
               => options.UseSqlite("Data Source=d2exp.db");
     }
}
