using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenScript : MonoBehaviour
{
    public ScreenTransitionAnimation screenTransition;

    private float idleElapsed = 0;
    private float inputElapsed = 0;

    void Update()
    {
        idleElapsed += Time.smoothDeltaTime;
        inputElapsed += Time.smoothDeltaTime;

        if ( Input.anyKey )
        {
            idleElapsed = 0;
        }
        else
        {
            inputElapsed = 0;
        }

        if ( idleElapsed > 10 || inputElapsed > 0.5f )
        {
            screenTransition.AnimateSceneEnd( () => SceneManager.LoadScene( "Title" ) );
        }
    }
}
