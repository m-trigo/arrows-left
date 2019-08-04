using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    private float elapsed = 0;

    void Update()
    {
        elapsed += Time.smoothDeltaTime;

        if (elapsed > 1 && Input.anyKey)
        {
            SceneManager.LoadScene("Main");
        }
    }
}
