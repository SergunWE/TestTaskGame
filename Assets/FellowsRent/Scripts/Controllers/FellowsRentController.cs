using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AffiseAttributionLib.Modules;
using fbg;
using FellowsRent.Models;
using FellowsRent.Utilities;
using Newtonsoft.Json;
using OneSignalSDK;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;
using Response = FellowsRent.Models.UserMatch.Response;

namespace FellowsRent.Controllers
{
    public class FellowsRentController : MonoBehaviour
    {
        [SerializeField] private LoadingController _loadingController;
        private GameDataConfig _gameDataConfig;
        
        private void Start()
        {
            _gameDataConfig = StaticDataController.GameDataConfig;
            _loadingController.StartLoading();

            if (PlayerData.IsFirstOpen)
            {
                AffiseController.Initialize(OnAffiseKeyValueCompleted);
            }
            else if (PlayerData.IsTargetUser)
            {
                AffiseController.Initialize(null);
                if (PlayerData.UserType == UserType.Organic)
                {
                    LoadOffer(_gameDataConfig.OrganicUrl, PlayerData.UserType);
                }
                else
                {
                    FacebookController.Initialize();
                    LoadOffer(PlayerData.URL, PlayerData.UserType);
                    RegisterOpenEvent();
                    
                }
            }
            else
            {
                _loadingController.StopLoading();
            }
            
        }


        private void OnAffiseKeyValueCompleted(List<AffiseKeyValue> data)
        {
            var clientId = data.FirstOrDefault(t => t.Key == Constants.TRACKING_ID_SUB_KEY)?.Value;
            TryMatchUser(OnUserMatched, OnUserMatchingFailed, clientId);
        }
        
        //TODO. NOT ASAP. Dont forget to change this one to async and remove onUserMatchedFailed. It is "try" method, it should return bool, not just callbacks. 
        private void TryMatchUser(Action<Response> onUserMatchedSuccessfully, Action onUserMatchingFailed, string clientId)
        {
            StartCoroutine(SendMatchRequest());

            IEnumerator SendMatchRequest()
            {
                WWWForm form = new WWWForm();
                form.AddField(Constants.APP_CUSTOM_ID_KEY, Application.identifier);
                form.AddField(Constants.SECRET_KEY_KEY, Constants.SECRET_KEY_VALE);
                if (!string.IsNullOrEmpty(clientId))
                {
                    form.AddField(Constants.CLIENT_ID_KEY, clientId);
                }

                UnityWebRequest uwr = UnityWebRequest.Post(Constants.VISIT_URL, form);
                yield return uwr.SendWebRequest();

                if (uwr.error != null)
                {
                    onUserMatchingFailed?.Invoke();
                }
                else
                {
                    try
                    {
                        var response = JsonConvert.DeserializeObject<Response>(uwr.downloadHandler.text);

                        if (FellowsRentUtilities.IsLinkForDirectAd(response.data.offerUrl))
                        {
                            response.data.offerUrl = FellowsRentUtilities.AddIDFATracking(response.data.offerUrl);
                        }
                        
                        onUserMatchedSuccessfully(response);
                        RegisterOpenEvent();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                    }
                }
            }
        }

        private void OnUserMatched(Response response)
        {
            OneSignal.Initialize(_gameDataConfig.OneSignalID);
            OneSignal.User.AddTags(new Dictionary<string, string>()
            {
                { Application.identifier, response.data.campaignId },
                { response.data.campaignId, response.data.clientId }
            });
            OneSignal.Notifications.RequestPermissionAsync(true);
            PlayerData.URL = response.data.offerUrl;
            PlayerData.ClientID = response.data.clientId;
            PlayerData.CampaignID = response.data.campaignId;
            FacebookController.Initialize();
            LoadOffer(PlayerData.URL, FellowsRentUtilities.GetUserType(PlayerData.URL));
            SubscribeForNotifications();
        }
        
        private void OnUserMatchingFailed()
        {
            OneSignal.Initialize(_gameDataConfig.OneSignalID);
            OneSignal.User.AddTag(Constants.PUSH_ID_ORGANIC, Constants.PUSH_ID_ORGANIC);
            OneSignal.Notifications.RequestPermissionAsync(true);
            LoadOffer(_gameDataConfig.OrganicUrl, UserType.Organic);
        }
        
        private void LoadOffer(string offerUrl, UserType userType)
        {
            WebViewController.Instance.OnLoadingErrorReceived += (code, message) =>
            {
                if (!PlayerData.IsTargetUser)
                {
                    _loadingController.StopLoading();
                    WebViewController.Instance.Hide();
                }
            };
            WebViewController.Instance.OnPageLoadedSuccessfully += (code, url) =>
            {
                PlayerData.SetAsTargetUser(userType);
            };

            WebViewController.Instance.OnPageLoadedWithError += (code, url) =>
            {
                if (!PlayerData.IsTargetUser)
                {
                    _loadingController.StopLoading();
                    WebViewController.Instance.Hide();
                }
            };

            WebViewController.Instance.LoadURL(offerUrl);
        }
        
        private void SubscribeForNotifications()
        {
            StartCoroutine(SubscribeForNotifications());

            IEnumerator SubscribeForNotifications()
            {
                WWWForm form = new WWWForm();
                form.AddField(Constants.SECRET_KEY_KEY, Constants.SECRET_KEY_VALE);

                UnityWebRequest uwr = UnityWebRequest.Post(Constants.NotificationSubscriptionURL, form);
                yield return uwr.SendWebRequest();
            }
        }
        
        private void RegisterOpenEvent()
        {
            StartCoroutine(RegisterOpenEvent());

            IEnumerator RegisterOpenEvent()
            {
                string url = Constants.OpenEventURL;
                UnityWebRequest uwr = UnityWebRequest.Get(url);
                yield return uwr.SendWebRequest();
            }
        }
    }
}