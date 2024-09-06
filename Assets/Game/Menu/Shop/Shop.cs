using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ArtifactsWallet _artifactsWallet;
        [SerializeField] private PriceView _priceView;

        private ItemSkeensShop[] _itemSkeens;

        private ItemSkeensShop _currentShop;

        private void Awake()
        {
            _artifactsWallet.Init();
            _itemSkeens = GetComponentsInChildren<ItemSkeensShop>();
            for (int i = 0; i < _itemSkeens.Length; i++)
            {
                _itemSkeens[i].Init(_artifactsWallet, _priceView);
                _itemSkeens[i].Close();
            }
            _currentShop = _itemSkeens[0];
            _currentShop.Open();
        }
        public void OpenItemSkeensShop(ItemSkeensShop skeensShop)
        {
            _currentShop = skeensShop;
            skeensShop.Open();
            for(int i = 0; i < _itemSkeens.Length; i++)
            {
                if(skeensShop.ItemName != _itemSkeens[i].ItemName)
                {
                    _itemSkeens[i].Close();
                }
            }
        }
        public void ShowNextSkeen()
        {
            _currentShop.ShowNextSkeen();
        }
        public void ShowPreviousSkeen()
        {
            _currentShop.ShowPreviousSkeen();
        }
    }
}

