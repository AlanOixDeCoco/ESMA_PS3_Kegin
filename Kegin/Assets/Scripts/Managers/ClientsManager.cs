using System;
using Kitchen;
using ScriptableObjects.Ingredients;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Managers
{
    public class ClientsManager : MonoBehaviour
    {
        [Header("Client")]
        [SerializeField] private Client _client;

        [SerializeField] private Image _orderImage;
        [SerializeField] private Sprite[] _clientsTextures;

        [Header("Requests")]
        [SerializeField] private IngredientSO[] _ingredientsSOs;

        private void Start()
        {
            SpawnNewClient();
        }

        public void SpawnNewClient()
        {
            var newClientTexture = _clientsTextures[Random.Range(0, _clientsTextures.Length)];
            var newClientRequest = _ingredientsSOs[Random.Range(0, _ingredientsSOs.Length)];
            _client.Setup(newClientTexture, newClientRequest);
            _client.gameObject.SetActive(true);

            _orderImage.sprite = newClientRequest.Sprite;
        }
    }
}
