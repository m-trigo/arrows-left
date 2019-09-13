using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCountdownAnimation : MonoBehaviour
{
    public Sprite arrowSprite;
    public Sprite arrowMissingSprite;

    public GameObject[] arrowCounters;

    public float duration;
    public float fadeTime;

    private float elapsed = 0;
    private int reverseIndex = 0;

    private int Length { get => arrowCounters.Length;  }

    void Start()
    {
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        if ( elapsed > duration )
        {
            elapsed %= duration;

            if ( reverseIndex < Length )
            {
                GameObject counter = arrowCounters[ Length - reverseIndex - 1 ];
                counter.GetComponent<SpriteRenderer>().sprite = arrowMissingSprite;
                reverseIndex++;
            }
            else
            {
                reverseIndex = 0;
                foreach( SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>() )
                {
                    spriteRenderer.sprite = arrowSprite;
                }
            }

        }
    }
}
