using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _info;
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _privacyPolicy;
    [SerializeField] private Loading _loading;
    [SerializeField] private TextMeshProUGUI _bestScore;

    private void Awake()
    {
        _bestScore.text = "Best score: " + Saver.GetInt("BestScore", 0).ToString() + "M";
        ClosePrivacyPolicy();
        CloseInfo();
        CloseShop();
        _loading.gameObject.SetActive(false);
    }
    public void StartGame()
    {
        _loading.LoadGame();
    }
    public void OpenInfo()
    {
        _info.SetActive(true);
    }
    public void OpenShop()
    {
        _shop.SetActive(true);
    }
    public void OpenPrivacyPolicy()
    {
        _privacyPolicy.SetActive(true);
    }
    public void CloseInfo()
    {
        _info.SetActive(false);
    }
    public void CloseShop()
    {
        _shop.SetActive(false);
    }
    public void ClosePrivacyPolicy()
    {
        _privacyPolicy.SetActive(false);
    }
}
