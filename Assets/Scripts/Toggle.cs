using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle : MonoBehaviour
{
    [SerializeField] private Sprite offSprite;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Image targetButton;

    public void ChangeSprite() {
        targetButton.sprite = targetButton.sprite == offSprite ? onSprite : offSprite;
    }
}
