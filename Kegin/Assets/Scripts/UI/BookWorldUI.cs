using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookWorldUI : MonoBehaviour
{
    [SerializeField] private Image _resultImage;
    [SerializeField] private TextMeshProUGUI _resultName;
    [SerializeField] private Transform _ingredientImagesContainer;

    public void UpdateUI(BookUI bookUI)
    {
        _resultImage.sprite = bookUI.ResultImage.sprite;
        //_resultName.text = bookUI.ResultName.text;
    }
}
