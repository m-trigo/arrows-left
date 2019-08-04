﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    private float elapsed = 0;

    void Update()
    {
        elapsed += Time.smoothDeltaTime;

        if (elapsed > 1 && Input.anyKey)
        {
            SceneManager.LoadScene("Start");
        }
    }
}
