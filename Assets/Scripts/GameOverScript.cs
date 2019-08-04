using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Start");
        }
    }
}
