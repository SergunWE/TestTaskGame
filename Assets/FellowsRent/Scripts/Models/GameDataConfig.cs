using System;
using UnityEngine;

namespace FellowsRent.Models
{
    [CreateAssetMenu(menuName = "Fellows Rent/Game Data Config")]
    public class GameDataConfig : ScriptableObject
    {
        [SerializeField] private string _organicUrl;
        [SerializeField] private string _oneSignalID;
        [SerializeField] private string _policyLink;
        [SerializeField] private string _affiseAppID;
        [SerializeField] private string _affiseAppSecretKey;
        public string OrganicUrl => _organicUrl;
        public string OneSignalID => _oneSignalID;
        public string PolicyLink => _policyLink;
        public string AffiseAppID => _affiseAppID;
        public string AffiseAppSecretKey => _affiseAppSecretKey;

    }
}
