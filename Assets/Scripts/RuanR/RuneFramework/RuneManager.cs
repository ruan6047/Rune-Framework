using System.Collections;
using System.Collections.Generic;
using RuanR.RuneFramework.Utility;
using Sirenix.OdinInspector;
using UnityEngine;
namespace RuanR.RuneFramework
{
    public class RuneManager : SerializedMonoBehaviour
    {
        private static RuneManager _instance;
        public static  RuneManager Instance => GetInstance();
        private static RuneManager GetInstance() {
            if (_instance != null) return _instance;
            var go = GameObject.Find("RuneManager");
            if (go == null) 
            { 
                go = new GameObject("RuneManager");
                DontDestroyOnLoad(go);   
            }
            _instance = go.AddComponent<RuneManager>();
            return _instance;
        }

        public void Init()
        {
            RuneLog.Log("Hello, World!");
        }
    }
}

