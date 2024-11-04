using System;
using System.Collections.Generic;
using NLua;

namespace GameMono;

class AI(string luaScript) : LuaModule(luaScript)
{
    protected override bool ValidateScript()
    {
        if((string)state["SCRIPTTYPE"] != "AI")
        {
            Console.WriteLine(ScriptName + " is not an AI script type!");
            return false;
        }

        return true;
    }

    public static AI CreateAI(string name)
    {
        return new AI(name);
    }
}