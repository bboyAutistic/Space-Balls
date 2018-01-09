using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUI : MonoBehaviour {
    [SerializeField]
    RectTransform widthOfHelathBar;
    float maxWidth;

    void Awake()
    {
        maxWidth = widthOfHelathBar.rect.width;
    }
	
    public void UpdateShieldDisplay(float percentage)
    {
        widthOfHelathBar.sizeDelta = new Vector2(maxWidth * percentage, 10f);

    }
}
