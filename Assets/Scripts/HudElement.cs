using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudElement : MonoBehaviour
{
    private const float BLINK_TIME_IN_SECONDS = 0.25f;

    private float elapsed = 0;
    private float timeoutInSeconds = BLINK_TIME_IN_SECONDS;
    private bool visible = true;
    private bool loop = false;

    public void Blink( float timeout = BLINK_TIME_IN_SECONDS )
    {
        SetChildrenVisibility( false );
        timeoutInSeconds = timeout;
        elapsed = 0;
    }

    public void SetLoop( bool shouldLoop )
    {
        loop = shouldLoop;
    }

    private void SetChildrenVisibility( bool isVisible )
    {
        visible = isVisible;
        foreach ( Renderer renderer in GetComponentsInChildren<Renderer>() )
        {
            renderer.enabled = isVisible;
        }

        Renderer rootRenderer = GetComponent<Renderer>();
        if ( rootRenderer != null )
        {
            rootRenderer.enabled = isVisible;
        }
    }

    private void Update()
    {
        elapsed += Time.deltaTime;

        float timeout = timeoutInSeconds;
        if ( loop )
        {
            timeout *= 4;
        }

        if ( elapsed > timeout )
        {
            if ( !visible )
            {
                SetChildrenVisibility( true );
            }
            else if ( loop )
            {
                SetChildrenVisibility( false );
            }

            elapsed = elapsed % timeoutInSeconds;
        }
    }
}
