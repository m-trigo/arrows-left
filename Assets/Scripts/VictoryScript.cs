using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScript : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Start");
        }
    }
}
