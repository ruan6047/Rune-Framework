using System;
using System.Collections;
using System.Collections.Generic;
using RuanR.RuneFramework.Utility;
using Sirenix.OdinInspector;
using UnityEngine;
using static RuanR.RuneFramework.Utility.RuneLog;

namespace RuanR.RuneFramework
{
    public class RuneLoader : SerializedMonoBehaviour
    {
        private void Awake()
        {
            RuneManager.Instance.Init();
        }
    }
}

