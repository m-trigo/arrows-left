using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFeedback : MonoBehaviour
{
    private bool shouldShow = false;

    private void Update()
    {
        GetComponent<Canvas>().enabled = shouldShow;        
    }

    public void HasInput( bool hasInput )
    {
        shouldShow = hasInput;
    }
}
