using System;
using System.Collections.Generic;
using RuanR.RuneFramework.Manager;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RuanR.RuneFramework.Log
{
    public class LogManager : Singleton<LogManager>
    {
    #region Private Variables

        [ShowInInspector] private bool                 isBlockAnyLog    = false;
        [ShowInInspector] private List<LogMessageType> logTypeWhitelist = new();
        [ShowInInspector] private List<Type>           systemBlacklist  = new();

    #endregion

    #region Public Methods

        public override void Init()
        {
            logTypeWhitelist.Add(LogMessageType.all);
            logTypeWhitelist.Add(LogMessageType.info);
            logTypeWhitelist.Add(LogMessageType.error);
            logTypeWhitelist.Add(LogMessageType.warning);
            logTypeWhitelist.Add(LogMessageType.test);
        }

        public void Log(string _message , LogMessageType _messageType = LogMessageType.all , Type _system = null)
        {
            if (WhitelistFilter(_messageType) && BlacklistFilter(_system))
            {
                Debug.Log(_message);
            }
        }

    #endregion

    #region Private Methods

        private bool BlacklistFilter(Type _type)
        {
            if (isBlockAnyLog)
            {
                return false;
            }

            for (int i = 0 ; i < systemBlacklist.Count ; i++)
            {
                if (systemBlacklist[i] == _type)
                {
                    return false;
                }
            }

            return true;
        }

        private bool WhitelistFilter(LogMessageType _messageType)
        {
            for (int i = 0 ; i < logTypeWhitelist.Count ; i++)
            {
                if (logTypeWhitelist[i] == LogMessageType.all || logTypeWhitelist[i] == _messageType)
                {
                    return true;
                }
            }

            return false;
        }

    #endregion
    }

    [Flags]
    public enum LogMessageType
    {
        [LabelText("全部")] all     = 0 ,
        [LabelText("資訊")] info    = 1 ,
        [LabelText("錯誤")] error   = 2 ,
        [LabelText("錯誤")] warning = 3 ,
        [LabelText("測試")] test    = 4
    }
}