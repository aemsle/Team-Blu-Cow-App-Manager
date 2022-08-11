using System;
using UnityEngine;

namespace blu
{
    public class Bootstrap : MonoBehaviour
    {
        public class BootstrapException : ApplicationException
        {
            public BootstrapException()
            {
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void loadApplication()
        {
            // create app
            GameObject app = new GameObject("App", typeof(blu.App));

            // null check game object
            if (app == null)
            {
                throw new BootstrapException();
            }

            // null check script
            if (app.GetComponent<blu.App>() == null)
            {
                throw new BootstrapException();
            }

            DontDestroyOnLoad(app);
        }
    }
}