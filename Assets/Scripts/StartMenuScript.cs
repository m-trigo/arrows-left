using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    private float elapsed = 0;

    void Update()
    {
        elapsed += Time.smoothDeltaTime;

        if ( elapsed > 0.4f && Input.anyKey )
        {
            SceneManager.LoadScene( "Main" );
        }
    }
}
