using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    public ScreenTransitionAnimation screenTransition;

    private float elapsed = 0;

    void Update()
    {
        elapsed += Time.deltaTime;

        if ( !Input.anyKey )
        {
            elapsed = 0;
        }

        if ( elapsed > 0.5f )
        {
            screenTransition.AnimateSceneEnd( () => SceneManager.LoadScene( "Tutorial" ) );
        }
    }
}
