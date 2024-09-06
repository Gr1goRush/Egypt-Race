using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Shop
{
    public class ItemSkeensShop : MonoBehaviour
    {
        [SerializeField] private string _itemName;
        [SerializeField] private Transform _context;

        private Vector3 _startPos;
        private PriceView _priceView;
        private int _currentSkeen;
        private ArtifactsWallet _wallet;
        private ShopItem[] _skeens;

        public string ItemName => _itemName;

        public void Init(ArtifactsWallet wallet, PriceView priceView)
        {
            _priceView = priceView;
            _startPos = _context.localPosition;
            _wallet = wallet;
            _skeens = GetComponentsInChildren<ShopItem>();
            for (int i = 0; i < _skeens.Length; i++)
            {
                _skeens[i].Init(wallet, _itemName);
            }
            _priceView.Disable();
        }
        public void ShowNextSkeen()
        {
            _currentSkeen += 1;
            if(_currentSkeen >= _skeens.Length)
            {
                _currentSkeen = 0;
            }
            SetPosition();
            ShowPrice();
        }
        public void ShowPreviousSkeen()
        {
            _currentSkeen -= 1;
            if (_currentSkeen < 0)
            {
                _currentSkeen = _skeens.Length - 1;
            }
            SetPosition();
            ShowPrice();
        }
        private void ShowPrice()
        {
            ShopItem skeen = _skeens[_currentSkeen];
            if (skeen.IsPurchased)
            {
                _priceView.Disable();
                return;
            }
            _priceView.ShowPrice(skeen.Price, skeen.ArtifactType);
        }
        private void SetPosition()
        {
            ShopItem item = _skeens[_currentSkeen];
            Vector3 position = _startPos;
            position.x -= item.transform.localPosition.x;
            _context.localPosition = position;
        }
        public void Open()
        {
            gameObject.SetActive(true);
            SetPosition();
            ShowPrice();
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}