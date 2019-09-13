using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    void Update()
    {
        if ( Input.anyKey )
        {
            SceneManager.LoadScene( "Main" );
        }
    }
}
