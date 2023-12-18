using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookWorldUI : MonoBehaviour
{
    [SerializeField] private Image _resultImage;
    [SerializeField] private Transform _ingredientImagesContainer;

    public Image ResultImage => _resultImage;
    public Transform IngredientImagesContainer => _ingredientImagesContainer;
}
