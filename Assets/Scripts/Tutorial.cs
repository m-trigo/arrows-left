using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public string[] sections;
    public float fadeTime;
    public float timeout;

    [HideInInspector]
    public bool started = false;

    private int index = 0;
    private int nextIndex = 0;
    private float elapsed = 0;
    private TextMeshProUGUI textBox = null;

    private void Start()
    {
        textBox = GetComponentInChildren<TextMeshProUGUI>();
        textBox.SetText( sections[ index ] );
    }

    private void Update()
    {
        if ( started )
        {
            elapsed += Time.deltaTime;
        }

        if ( nextIndex < sections.Length && index != nextIndex && elapsed > fadeTime )
        {
            index = nextIndex;
            textBox.SetText( sections[ index ] );
            textBox.CrossFadeAlpha( 1, fadeTime, false );
            elapsed = 0;
        }

        if ( elapsed > timeout )
        {
            Next();
        }
    }

    public void Next()
    {
        if ( nextIndex == index )
        {
            nextIndex++;
            elapsed = 0;
            textBox.CrossFadeAlpha( 0, fadeTime, false );
        }
    }
}
