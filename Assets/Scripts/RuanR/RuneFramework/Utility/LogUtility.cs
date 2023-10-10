using RuanR.RuneFramework.Log;

namespace RuanR.RuneFramework.Utility
{
    public static class LogUtility
    {
    #region Public Methods

        public static void Log(string s)
        {
            LogManager.Instance.Log(s);
        }

    #endregion
    }
}