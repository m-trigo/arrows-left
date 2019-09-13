using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    private float elapsed = 0;

    void Update()
    {
        elapsed += Time.smoothDeltaTime;

        if ( elapsed > 60 )
        {
            SceneManager.LoadScene( "Title" );
        }
        else if ( Input.anyKeyDown && elapsed > 0.4f )
        {
            SceneManager.LoadScene( "Main" );
        }
    }
}
