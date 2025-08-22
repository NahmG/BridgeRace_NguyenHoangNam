using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Core
{
    using Sensor;
    using Navigation;
    using Movement;
    using Display;

    public abstract class CoreSystem : MonoBehaviour
    {
        [SerializeField]
        List<BaseCore> cores;

        Dictionary<Type, BaseCore> compDict = new();

        public CharacterStats Stats { get; private set; }

        public MovementCore MOVEMENT => GetCoreComponent<MovementCore>();

        public DisplayCore DISPLAY => GetCoreComponent<DisplayCore>();

        public NavigationCore NAVIGATION => GetCoreComponent<NavigationCore>();

        public SensorCore SENSOR => GetCoreComponent<SensorCore>();

        public virtual void Initialize(CharacterStats stats)
        {
            Stats = stats;

            SENSOR.ReceiveInfo(NAVIGATION);
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
                var comp = cores.FirstOrDefault() as T ??
                        GetComponentInChildren<T>() ??
                        new GameObject().AddComponent<T>();

                comp.transform.SetParent(transform);
                comp.Initialize();
                compDict[type] = comp;
            }

            return compDict[type] as T;
        }
    }
}
