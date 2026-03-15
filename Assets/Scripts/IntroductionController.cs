using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroductionController : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}