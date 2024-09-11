using System;
using System.Collections.Generic;
using AffiseAttributionLib;
using AffiseAttributionLib.Modules;
using UnityEngine;

namespace FellowsRent.Controllers
{
    public class AffiseController : MonoBehaviour
    {
        public static void Initialize(OnKeyValueCallback onKeyValueCompleted)
        {
            var gameDataConfig = StaticDataController.GameDataConfig;
            Affise
                .Settings(
                    affiseAppId: gameDataConfig.AffiseAppID,
                    secretKey: gameDataConfig.AffiseAppSecretKey
                )
                .Start();
            Affise.Module.ModuleStart(AffiseModules.Advertising);
            if (onKeyValueCompleted != null)
            {
                Debug.Log("Try get status");
                Affise.Module.GetStatus(AffiseModules.Status, onKeyValueCompleted);
            }
            Affise.Debug.Validate(status =>
            {
                Debug.Log(status);
            });
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