using FellowsRent.Models;
using UnityEngine;

namespace FellowsRent.Controllers
{
    //TODO. In future would be cool to replace this shitty P Prefs to json
    public static class PlayerData
    {
        //TODO Extract to "Keys" class
        private const string URL_KEY = "URL";
        private const string CLIENT_ID_KEY = "CLIENT_ID";
        private const string CAMPAIGN_ID_KEY = "CAMPAIGN_ID";
        private const string PUSH_ID_KEY = "PUSH_ID";
        private const string IS_FIRST_OPEN_KEY = "FIRST_OPEN";
        private const string IS_TARGET_USER_KEY = "TARGET_USER";
        private const string USER_TYPE_KEY = "USER_TYPE";
        
        public static UserType UserType
        {
            get => (UserType)PlayerPrefs.GetInt(USER_TYPE_KEY);
            private set => PlayerPrefs.SetInt(USER_TYPE_KEY, (int)value);
        }

        public static string PushID
        {
            get => PlayerPrefs.GetString(PUSH_ID_KEY);
            set => PlayerPrefs.SetString(PUSH_ID_KEY, value);
        }

        public static string CampaignID
        {
            get => PlayerPrefs.GetString(CAMPAIGN_ID_KEY);
            set => PlayerPrefs.SetString(CAMPAIGN_ID_KEY, value);
        }

        public static string URL
        {
            get => PlayerPrefs.GetString(URL_KEY);
            set => PlayerPrefs.SetString(URL_KEY, value);
        }

        public static string ClientID
        {
            get => PlayerPrefs.GetString(CLIENT_ID_KEY);
            set => PlayerPrefs.SetString(CLIENT_ID_KEY, value);
        }


        public static bool IsFirstOpen
        {
            get
            {
                if (PlayerPrefs.HasKey(IS_FIRST_OPEN_KEY))
                {
                    return false;
                }

                PlayerPrefs.SetInt(IS_FIRST_OPEN_KEY, 1);
                return true;
            }
        }

        public static bool IsTargetUser => PlayerPrefs.HasKey(IS_TARGET_USER_KEY);
        
        public static void SetAsTargetUser(UserType userType)
        {
            PlayerPrefs.SetInt(IS_TARGET_USER_KEY,1);
            UserType = userType;
        }
    }
}