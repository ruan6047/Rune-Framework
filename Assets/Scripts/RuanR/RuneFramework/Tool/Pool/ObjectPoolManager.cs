using System;
using System.Collections.Generic;
using RuanR.RuneFramework.Manager;
using RuanR.RuneFramework.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RuanR.RuneFramework.Tool.Pool
{
    public class ObjectPoolManager : Manager<ObjectPoolManager>
    {
        #region Private Variables

        [ShowInInspector] [HideReferenceObjectPicker]
        private Dictionary<Type, IPool> pools = new();

        #endregion

        #region Public Methods

        public override void Init()
        {
            this.transform.position = Vector3.one * short.MaxValue;
        }

        public void Recycle<T>(T _obj) where T : class, IPoolItem, new()
        {
            var type = typeof(T);
            if (_obj is GameObjectPoolItem)
            {
                if (pools.TryGetValue(type, out var value))
                {
                    value.Release(_obj);
                    return;
                }
                _obj.Log($"{type.Name} 物件池不存在 無須回收 物件直接銷毀");
                if (_obj is MonoBehaviour obj)
                {
                    Destroy(obj);
                }
            }
            else if (pools.TryGetValue(type, out var value1) && value1 is IPool<T> objectPool)
            {
                objectPool.Release(_obj);
                return;
            }

            _obj.Log($"{type.Name}不屬於物件池物件 故不做處理");
        }

        public void Spawn<T>(out T _result) where T : class, IPoolItem, new()

        {
            _result = null;
            var type = typeof(T);
            if (pools.ContainsKey(type) && pools[type] is IPool<T> objectPool)
            {
                if (objectPool.TryGetObject(out var item))
                {
                    _result = item;
                    return;
                }

                _result = new T();
                _result.Log($"{typeof(T).Name}物件池中數量不夠 生成新物件", LogMessageType.Info);
                return;
            }

            this.Log($"{typeof(T).Name}並不存在於物件池中", LogMessageType.Warning);
        }

        public void Spawn<T>(out T _result, string _name, Transform _parent = null)
            where T : GameObjectPoolItem
        {
            var type = typeof(T);
            _result = null;
            if (pools.ContainsKey(type) && pools[type] is GameObjectPool<T> gameObjectPool)
            {
                if (gameObjectPool.TryGetObject(_name, out var item, _parent))
                {
                    _result = item;
                    return;
                }

                this.Log($"{type.Name}物件池中 並沒有名為{_name}的樣板", LogMessageType.Warning);
                return;
            }

            this.Log($"{type.Name}並不存在於物件池中", LogMessageType.Warning);
        }

        public void InitPool<T>(IPoolItemConfig _config = null) where T : class, IPoolItem, new()
        {
            var type = typeof(T);
            _config ??= new PoolItemConfig(10);
            pools.TryAdd(type, new ObjectPool<T>(_config));
        }

        public void InitPool<T>(T _prefab, IPoolItemConfig _config = null) where T : GameObjectPoolItem
        {
            if (_prefab == null)
                return;
            var type = typeof(T);
            if (pools.ContainsKey(type) == false)
            {
                var classRoot = new GameObject(type.Name);
                classRoot.transform.SetParent(this.transform);
                classRoot.transform.localPosition = Vector3.zero;
                pools.Add(type, new GameObjectPool<T>(classRoot.transform));
            }

            if (pools[type] is not GameObjectPool<T> gameObjectPool)
                return;
            _config ??= new PoolItemConfig(10);
            gameObjectPool.SpawnPrefabPool(_prefab, _config);
        }

        #endregion
    }
}