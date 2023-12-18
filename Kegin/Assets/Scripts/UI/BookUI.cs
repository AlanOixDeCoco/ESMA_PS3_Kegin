using System.Collections.Generic;
using Managers;
using ScriptableObjects.Preparations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookUI : MonoBehaviour
{
    [SerializeField] private SaveManager _saveManager;
    [SerializeField] private BookWorldUI _bookWorldUI;

    [SerializeField] private Image _resultImage;
    [SerializeField] private TextMeshProUGUI _resultName;
    [SerializeField] private Transform _ingredientImagesContainer;
    [SerializeField] private CursorManager _cursor;
    
    private PanelComponent _panelComponent;
    private int _currentPreparationIndex = 0;
    private List<PreparationSO> _unlockedPreparationsSOs;

    public Image ResultImage => _resultImage;

    public TextMeshProUGUI ResultName => _resultName;

    public Transform IngredientImagesContainer => _ingredientImagesContainer;

    private void Start()
    {
        _panelComponent = GetComponent<PanelComponent>();
        
        ClearBookUI();
    }

    public void OpenBookUI()
    {
        _unlockedPreparationsSOs = _saveManager.PlayerSave.UnlockedPreparationsSOs;

        if (_unlockedPreparationsSOs.Count <= 0) return;
        
        Debug.Log("There is more than 0 preps unlocked!");
        
        _cursor.SetUIState();
        
        ShowPreparation(_currentPreparationIndex);
        
        _panelComponent.OpenPanel();
    }
    
    public async void CloseBookUI()
    {
        await _panelComponent.ClosePanel();
        _cursor.SetInteractState();
    }

    private void ClearBookUI()
    {
        foreach (Transform child in IngredientImagesContainer)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void ShowPreparation(int preparationIndex)
    {
        ClearBookUI();
        
        ResultImage.sprite = _unlockedPreparationsSOs[preparationIndex].ResultIngredientSO.Sprite;
        ResultName.text = _unlockedPreparationsSOs[preparationIndex].ResultIngredientSO.Name;

        int imageIndex = 0;
        foreach (var ingredientSO in _unlockedPreparationsSOs[preparationIndex].Ingredients)
        {
            var ingredientImage = IngredientImagesContainer.GetChild(imageIndex).GetChild(0);
            ingredientImage.GetComponent<Image>().sprite = ingredientSO.Sprite;
            ingredientImage.parent.gameObject.SetActive(true);
            imageIndex++;
        }
        
        _bookWorldUI.UpdateUI(this);
    }
    
    public void ShowNextPreparation()
    {
        if (_unlockedPreparationsSOs.Count < 2) return;
        
        _currentPreparationIndex++;
        if (_currentPreparationIndex >= _unlockedPreparationsSOs.Count) _currentPreparationIndex = 0;
        
        ShowPreparation(_currentPreparationIndex);
    }
    
    public void ShowPreviousPreparation()
    {
        if (_unlockedPreparationsSOs.Count < 2) return;
        
        _currentPreparationIndex--;
        if (_currentPreparationIndex < 0) _currentPreparationIndex = _unlockedPreparationsSOs.Count - 1;
        
        ShowPreparation(_currentPreparationIndex);
    }
}
