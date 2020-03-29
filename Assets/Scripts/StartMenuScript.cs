using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    public ScreenTransitionAnimation screenTransition;

    private float elapsed = 0;

    public void OpenUrl()
    {
        Application.OpenURL("https://moving-pixels.itch.io");
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        if ( !Input.GetKey( KeyCode.Space ) )
        {
            elapsed = 0;
        }

        if ( elapsed > 0.5f )
        {
            screenTransition.AnimateSceneEnd( () => SceneManager.LoadScene( "Tutorial" ) );
        }
    }
}
