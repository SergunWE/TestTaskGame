using System;
using System.Collections.Generic;
using AffiseAttributionLib;
using AffiseAttributionLib.Modules;

namespace FellowsRent.Controllers
{
    public static class AffiseController
    {
        public static void Initialize(OnKeyValueCallback onKeyValueCompleted)
        {
            var gameDataConfig = StaticDataController.GameDataConfig;
            Affise
                .Settings(
                    affiseAppId: gameDataConfig.AffiseAppID, //Change to your app id
                    secretKey: gameDataConfig.AffiseAppID //Change to your SDK secretKey
                )
                .Start(); // Start Affise SDK
            Affise.Module.ModuleStart(AffiseModules.Status);
            Affise.Module.ModuleStart(AffiseModules.Advertising);
            Affise.SetTrackingEnabled(true); // to enable tracking
            Affise.SetBackgroundTrackingEnabled(true); // to enable background tracking
            if (onKeyValueCompleted != null)
            {
                Affise.Module.GetStatus(AffiseModules.Status, onKeyValueCompleted);
            }
            Affise.IOS.RegisterAppForAdNetworkAttribution((error) =>
            {
                UnityEngine.Debug.LogError(error);
            });
            Affise.RegisterDeeplinkCallback((value) =>
            {
                UnityEngine.Debug.LogError(value);
            });  
        }
    }
}