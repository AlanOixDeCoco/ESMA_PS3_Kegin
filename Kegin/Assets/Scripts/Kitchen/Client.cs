using ScriptableObjects.Ingredients;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Kitchen
{
    public class Client : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private SpriteRenderer _clientSprite;
        [SerializeField] private Image _requestImage;
        [SerializeField] private TextMeshProUGUI _requestText;

        [Header("Positions")]
        [SerializeField] private Transform _spawnPos;
        [SerializeField] private Transform _talkPos;
        [SerializeField] private Transform _exitPos;
        
        [Header("Events")]
        public UnityEvent _onClientHappy;
        public UnityEvent _onClientAngry;

        private IngredientSO _request;

        public IngredientSO Request => _request;

        public void Setup(Sprite clientTexture, IngredientSO request)
        {
            _clientSprite.sprite = clientTexture;
            _requestText.text = request.Name;
            _request = request;
            _requestImage.sprite = request.Sprite;
        }

        public void Sell(Transform context)
        {
            var inventory = context.GetComponent<Inventory>();
            var ingredientSO = inventory.Ingredients[0];
            
            if (ingredientSO == _request) _onClientHappy.Invoke();
            else _onClientAngry.Invoke();
            
            inventory.RemoveIngredient(ingredientSO);
        }
    }
}
