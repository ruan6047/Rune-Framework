using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RuanR.RuneFramework.Manager
{
    public class GameManager : SerializedMonoBehaviour
    {
        public static T GetManager<T>() where T : Manager<T>
        {
            if (_managers.ContainsKey(typeof(T)))
            {
                return _managers[typeof(T)] as T;
            }

            var singleton = GetInstance<T>();
            return singleton;
        }

        [RuntimeInitializeOnLoadMethod]
        public static void InitializedInstance()
        {
            var go = GameObject.Find("GameManager");
            if (go == null)
            {
                go = new GameObject("GameManager");
                DontDestroyOnLoad(go);
            }

            _instance = go.AddComponent<GameManager>();
            Root = go;
        }

    #region Private Methods

        private static T GetInstance<T>() where T : Manager<T>
        {
            if (_managers.ContainsKey(typeof(T)) == false)
            {
                var gameObject = new GameObject(typeof(T).Name);
                var singleton = gameObject.AddComponent<T>();
                gameObject.transform.SetParent(Root.transform);
                _managers.Add(typeof(T), singleton);
            }

            return _managers[typeof(T)] as T;
        }

    #endregion

    #region Private Variables

        [ShowInInspector] private static Dictionary<Type, Manager> _managers = new();

        private static GameManager _instance;

        internal static GameObject Root;

    #endregion
    }
}