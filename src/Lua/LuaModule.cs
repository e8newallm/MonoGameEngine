using System;
using NLua;
using System.Collections.Generic;

namespace GameMono;

abstract class LuaModule
{
    public readonly Lua state = new();
    protected string ScriptName {get; private set;}
    public bool IsValid {get; private set;}

    protected LuaModule(string luaScript)
    {
        ScriptName = luaScript;
        state.DoString(scripts[luaScript]);
        IsValid = BaseValidateScript();
    }

    public void RegThis(object linkedObject)
    {
        state["this"] = linkedObject;
    }

    protected bool BaseValidateScript()
    {
        if(state["SCRIPTTYPE"].GetType() != typeof(string))
        {
            Console.WriteLine(ScriptName + " does not contain a SCRIPTTYPE identifier!");
            return false;
        }

        return ValidateScript();
    }
    protected abstract bool ValidateScript();

    public bool IsFunction(string func)
    {
        return state[func] != null && state[func].GetType() == typeof(NLua.LuaFunction);
    }

    readonly private static Dictionary<string, string> scripts = [];
    public static void RegisterScript(string script, string name)
    {
        scripts.Add(name, script);
    }
}