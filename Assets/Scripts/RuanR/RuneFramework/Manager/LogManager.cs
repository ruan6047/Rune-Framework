using RuanR.RuneFramework.Utility;

namespace RuanR.RuneFramework.Manager
{
    public class LogManager : Singleton
    {
    #region Public Methods

        public void Print()
        {
            RuneLog.Log(nameof(LogManager));
        }

    #endregion
    }
}