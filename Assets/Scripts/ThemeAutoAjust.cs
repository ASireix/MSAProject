using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeAutoAjust : MonoBehaviour
{
    [SerializeField] List<RectTransform> themes;
    [SerializeField] RectTransform viewportContent;
    private List<bool> states = new List<bool>();

    private void Start() {
        foreach (var _ in themes) states.Add(false);
    }

    public void on_toggle(RectTransform theme) {
        int j = -1;
        Vector2 size = new Vector2();
        for (int i = 0; i < themes.Count; i++) {
            if (j != -1) {
                int sign = states[j] ? -1 : 1;
                themes[i].position += Vector3.up * size[1] * sign;
                viewportContent.sizeDelta -= Vector2.up * size[1] * sign;
            }
            if (theme == themes[i]) {
                states[i] ^= true;  // toggles the state
                RectTransform questions = (RectTransform) themes[i].Find("Questions");
                size = questions.sizeDelta;
                viewportContent.sizeDelta += Vector2.up * size[1] * (states[i] ? 1 : -1);
                j = i;
            }
        }
    }
}
