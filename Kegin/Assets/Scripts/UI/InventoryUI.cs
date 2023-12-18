using System.Threading.Tasks;
using ScriptableObjects.Ingredients;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(PanelComponent))]
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform _ingredientBtnsContainer;
    [SerializeField] private TMPro.TextMeshProUGUI _inventoryTitle;
    [SerializeField] private GameObject _ingredientPrefab;
    [SerializeField] private CursorManager _cursor;

    [SerializeField] private UnityEvent _beforeInventoryOpen;

    private Inventory _openedInventory;

    public Inventory OpenedInventory => _openedInventory;

    public async void OpenInventoryUI(Transform inventoryTransform)
    {
        _openedInventory = inventoryTransform.GetComponent<Inventory>();
        _inventoryTitle.text = OpenedInventory.Name;
        
        _beforeInventoryOpen.Invoke();

        foreach (var ingredient in OpenedInventory.Ingredients)
        {
            var ingredientBtn = _ingredientBtnsContainer.Find(ingredient.Name);
            
            ingredientBtn.gameObject.SetActive(true);

            // Set the ingredient count if stacked
            if (OpenedInventory.Stack)
            {
                ingredientBtn.Find("text_Count").GetComponent<TMPro.TextMeshProUGUI>().text = OpenedInventory.IngredientsQuantities[ingredient].ToString();
                ingredientBtn.Find("text_Count").gameObject.SetActive(true);
            }
            else ingredientBtn.Find("text_Count").gameObject.SetActive(false);

            await Task.Yield();
        }

        GetComponent<PanelComponent>().OpenPanel();
    }

    public async void CloseInventoryUI()
    {
        await GetComponent<PanelComponent>().ClosePanel();
        ClearInventoryUI();
        _cursor.SetInteractState();
    }

    private async void CloseInventoryUIForDrag(IngredientSO ingredientSO)
    {
        _cursor.SetDragState(ingredientSO, OpenedInventory);
        ClearInventoryUI();
        await GetComponent<PanelComponent>().ClosePanel();
    }

    private void ClearInventoryUI()
    {
        foreach (Transform child in _ingredientBtnsContainer)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void Setup(IngredientSO[] ingredientsSOs)
    {
        foreach (var ingredientSO in ingredientsSOs)
        {
            var ingredientBtn = Instantiate(_ingredientPrefab, _ingredientBtnsContainer).transform;
            ingredientBtn.name = ingredientSO.Name;
            ingredientBtn.Find("img_Ingredient").GetComponent<Image>().sprite = ingredientSO.Sprite;
            ingredientBtn.Find("text_Name").GetComponent<TextMeshProUGUI>().text = ingredientSO.Name;
            
            var draggable = ingredientBtn.GetComponent<Draggable>();
            draggable.Setup(ingredientSO);

            draggable._onBeginDrag.AddListener(CloseInventoryUIForDrag);
        }
        ClearInventoryUI();
    }
}
