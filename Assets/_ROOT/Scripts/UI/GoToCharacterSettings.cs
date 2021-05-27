using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToCharacterSettings : MonoBehaviour
{
    public void GoToCharacterScene()
    {
        SceneManager.LoadScene("CharacterScene");
    }
}
