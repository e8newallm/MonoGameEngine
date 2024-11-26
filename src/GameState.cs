public class GameState
{
    static private bool _pause = true;
    static private bool _stepping = false;
    static public bool Paused { get => _pause; set => _pause = value; }

    static private int _scale = 50;
    static public int Scale { get => _scale; set => _scale = value; }

    static public void DebugStep()
    {
        _pause = false;
        _stepping = true;
    }

    static public void DebugUnstep()
    {
        if(_stepping)
        {
            _pause = true;
            _stepping = false;
        }
    }
}