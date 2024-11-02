using NLua;

namespace GameMono;

abstract class LuaModule
{
    public readonly Lua state = new();

    public LuaModule(string luaScript)
    {
        state.DoString(luaScript);
        InitScript();
    }

    protected abstract void InitScript();
}