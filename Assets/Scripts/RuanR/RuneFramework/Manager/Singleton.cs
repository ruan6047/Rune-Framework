using Sirenix.OdinInspector;
using UnityEngine;

namespace RuanR.RuneFramework.Manager
{
    public class Singleton : SerializedMonoBehaviour
    {
    #region Private Variables

        private static Singleton _instance;

    #endregion

    #region Public Methods

        public static T GetInstance<T>() where T : Singleton
        {
            if (_instance != null) return _instance as T;
            var root       = GameManager.Instance.root;
            var gameObject = new GameObject();
            gameObject.name = typeof(T).Name;
            T singleton = gameObject.AddComponent<T>();
            gameObject.transform.parent = root.transform;
            _instance                   = singleton;
            GameManager.Instance.managers.Add(typeof(T) , singleton);
            return _instance as T;
        }

    #endregion
    }
}