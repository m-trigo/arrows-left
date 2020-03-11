using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenScript : MonoBehaviour
{
    public ScreenTransitionAnimation screenTransition;

    private float elapsed = 0;

    void Update()
    {
        elapsed += Time.smoothDeltaTime;

        if ( !screenTransition.IsTransitionOver() )
        {
            return;
        }

        if ( Input.GetKey( KeyCode.Space ) || elapsed > 10 )
        {
            screenTransition.AnimateSceneEnd( () => SceneManager.LoadScene( "Title" ) );
        }
    }
}
