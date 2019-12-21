using CInventory.Models.Interfaces;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
public class GlobalDataParser
{
    public GlobalDataParser()
    {
    }
    public T LoadData<T>(string url) where T : IFromJObject<T>, new()
    {
        if (url == null) return default(T);
        var file = new File();
        if (!file.FileExists(url)) return default(T);
        file.Open(url, (int)File.ModeFlags.Read);
        var text = file.GetAsText();
        var jobject = JObject.Parse(text);
        var data = new T();
        data.FromJObject(jobject);
        return data;
    }
    public void WriteData<T>(string url, T dict) where T : IFromJObject<T>, new()
    {

    }

}
