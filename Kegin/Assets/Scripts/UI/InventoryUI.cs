using System.Threading.Tasks;
using ScriptableObjects.Ingredients;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PanelComponent))]
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform _ingredientBtnsContainer;
    [SerializeField] private TMPro.TextMeshProUGUI _inventoryTitle;
    [SerializeField] private GameObject _ingredientPrefab;
    [SerializeField] private CursorManager _cursor;

    private Inventory _openedInventory;

    private void Start()
    {
        ClearInventoryUI();
    }

    public async void OpenInventoryUI(Transform inventoryTransform)
    {
        _openedInventory = inventoryTransform.GetComponent<Inventory>();
        _inventoryTitle.text = _openedInventory.Name;

        foreach (var ingredient in _openedInventory.Ingredients)
        {
            var ingredientBtn = _ingredientBtnsContainer.Find(ingredient.Name);
            
            ingredientBtn.gameObject.SetActive(true);

            // Set the ingredient count if stacked
            if (_openedInventory.Stack)
            {
                ingredientBtn.Find("text_Count").GetComponent<TMPro.TextMeshProUGUI>().text = _openedInventory.IngredientsQuantities[ingredient].ToString();
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
        _cursor.SetDragState(ingredientSO, _openedInventory);
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

            var draggable = ingredientBtn.GetComponent<Draggable>();
            draggable.Setup(ingredientSO);

            draggable._onBeginDrag.AddListener(CloseInventoryUIForDrag);
        }
    }
}
