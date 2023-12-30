using System.Collections.Generic;
using System.Linq;
using RuanR.RuneFramework.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RuanR.RuneFramework.Tool.Pool
{
    public abstract class GameObjectPoolItem : MonoBehaviour, IPoolItem
    {
        public string Source { get; set; }

        public virtual void OnRecycle() { }

        public virtual void OnReset() { }
        public void SetPoolConfig(IPoolItemConfig _config = null) { }
    }
    public class GameObjectPool<T> : IPool where T : GameObjectPoolItem
    {
        #region Constructor

        public GameObjectPool(Transform _transform)
        {
            transform = _transform;
        }

        #endregion

        #region Private Variables

        [ShowInInspector] private Dictionary<string, (T Template, Transform Root, Queue<T> Queue, IPoolItemConfig Config)> pools = new();

        private readonly Transform transform;

        #endregion

        #region Public Methods

        public void Release(IPoolItem _item)
        {
            if (_item is not T item)
                return;
            if (pools.TryGetValue(item.Source, out var pool))
            {
                item.name = item.Source;
                _item.OnRecycle();
                item.OnReset();
                pool.Queue.Enqueue(item);
                var objTransform = item.transform;
                objTransform.SetParent(pool.Root);
                objTransform.localPosition = Vector3.zero;
            }
            else
            {
                this.Log($"名為{typeof(T).Name}({item.Source})的物件池不存在 無須回收 物件直接銷毀");
                Object.Destroy(item.gameObject);
            }
        }

        public void SpawnPrefabPool(T _prefab, IPoolItemConfig _config)
        {
            if (pools.TryGetValue(_prefab.name, out var pool))
            {
                this.Log("警告:此物件已生成物件池 無須生成", LogMessageType.Warning);
                return;
            }
            var o = new GameObject(_prefab.name);
            o.transform.SetParent(transform);
            o.transform.localPosition = Vector3.zero;
            var template = Object.Instantiate(_prefab, o.transform);
            template.transform.name = "Template";

            template.Source = _prefab.name;
            pools.Add(_prefab.name, (template, o.transform, new Queue<T>(), _config));
            var data = pools[_prefab.name];
            while (_config.Amount > data.Queue.Count)
                data.Queue.Enqueue(GetObject(_prefab.name, o.transform, true));
        }


        public bool TryGetObject(string _name, out T _result, Transform _parent)
        {
            _result = GetObject(_name, _parent);
            return _result != null;
        }

        private T GetObject(string _name, Transform _parent, bool _isInit = false)
        {
            if (!pools.ContainsKey(_name)) return null;
            var data = pools[_name];
            T result;
            if (!data.Queue.Any() || _isInit)
            {
                result = Object.Instantiate(data.Template);
                result.Source = _name;
                result.name = _name;
                result.SetPoolConfig(data.Config);
            }
            else
            {
                result = data.Queue.Dequeue();
            }
            if (_parent == null)
            {
                _parent = data.Root;
            }
            var resultTransform = result.transform;
            resultTransform.SetParent(_parent);
            resultTransform.localPosition = Vector3.zero;

            return result;
        }

        #endregion
    }
}