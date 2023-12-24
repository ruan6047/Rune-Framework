using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RuanR.RuneFramework.Manager
{
    public class GameManager : SerializedMonoBehaviour
    {
    #region Public Variables

        public static GameManager Instance => GetInstance();

    #endregion

    #region f

        public static T GetManager<T>() where T : Manager<T>
        {
            if (_managers.ContainsKey(typeof(T)))
            {
                return _managers[typeof(T)] as T;
            }

            var singleton = GetInstance<T>();
            return singleton;
        }

    #endregion

    #region Private Variables

        [ShowInInspector] private static Dictionary<Type, Manager> _managers = new();

        private static GameManager _instance;

        internal static GameObject Root;

    #endregion

    #region Private Methods

        private static T GetInstance<T>() where T : Manager<T>
        {
            if (_instance == null)
                GetInstance();
            if (_managers.ContainsKey(typeof(T)) == false)
            {
                var gameObject = new GameObject(typeof(T).Name);
                T singleton = gameObject.AddComponent<T>();
                gameObject.transform.SetParent(Root.transform);
                _managers.Add(typeof(T), singleton);
            }

            return _managers[typeof(T)] as T;
        }

        private static GameManager GetInstance()
        {
            if (_instance != null) return _instance;
            var go = GameObject.Find("GameManager");
            if (go == null)
            {
                go = new GameObject("GameManager");
                DontDestroyOnLoad(go);
            }

            _instance = go.AddComponent<GameManager>();
            Root = go;
            return _instance;
        }

    #endregion
    }
}