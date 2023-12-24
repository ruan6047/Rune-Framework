using Sirenix.OdinInspector;

namespace RuanR.RuneFramework.Manager
{
    public class Manager<T> : Manager where T : Manager<T>
    {
    #region Private Variables

        private static T instance;

    #endregion

    #region Public Variables

        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = GameManager.GetManager<T>();
                instance.Init();
                return instance;
            }
            set => instance = value;
        }

    #endregion
    }

    public abstract class Manager : SerializedMonoBehaviour
    {
    #region Public Methods

        public virtual void Init() { }

    #endregion
    }
}