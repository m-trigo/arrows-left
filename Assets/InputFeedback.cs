using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFeedback : MonoBehaviour
{
    public GameObject top;
    public GameObject bottom;
    public GameObject left;
    public GameObject right;

    private bool shouldShow = false;
    private const float SPEED_FACTOR = 4;

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        GetComponent<Canvas>().enabled = shouldShow;
        if ( shouldShow )
        {
            float speed = SPEED_FACTOR * Time.deltaTime;

            bottom.GetComponent<RectTransform>().anchorMax = Vector2.MoveTowards( bottom.GetComponent<RectTransform>().anchorMax,
                    new Vector2( 1, 0 ), speed );

            if ( Vector2.Distance( bottom.GetComponent<RectTransform>().anchorMax, new Vector2( 1, 0 ) ) < 0.1f )
            {
               right.GetComponent<RectTransform>().anchorMax = Vector2.MoveTowards( right.GetComponent<RectTransform>().anchorMax,
                    new Vector2( 1, 1 ), speed );
            }

            if ( Vector2.Distance( right.GetComponent<RectTransform>().anchorMax, new Vector2( 1, 1 ) ) < 0.1f )
            {
                top.GetComponent<RectTransform>().anchorMin = Vector2.MoveTowards( top.GetComponent<RectTransform>().anchorMin,
                    new Vector2( 0, 1 ), speed );
            }

            if ( Vector2.Distance( top.GetComponent<RectTransform>().anchorMin, new Vector2( 0, 1 ) ) < 0.1f )
            {
                left.GetComponent<RectTransform>().anchorMin = Vector2.MoveTowards( left.GetComponent<RectTransform>().anchorMin,
                    new Vector2( 0, 0 ), speed );
            }
        }
        else
        {
            Reset();
        }
    }

    private void Reset()
    {
        top.GetComponent<RectTransform>().anchorMin = new Vector2( 1, 1 );
        top.GetComponent<RectTransform>().anchorMax = new Vector2( 1, 1 );
            
        bottom.GetComponent<RectTransform>().anchorMin = Vector2.zero;
        bottom.GetComponent<RectTransform>().anchorMax = Vector2.zero;

        left.GetComponent<RectTransform>().anchorMin = new Vector2( 0, 1 );
        left.GetComponent<RectTransform>().anchorMax = new Vector2( 0, 1 );

        right.GetComponent<RectTransform>().anchorMin = new Vector2( 1, 0 );
        right.GetComponent<RectTransform>().anchorMax = new Vector2( 1, 0 );
    }

    public void HasInput( bool hasInput )
    {
        shouldShow = hasInput;
    }
}
