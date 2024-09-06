using Facebook.Unity;
using UnityEngine;

namespace FellowsRent.Controllers
{
    public static class FacebookController
    {
        public static void Initialize()
        {
            if (!FB.IsInitialized)
            {
                FB.Init(InitializeCallback, null);
            }
            else
            {
                FB.ActivateApp();
            }
        }

        private static void InitializeCallback()
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                Debug.Log("Failed to Initialize the Facebook SDK");
            }
        }

    }
}