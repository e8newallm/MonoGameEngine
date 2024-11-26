using System;
using NLua;
using System.Collections.Generic;

namespace GameMono;

public abstract class LuaModule : DataStore<string>
{
    public readonly Lua state = new();
    protected string ScriptName {get; private set;}
    public bool IsValid {get; private set;}

    protected LuaModule(string luaScript)
    {
        ScriptName = luaScript;
        state.DoString(Data[luaScript]);
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
}