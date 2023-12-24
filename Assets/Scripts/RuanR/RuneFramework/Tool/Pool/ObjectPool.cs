#region

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

#endregion

namespace RuanR.RuneFramework.Tool.Pool
{
    public interface IPool
    {
        void Release(IPoolItem _item);
    }

    public interface IPool<T> : IPool
    {
        #region Public Methods

        bool TryGetObject(out T _result);

        #endregion
    }

    public class ObjectPool<T> : IPool<T> where T : class, IPoolItem, new()
    {
        [ShowInInspector] private readonly IPoolItemConfig config;
        [ShowInInspector] private readonly Queue<T> queue;

        public ObjectPool(IPoolItemConfig _config)
        {
            config = _config;
            queue = new Queue<T>();
            for (var i = 0; i < config.Amount; i++)
            {
                queue.Enqueue(new T());
            }
        }

        public void Release(IPoolItem _item)
        {
            if (_item is not T t) return;
            t.OnRecycle();
            t.OnReset();
            queue.Enqueue(t);
        }


        public bool TryGetObject(out T _result)
        {
            _result = GetObject();
            return _result != null;
        }

        private T GetObject()
        {
            return queue.TryDequeue(out var item) ? item : new T();
        }
    }

    public interface IPoolItem
    {
        #region Public Methods

        /// <summary>
        /// Recycle Action
        /// </summary>
        void OnRecycle();

        void OnReset();

        #endregion
    }
    public interface IPoolItemConfig
    {
        public int Amount { get; }
    }
    [Serializable]
    public class PoolItemConfig : IPoolItemConfig
    {
        [SerializeField] private int amount;

        public PoolItemConfig(int _amount)
        {
            amount = _amount;
        }

        public int Amount
        {
            get => amount;
        }
    }
}