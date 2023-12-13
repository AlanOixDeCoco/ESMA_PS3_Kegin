using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PanelComponent : MonoBehaviour
{
    public const float _panelOpenDuration = .2f;

    [SerializeField] private Transform _panelArea;

    private void Awake()
    {
        if (_panelArea == null) _panelArea = transform.GetChild(0);
    }

    public async void OpenPanel()
    {
        GetComponent<Image>().enabled = true;
        _panelArea.gameObject.SetActive(true);

        float scale = 0;
        while (scale + Time.deltaTime / _panelOpenDuration < 1)
        {
            scale += Time.deltaTime / _panelOpenDuration;
            _panelArea.localScale = Vector3.one * scale;
            await Task.Yield();
        }
        _panelArea.localScale = Vector3.one;
    }

    public async Task ClosePanel()
    {
        float scale = 1;
        while (scale - Time.deltaTime / _panelOpenDuration > 0)
        {
            scale -= Time.deltaTime / _panelOpenDuration;
            _panelArea.localScale = Vector3.one * scale;
            await Task.Yield();
        }
        _panelArea.localScale = Vector3.zero;

        GetComponent<Image>().enabled = false;
        _panelArea.gameObject.SetActive(false);
    }
}
