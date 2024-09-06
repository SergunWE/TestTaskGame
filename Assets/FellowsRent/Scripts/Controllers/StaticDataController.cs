using System;
using System.Linq;
using FellowsRent.Models;
using UnityEngine;

namespace FellowsRent.Controllers
{
    public class StaticDataController : MonoBehaviour
    {
        public static GameDataConfig GameDataConfig;
        
        private void Awake()
        {
            GameDataConfig = Resources.LoadAll<GameDataConfig>("").First();
        }
    }
}