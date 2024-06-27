namespace GelatoLock.GSHelpers
{
    public static class Debug
    {
        public static void Log(string log)
        {
            TaleWorlds.Library.Debug.Print(log, 0, TaleWorlds.Library.Debug.DebugColor.Green);
        }
        
        public static void Error(string log)
        {
            TaleWorlds.Library.Debug.Print(log, 0, TaleWorlds.Library.Debug.DebugColor.Red);
        }


    }
}
