using System;
using Microsoft.Xna.Framework;
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

        if(!IsFunction("update"))
        {
            Console.WriteLine(ScriptName + " does not contain an update function!");
            return false;
        }

        return true;
    }

    public void Update(World terrain, GameTime gameTime)
    {
        if(IsValid) (state["update"] as LuaFunction).Call(terrain, gameTime);
    }

    public static AI CreateAI(string name)
    {
        return new AI(name);
    }
}