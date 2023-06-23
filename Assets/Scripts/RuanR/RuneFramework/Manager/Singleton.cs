using Sirenix.OdinInspector;
using UnityEngine;

namespace RuanR.RuneFramework.Manager
{
    public class Singleton<T> : SerializedMonoBehaviour where T : Component
    {
        private static T _instance;
        public static  T Instance => GetInstance();
        private static T GetInstance()
        {
            if (_instance != null) return _instance;
            var go = GameObject.Find("RuneManager");
            if (go == null)
            {
                go = new GameObject("RuneManager");
                DontDestroyOnLoad(go);
            }

            _instance = go.AddComponent<T>();
            return _instance;
        }

    }
}