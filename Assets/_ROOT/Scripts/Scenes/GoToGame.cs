using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToGame : MonoBehaviour
{
    public void GoToGameScene()
    {
        FindObjectOfType<Fader>().FadeIn();
        SceneManager.LoadScene("1");
    }
}
