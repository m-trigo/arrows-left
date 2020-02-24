using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public ScreenTransitionAnimation screenTransition;

    private float elapsed = 0;

    void Update()
    {
        elapsed += Time.smoothDeltaTime;

        if ( elapsed > 10f || ( Input.anyKeyDown && elapsed > 1 ) )
        {
            screenTransition.AnimateSceneEnd( () => SceneManager.LoadScene( "Title" ) );
        }
    }
}
