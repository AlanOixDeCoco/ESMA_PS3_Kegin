using System.Collections.Generic;
using Managers;
using ScriptableObjects.Preparations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookUI : MonoBehaviour
{
    [SerializeField] private SaveManager _saveManager;

    [SerializeField] private Image _resultImage, _worldResultImage;
    [SerializeField] private TextMeshProUGUI _resultName;
    [SerializeField] private Transform _ingredientImagesContainer, _worldIngredientImagesContainer;
    [SerializeField] private CursorManager _cursor;
    
    private PanelComponent _panelComponent;
    private int _currentPreparationIndex = 0;
    private List<PreparationSO> _unlockedPreparationsSOs;

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
        foreach (Transform child in _ingredientImagesContainer)
        {
            child.gameObject.SetActive(false);
        }
        
        foreach (Transform child in _worldIngredientImagesContainer)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void DisplayNewPreparation()
    {
        _unlockedPreparationsSOs = _saveManager.PlayerSave.UnlockedPreparationsSOs;
        _currentPreparationIndex = _unlockedPreparationsSOs.Count - 1;
        OpenBookUI();
    }

    private void ShowPreparation(int preparationIndex)
    {
        ClearBookUI();
        
        // Screen UI
        _resultImage.sprite = _unlockedPreparationsSOs[preparationIndex].ResultIngredientSO.Sprite;
        _resultName.text = _unlockedPreparationsSOs[preparationIndex].ResultIngredientSO.Name;
        
        // World UI
        _worldResultImage.sprite = _resultImage.sprite;
        _worldResultImage.enabled = true;

        int imageIndex = 0;
        foreach (var ingredientSO in _unlockedPreparationsSOs[preparationIndex].Ingredients)
        {
            // Screen UI
            var ingredientImage = _ingredientImagesContainer.GetChild(imageIndex).GetChild(0);
            ingredientImage.GetComponent<Image>().sprite = ingredientSO.Sprite;
            ingredientImage.parent.gameObject.SetActive(true);
            
            // World UI
            ingredientImage = _worldIngredientImagesContainer.GetChild(imageIndex).GetChild(0);
            ingredientImage.GetComponent<Image>().sprite = ingredientSO.Sprite;
            ingredientImage.parent.gameObject.SetActive(true);
            
            imageIndex++;
        }
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
