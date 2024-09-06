using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Android;

namespace FellowsRent.Controllers
{
    public delegate void LoadingErrorReceivedDelegate(int errorCode, string errorMessage);

    public delegate void PageLoadedSuccessfullyDelegate(int responseCode, string url);

    public delegate void PageLoadedWithErrorDelegate(int responseCode, string url);
    
    public class WebViewController : MonoBehaviour
    {
        public event PageLoadedSuccessfullyDelegate OnPageLoadedSuccessfully;
        public event PageLoadedWithErrorDelegate OnPageLoadedWithError;
        public event LoadingErrorReceivedDelegate OnLoadingErrorReceived;

        [SerializeField] private GameObject _blackScreen;
        private UniWebView _uniWebView;
        private Coroutine _updateOrientationCoroutine;

        public static WebViewController Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            UniWebView.SetAllowAutoPlay(true);
            UniWebView.SetAllowInlinePlay(true);
        }

        public void LoadURL(string url)
        {
            if (_uniWebView == null)
            {
                _uniWebView = gameObject.AddComponent<UniWebView>();
                _uniWebView.Frame = new Rect(0, 0, Screen.safeArea.width, Screen.safeArea.height);
                _uniWebView.SetAllowBackForwardNavigationGestures(true);
                _uniWebView.OnLoadingErrorReceived += (view, code, message, payload) => OnLoadingErrorReceived?.Invoke(code, message);
                _uniWebView.OnShouldClose += view => false;
                _uniWebView.OnOrientationChanged += (view, orientation) => 
                {
                    if (_updateOrientationCoroutine != null)
                    {
                        StopCoroutine(_updateOrientationCoroutine);
                    }
                    StartCoroutine(UpdateOrientation());
                };
                _uniWebView.EmbeddedToolbar.Show();
                _uniWebView.EmbeddedToolbar.SetDoneButtonText("");
                _uniWebView.EmbeddedToolbar.SetBackgroundColor(Color.black);
                _uniWebView.RegisterOnRequestMediaCapturePermission((permission) => UniWebViewMediaCapturePermissionDecision.Grant);
                _uniWebView.OnPageFinished += (view, code, url) =>
                {
                    if (code != Models.Constants.NOT_FOUND_CODE)
                    {
                        SetAutorotation();
                        _blackScreen.SetActive(true);
                        OnPageLoadedSuccessfully?.Invoke(code, url);
                        _uniWebView.Show();
                    }
                    else
                    {
                        OnPageLoadedWithError?.Invoke(code, url);
                        _blackScreen.SetActive(false);
                    }
                };
            }

            _uniWebView.Load(url);
        }

        private IEnumerator UpdateOrientation()
        {
            yield return new WaitForSeconds(0.1f);
            _uniWebView.Frame = new Rect(0, 0, Screen.safeArea.width, Screen.safeArea.height);
        }
        
        public void LoadSafeURL(string url)
        {
            UniWebViewSafeBrowsing.Create(url).Show();
        }

        private void RequestCameraAccess()
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
                Permission.RequestUserPermission(Permission.Camera,null);
        }

        private void SetAutorotation()
        {
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }

        public void Hide()
        {
            _uniWebView.Hide();
        }
    }
}