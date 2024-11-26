using System.Collections.Generic;

public class DataStore<T>
{
    static readonly protected Dictionary<string, T> Data = [];
    static public void Register(T data, string name)
    {
        Data.Add(name, data);
    }

    static public T Get(string name)
    {
        return Data[name];
    }
}