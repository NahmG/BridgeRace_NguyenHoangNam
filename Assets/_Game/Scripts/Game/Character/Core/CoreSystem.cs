using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Core
{
    using Sensor;

    public abstract class CoreSystem : MonoBehaviour
    {
        [SerializeField]
        List<BaseCore> cores;
        Dictionary<Type, BaseCore> compDict = new();

        public CharacterStats Stats { get; private set; }

        public MovementCore MOVE => GetCoreComponent<MovementCore>();

        public DisplayCore DIS => GetCoreComponent<DisplayCore>();

        public NavigationCore NAV => GetCoreComponent<NavigationCore>();

        public SensorCore SEN => GetCoreComponent<SensorCore>();

        public virtual void Initialize(CharacterStats stats)
        {
            Stats = stats;

            SEN.ReceiveInfo(NAV);
        }

        public virtual void UpdateData()
        {
            foreach (var key in compDict.Keys)
            {
                compDict[key].UpdateData();
            }
        }

        public virtual void FixedUpdate()
        {
            foreach (var key in compDict.Keys)
            {
                compDict[key].FixedUpdateData();
            }
        }

        T GetCoreComponent<T>() where T : BaseCore
        {
            var type = typeof(T);
            if (!compDict.ContainsKey(type) || compDict[type] == null)
            {
                var comp = cores.FirstOrDefault() as T;
                comp.Initialize();
                compDict[type] = comp;
            }

            return compDict[type] as T;
        }
    }
}
