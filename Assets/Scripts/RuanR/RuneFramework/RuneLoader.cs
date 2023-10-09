using RuanR.RuneFramework.Manager;
using Sirenix.OdinInspector;

namespace RuanR.RuneFramework
{
    public class RuneLoader : SerializedMonoBehaviour
    {
    #region Private Methods

        private void Awake()
        {
            GameManager.Instance.Init();
            LogManager logM = GameManager.Instance.GetManager<LogManager>();
            logM.Print();
        }

    #endregion
    }
}