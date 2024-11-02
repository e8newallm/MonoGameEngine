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

    private static bool VerifyAI(string script)
    {
        Lua state = new();
        state.DoString(script);
        if(state.GetString("FILETYPE") != "AI")
        {
            Console.WriteLine("FILETYPE isn't \"AI\"");
            return false;
        }

        if(state.GetFunction("init") == null)
        {
            Console.WriteLine("no init function found!");
            return false;
        }

        return true;
    }

    readonly private static Dictionary<string, string> scripts = [];
    public static bool RegisterAI(string script, string name)
    {
        if(VerifyAI(script))
        {
            scripts.Add(name, script);
            return true;
        }
        return false;
    }

    public static AI CreateAI(string name)
    {
        return new AI(scripts[name]);
    }
}