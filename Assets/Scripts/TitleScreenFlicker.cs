using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleScreenFlicker : MonoBehaviour
{
    public float duration;
    public float fadeTime;

    private float elapsed = 0;
    private int alpha = 1;
    private TextMeshProUGUI text = null;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        if ( elapsed > duration )
        {
            elapsed %= duration;

            if ( alpha == 1 )
            {
                alpha = 0;
            }
            else
            {
                alpha = 1;
            }

            text.CrossFadeAlpha( alpha, fadeTime, false );
        }
    }
}
