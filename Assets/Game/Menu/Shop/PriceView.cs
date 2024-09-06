using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PriceView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _priceOutput;
    [SerializeField] private Image _artifactImg;
    [SerializeField] private ArtifactViewData[] _artifactsData;

    public void ShowPrice(int price, string artifact)
    {
        if (price == 0)
        {
            Disable();
        }
        else
        {
            Enable();
        }
        _priceOutput.text = price.ToString();
        for (int i = 0; i < _artifactsData.Length; i++)
        {
            if (_artifactsData[i].Name == artifact)
            {
                _artifactImg.sprite = _artifactsData[i].Sprite;
            }
        }
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    public void Enable()
    {
        gameObject.SetActive(true);
    }
    [Serializable]
    struct ArtifactViewData
    {
        public Sprite Sprite;
        public string Name;
    }
}
