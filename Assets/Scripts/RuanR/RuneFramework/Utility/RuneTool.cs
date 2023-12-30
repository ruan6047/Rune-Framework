using RuanR.RuneFramework.Tool;
using RuanR.RuneFramework.Tool.Pool;

namespace RuanR.RuneFramework.Utility
{
    public static class RuneTool
    {
    #region Public Methods

        public static void Log(this object _system, string _message = null,
                               LogMessageType _messageType = LogMessageType.All)
        {
            LogManager.Instance.Log(_message, _messageType, _system);
        }

        public static void Recycle<T>(ref T _item) where T : class, IPoolItem, new()
        {
            ObjectPoolManager.Instance.Recycle(_item);
            _item = null;
        }

    #endregion
    }
}