using System;
using System.Collections.Generic;
using NLua;

namespace GameMono;

class AI(string luaScript) : LuaModule(luaScript)
{
    protected override void InitScript()
    {
        LuaFunction init = (LuaFunction)state["init"];
        Console.WriteLine("EXECUTING INIT");
        init.Call();
    }

    readonly private static Dictionary<string, string> scripts = [];
    public static void RegisterAI(string script, string name)
    {
        scripts.Add(name, script);
    }

    public static AI CreateAI(string name)
    {
        return new AI(scripts[name]);
    }
}