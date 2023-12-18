using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolWorldUI : MonoBehaviour
{
    private Image _progressImage;
    private TextMeshProUGUI _toolLabel;

    private void Start()
    {
        _progressImage = transform.Find("img_Progress").GetComponent<Image>();
        _toolLabel = transform.Find("label_Tool").GetComponent<TextMeshProUGUI>();
    }

    public void Show(bool show)
    {
        _progressImage.gameObject.SetActive(show);
        _toolLabel.gameObject.SetActive(show);
    }
    
    public void SetProgress(float progress)
    {
        _progressImage.fillAmount = progress;
    }
}
