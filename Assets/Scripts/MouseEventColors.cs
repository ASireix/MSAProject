using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseEventColors : MonoBehaviour
{
    [SerializeField] private Color32 normalColor;
    [SerializeField] private Color32 hoveredColor;
    [SerializeField] private Color32 pressedColor;
    private Image img;

    private void Start() {
        img = GetComponent<Image>();
        on_leave();
    }

    public void on_enter() {
        img.color = hoveredColor;
    }

    public void on_leave() {
        img.color = normalColor;
    }

    public void on_press() {
        img.color = pressedColor;
    }

    public void on_release() {
        img.color = hoveredColor;
    }

}
