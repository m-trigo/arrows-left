using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    private float elapsed = 0;

    void Update()
    {
        elapsed += Time.deltaTime;

        if ( Input.anyKeyDown && elapsed > 0.2f )
        {
            SceneManager.LoadScene( "Tutorial" );
        }
    }
}
