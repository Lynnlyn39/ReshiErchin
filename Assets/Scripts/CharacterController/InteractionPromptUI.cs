using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera _mainCam;
    [SerializeField]
    private GameObject _uiPanel;
    [SerializeField]
    private TextMeshProUGUI _prompText;

    private void Start()
    {
        _mainCam = Camera.main;
        _uiPanel.SetActive(false);
    }

    private void LateUpdate()
    {
        var rotation = _mainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up );
    }

    public bool isDisplayed = false;

    public void SetUp(string promtText)
    {
        _prompText.text = promtText;
        _uiPanel.SetActive(true);
        isDisplayed = true;
    }
    public void Close()
    {
        isDisplayed = false;
        _uiPanel.SetActive(false);
    }
}
