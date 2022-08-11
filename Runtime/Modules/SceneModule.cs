using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

namespace blu
{
    public class SceneModule : Module
    {
        [HideInInspector] public float loadProgess = 0f;
        [HideInInspector] public bool switching = false;

        public void SwitchScene(string in_scene)
        {
            if (switching)
                return;
            else
                switching = true;

            StartCoroutine(LoadLevel(in_scene));
        }

        private IEnumerator LoadLevel(string in_scene, float in_delay = 0f, bool test = false)
        {
            yield return new WaitForSeconds(in_delay); // allow for animation to trigger

            AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(in_scene); // begin async scene swap after intro if any

            if (sceneLoad == null) // catch any invalid scene switch calls
            {
                Debug.Log("[App/SceneModule]: Unable to load scene: " + in_scene);
                Debug.Log("[App/SceneModule]: Wrong or missing index?");
                yield break;
            }

            if (!test)
            {
                while (!sceneLoad.isDone) // report the progress of the scene load
                {
                    loadProgess = Mathf.Clamp01(sceneLoad.progress / 0.9f); // 0f - 0.9f -> 0f - 1f
                    yield return null;
                }
            }
            else
            {
                float currentTime = 0f; // Loading bar test code
                while (currentTime < 3f) // report the progress of the scene load
                {
                    currentTime += Time.deltaTime;
                    yield return null;
                }
            }

            yield return new WaitForSeconds(in_delay); // allow for animation to trigger

            switching = false;

            yield break;
        }

        public void Quit()
        {
            Debug.Log("[App]: Quitting");
            UnityEngine.Application.Quit();
        }

        public override void Initialize()
        {
            Debug.Log("[App]: Initializing scene module");
        }
    }
}