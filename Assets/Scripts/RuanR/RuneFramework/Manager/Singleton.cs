using Sirenix.OdinInspector;

namespace RuanR.RuneFramework.Manager
{
    public class Singleton<T> : Singleton where T : Singleton<T>
    {
    #region Public Variables

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = GameManager.GetManager<T>();
                _instance.Init();
                return _instance;
            }
            set => _instance = value;
        }

    #endregion

    #region Private Variables

        private static T _instance;

    #endregion
    }

    public abstract class Singleton : SerializedMonoBehaviour
    {
    #region Public Methods

        public virtual void Init() { }

    #endregion
    }
}