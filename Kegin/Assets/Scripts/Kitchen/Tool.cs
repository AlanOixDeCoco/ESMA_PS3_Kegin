using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Kitchen
{
    public class Tool : MonoBehaviour
    {
        private const float _cookingTime = 5f;
        
        [SerializeField] private CookingManager _cookingManager;
        [SerializeField] private ToolWorldUI _toolWorldUI;

        [SerializeField] private UnityEvent _onCookingBegin, _onCookingEnd;
        
        private Inventory _inventory;
        private Interactive _interactiveComponent;

        private void Start()
        {
            _inventory = GetComponent<Inventory>();
            _interactiveComponent = GetComponent<Interactive>();
        }

        public void StartCooking(ToolUI context)
        {
            if (!_interactiveComponent.Interactable) return;
            StartCoroutine(Cook(context));
        }

        private IEnumerator Cook(ToolUI context)
        {
            context.CloseUI();
            _toolWorldUI.Show(true);
            _toolWorldUI.SetProgress(0f);
            
            _onCookingBegin.Invoke();
            
            // Deactivate the interactive component
            _interactiveComponent.Interactable = false;
            
            float cookedFor = 0f;
            while (cookedFor < _cookingTime)
            {
                cookedFor += Time.deltaTime;
                
                // --> Update Tool cooking UI here
                _toolWorldUI.SetProgress(cookedFor/_cookingTime);
                
                yield return null;
            }
            
            // Get the ingredient that will be returned when the cooking is done
            var result = _cookingManager.GetPreparationResult(_inventory.Ingredients);
            _inventory.ClearInventory();
            _inventory.AddIngredient(result);
            
            _onCookingEnd.Invoke();
            
            _toolWorldUI.Show(false);
            
            // Re-activate the interactive component
            _interactiveComponent.Interactable = true;
        }
    }
}
