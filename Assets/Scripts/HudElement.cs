using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudElement : MonoBehaviour
{
    private const float BLINK_TIME_IN_SECONDS = 0.1f;

    private float elapsed = 0;
    private float timeoutInSeconds = BLINK_TIME_IN_SECONDS;
    private bool visible = true;

    public void Blink()
    {
        SetChildrenVisibility( false );
        elapsed = 0;
    }

    private void SetChildrenVisibility( bool isVisible, float timeout = BLINK_TIME_IN_SECONDS )
    {
        visible = isVisible;
        foreach ( Renderer renderer in GetComponentsInChildren<Renderer>() )
        {
            renderer.enabled = isVisible;
        }
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        if ( !visible && elapsed > timeoutInSeconds )
        {
            SetChildrenVisibility( true );
        }

    }
}
