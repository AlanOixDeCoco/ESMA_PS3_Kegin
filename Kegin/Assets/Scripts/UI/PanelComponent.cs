using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PanelComponent : MonoBehaviour
{
    private const float PanelOpenDuration = .2f;

    [SerializeField] private Transform _panelArea;

    [SerializeField] private Color _openColor;

    private Image _backgroundImage;
    private Button _backgroundButton;
    
    private void Awake()
    {
        if (_panelArea == null) _panelArea = transform.GetChild(0);
        
        _backgroundImage = GetComponent<Image>();
        _backgroundButton = GetComponent<Button>();
        _backgroundImage.color = Color.clear;
    }

    public async void OpenPanel()
    {
        _panelArea.gameObject.SetActive(true);
        _backgroundButton.enabled = false;
        _backgroundImage.enabled = true;
        float scale = 0;
        while (scale + Time.deltaTime / PanelOpenDuration < 1)
        {
            scale += Time.deltaTime / PanelOpenDuration;
            _panelArea.localScale = Vector3.one * scale;
            _backgroundImage.color = Color.Lerp(Color.clear, _openColor, scale);
            await Task.Yield();
        }
        _backgroundButton.enabled = true;
        _panelArea.localScale = Vector3.one;
    }

    public async Task ClosePanel()
    {
        float scale = 1;
        while (scale - Time.deltaTime / PanelOpenDuration > 0)
        {
            scale -= Time.deltaTime / PanelOpenDuration;
            _panelArea.localScale = Vector3.one * scale;
            _backgroundImage.color = Color.Lerp(Color.clear, _openColor, scale);
            await Task.Yield();
        }
        _panelArea.localScale = Vector3.zero;

        _backgroundImage.enabled = false;
        _panelArea.gameObject.SetActive(false);
    }
}
