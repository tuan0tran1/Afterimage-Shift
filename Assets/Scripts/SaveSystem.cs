using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static void SaveLevel(int level)
    {
        PlayerPrefs.SetInt("UnlockedLevel", level);
        PlayerPrefs.Save();
    }

    public static int LoadLevel()
    {
        return PlayerPrefs.GetInt("UnlockedLevel", 1);
    }
}