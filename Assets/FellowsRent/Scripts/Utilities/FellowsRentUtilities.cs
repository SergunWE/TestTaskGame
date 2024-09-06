using System;
using AffiseAttributionLib;
using FellowsRent.Models;
using UnityEngine.iOS;

namespace FellowsRent.Utilities
{
    public static class FellowsRentUtilities
    {
        public static bool IsLinkForDirectAd(string link)
        {
            return !link.Contains("tracking_id");
        }
        
        public static string AddIDFATracking(string link)
        {
            if (!link.Contains("?"))
            {
                link += "?";
            }

            link += $"&idfa={Device.advertisingIdentifier}&device={Affise.GetRandomDeviceId()}&user={Affise.GetRandomUserId()}";

            return link;
        }

        public static UserType GetUserType(string url)
        {
            if (url.Contains("tracking_id"))
            {
                return UserType.Web2AppAd;
            }

            if (url.Contains("idfa"))
            {
                return UserType.DirectAd;
            }

            if (!string.IsNullOrEmpty(url))
            {
                return UserType.Organic;
            }

            return UserType.WhitePlayer;
        }
    }
}