using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UnderlineTextOnHover : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetUnderline(bool active)
    {
        text.fontStyle = active ? FontStyles.Underline : FontStyles.Normal;
    }
}
