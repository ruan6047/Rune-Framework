using System;
using System.Collections.Generic;
using RuanR.RuneFramework.Manager;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RuanR.RuneFramework.Tool
{
    public class LogManager : Manager<LogManager>
    {
    #region Private Variables

        [ShowInInspector] private bool isBlockAnyLog = false;
        [ShowInInspector] private List<LogMessageType> logTypeWhitelist = new();
        [ShowInInspector] private List<Type> systemBlacklist = new();

    #endregion

    #region Public Methods

        public override void Init()
        {
            logTypeWhitelist.Add(LogMessageType.All);
            logTypeWhitelist.Add(LogMessageType.Info);
            logTypeWhitelist.Add(LogMessageType.Error);
            logTypeWhitelist.Add(LogMessageType.Warning);
            logTypeWhitelist.Add(LogMessageType.Test);
        }

        public void Log(string _message, LogMessageType _messageType = LogMessageType.All, object _system = null)
        {
            if (_system != null && WhitelistFilter(_messageType) && BlacklistFilter(_system.GetType()))
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

            for (var i = 0; i < systemBlacklist.Count; i++)
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
            for (var i = 0; i < logTypeWhitelist.Count; i++)
            {
                if (logTypeWhitelist[i] == LogMessageType.All || logTypeWhitelist[i] == _messageType)
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
        [LabelText("全部")] All = 0,
        [LabelText("資訊")] Info = 1,
        [LabelText("錯誤")] Error = 2,
        [LabelText("錯誤")] Warning = 3,
        [LabelText("測試")] Test = 4,
    }
}