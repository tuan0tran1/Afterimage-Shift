using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class FinalStoryController : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;
    public GameObject continueButton;
    public GameObject startButton;

    [Header("Story Data")]
    public string[] titles;
    [TextArea(5, 10)]
    public string[] segments;

    [Header("Settings")]
    public float typeSpeed = 0.04f;

    private int currentIndex = 0;
    private bool isTyping = false;

    void Start()
    {
        continueButton.SetActive(false);
        startButton.SetActive(false);
        ShowSegment();
    }

    public void ShowSegment()
    {
        if (currentIndex < segments.Length)
        {
            StartCoroutine(TypeText(segments[currentIndex]));
            titleText.text = titles[currentIndex].ToUpper();
        }
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        contentText.text = "";
        continueButton.SetActive(false);

        foreach (char c in text.ToCharArray())
        {
            contentText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;

        if (currentIndex < segments.Length - 1)
            continueButton.SetActive(true);
        else
            startButton.SetActive(true);
    }

    public void OnContinuePressed()
    {
        if (isTyping) return;

        currentIndex++;
        ShowSegment();
    }

    public void OnStartGame()
    {
        // Chuyển đến màn chơi đầu tiên (Level_01)
        SceneManager.LoadScene("Level_01");
    }
}