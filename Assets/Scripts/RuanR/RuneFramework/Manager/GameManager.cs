using System;
using System.Collections.Generic;
using RuanR.RuneFramework.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RuanR.RuneFramework.Manager
{
    public class GameManager : SerializedMonoBehaviour
    {
    #region Public Variables

        public static GameManager Instance => GetInstance();

    #endregion

    #region Private Variables

        private static GameManager _instance;

        [ShowInInspector]
        internal Dictionary<Type , Singleton> managers = new();

        internal GameObject root;

    #endregion

    #region Public Methods

        public T GetManager<T>() where T : Singleton , new()
        {
            if (managers.ContainsKey(typeof(T)))
            {
                return managers[typeof(T)] as T;
            }

            var singleton = Singleton.GetInstance<T>();
            return singleton;
        }

        public void Init()
        {
            RuneLog.Log("Hello, World!");
        }

    #endregion

    #region Private Methods

        private static GameManager GetInstance()
        {
            if (_instance != null) return _instance;
            var go = GameObject.Find("GameManager");
            if (go == null)
            {
                go = new GameObject("GameManager");
                DontDestroyOnLoad(go);
            }

            _instance      = go.AddComponent<GameManager>();
            _instance.root = go;
            return _instance;
        }

    #endregion
    }
}