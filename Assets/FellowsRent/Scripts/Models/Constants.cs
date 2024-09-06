using System;
using FellowsRent.Controllers;

namespace FellowsRent.Models
{
    public static class Constants
    {
        public const string GAME_DATA_CONFIG_NAME = "GameConfigData";
        public const string APP_CUSTOM_ID_KEY = "appCustomId";
        public const string SECRET_KEY_KEY = "secretKey";
        public const string CLIENT_ID_KEY = "clientId";
        public const string TRACKING_ID_SUB_KEY = "custom_sub3";
        public const string SECRET_KEY_VALE = "b6facc527465b33001dc3dd8a272dc42da33d4584ad558b5e24bed9a986000ef";
        public const string VISIT_URL = "https://fellows.rent/api/applications/visit";
        public const string PUSH_ID_ORGANIC = "ORGANIC";
        public const int NOT_FOUND_CODE = 404;
        private const string DirectAdOpenEventURL = "https://fellows.rent/api/stat/postback?type=open&campaignId=";
        private const string Web2AppOpenEventURL = "https://fellows.rent/api/stat/postback?type=open&trackingId=";
        public static string NotificationSubscriptionURL => "https://fellows.rent/api/applications/clients/" + PlayerData.ClientID;

        public static string OpenEventURL
        {
            get
            {
                return PlayerData.UserType switch
                {
                    UserType.DirectAd => DirectAdOpenEventURL + PlayerData.CampaignID,
                    UserType.Web2AppAd => Web2AppOpenEventURL + PlayerData.ClientID,
                    _ => throw new Exception("NO OPEN EVENTS URL FOR NON AD USERS")
                };
            }
        }

    }
}