using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    public ScreenTransitionAnimation screenTransition;

    private float idleElapsed = 0;
    private float inputElapsed = 0;

    void Update()
    {
        idleElapsed += Time.smoothDeltaTime;
        inputElapsed += Time.smoothDeltaTime;

        if ( Input.GetKey( KeyCode.Space ) )
        {
            idleElapsed = 0;
        }
        else
        {
            inputElapsed = 0;
        }

        if ( idleElapsed > 10 )
        {
            screenTransition.AnimateSceneEnd( () => SceneManager.LoadScene( "Title" ) );
        }
        else if ( inputElapsed > 0.5f )
        {
            screenTransition.AnimateSceneEnd( () => SceneManager.LoadScene( "Main" ) );
        }
    }
}
