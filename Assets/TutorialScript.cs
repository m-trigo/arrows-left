using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    public ScreenTransitionAnimation screenTransition;

    private float elapsed = 0;

    void Update()
    {
        elapsed += Time.smoothDeltaTime;

        if ( elapsed > 60 )
        {
            screenTransition.AnimateSceneEnd( () => SceneManager.LoadScene( "Title" ) );
        }
        else if ( Input.anyKeyDown && elapsed > 0.4f )
        {
            screenTransition.AnimateSceneEnd( () => SceneManager.LoadScene( "Main" ) );
        }
    }
}
