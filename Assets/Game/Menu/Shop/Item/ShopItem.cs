using UnityEngine;

namespace Shop
{
    public class ShopItem : MonoBehaviour
    {

        [SerializeField] private string _name;
        [SerializeField] private GameObject _buyBtn;
        [SerializeField] private GameObject _selectBtn;
        [SerializeField] private int _price;
        [SerializeField] private string _artifactType;

        private ArtifactsWallet _wallet;
        public string ArtifactType => _artifactType;
        public int Price => _price;
        public string Type { get; private set; }
        public bool IsPurchased { get; private set; }


        public void Init(ArtifactsWallet wallet, string type)
        {
            _wallet = wallet;
            Type = type;
            IsPurchased = Saver.GetBool(Type + _name, false);
            if (_name == "0")
            {
                IsPurchased = true;
                Saver.SaveBool(true, Type + _name);
                _buyBtn.SetActive(false);
                _selectBtn.SetActive(true);
            }

            if (IsPurchased)
            {
                _buyBtn.SetActive(false);
                _selectBtn.SetActive(true);
            }
            else
            {
                _buyBtn.SetActive(true);
                _selectBtn.SetActive(false);
            }
        }
        public void Buy()
        {
            bool isPurchased = _wallet.TryRemoveArtifacts(_artifactType, _price);
            if (isPurchased)
            {
                IsPurchased = true;
                Saver.SaveBool(true,Type + _name);
                _buyBtn.SetActive(false);
                _selectBtn.SetActive(true);
            }
        }
        public void Select()
        {
            Saver.SaveString(_name, Type);
        }
    }
}