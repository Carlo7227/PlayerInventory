using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CInventory.Models.Interfaces
{
    public interface IFromJObject<T>
    {
        T FromJObject(JObject jObject);
    }
}