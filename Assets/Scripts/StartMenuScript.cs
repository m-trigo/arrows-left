using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    void Update()
    {
        if ( Input.anyKeyDown )
        {
            SceneManager.LoadScene( "Main" );
        }
    }
}
