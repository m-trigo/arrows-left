using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    public ScreenTransitionAnimation screenTransition;

    private float elapsed = 0;

    void Update()
    {
        elapsed += Time.deltaTime;

        if ( Input.anyKeyDown && elapsed > 0.2f )
        {
            screenTransition.AnimateSceneEnd( () => SceneManager.LoadScene( "Tutorial" ) );
        }
    }
}
