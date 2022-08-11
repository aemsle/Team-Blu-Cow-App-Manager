using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Reflection;
using System;

namespace blu
{
    public class App : MonoBehaviour
    {
        // Singleton instance
        [HideInInspector] private static App instance = null;

        private List<blu.Module> _modules = new List<blu.Module>();
        private blu.ModuleManager _moduleManager = new blu.ModuleManager();

        [HideInInspector] public static List<blu.Module> LoadedModules { get => instance._modules; }
        [HideInInspector] public static Transform Transform { get => instance.transform; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            AddBaselineModules();
        }

        private void AddBaselineModules()
        {
            AddModule<SceneModule>();
        }

        public static T GetModule<T>() where T : blu.Module
        {
            return instance._moduleManager.GetModule<T>();
        }

        public static void AddModule<T>() where T : blu.Module
        {
            instance._moduleManager.AddModule<T>();
        }

        public static void RemoveModule<T>() where T : blu.Module
        {
            instance._moduleManager.RemoveModule<T>();
        }

        private void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            foreach (var module in _modules)
            {
                Handles.DrawAAPolyLine(transform.position, module.transform.position);
            }
#endif
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            Gizmos.DrawIcon(Transform.position, nameof(App), true);
#endif
        }
    }
}