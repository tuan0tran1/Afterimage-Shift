using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class AudioSetupTool
{
    [MenuItem("SHIFT/Cập nhật Toàn bộ Âm Thanh (Setup Audio)")]
    public static void SetupAudio()
    {
        // Đường dẫn file nhạc 
        string jumpPath = "Assets/Project/Audio/jump/dragon-studio-cartoon-jump-463196.mp3";
        string losePath = "Assets/Project/Audio/lose/tuomas_data-game-over-39-199830.mp3";
        string winPath = "Assets/Project/Audio/win/eaglaxle-gaming-victory-464016.mp3"; // lấy file win đầu tiên

        AudioClip jumpClip = AssetDatabase.LoadAssetAtPath<AudioClip>(jumpPath);
        AudioClip loseClip = AssetDatabase.LoadAssetAtPath<AudioClip>(losePath);
        AudioClip winClip = AssetDatabase.LoadAssetAtPath<AudioClip>(winPath);

        if (jumpClip == null) Debug.LogError("Không tìm thấy file nhạc jump: " + jumpPath);
        if (loseClip == null) Debug.LogError("Không tìm thấy file nhạc lose: " + losePath);
        if (winClip == null) Debug.LogError("Không tìm thấy file nhạc win: " + winPath);

        // --- 1. SETUP PLAYER (JUMP) ---
        GameObject player = GameObject.Find("Player") ?? GameObject.FindGameObjectWithTag("Player");
        if (player != null && jumpClip != null)
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc != null)
            {
                // Tìm kiếm AutoSource đang có (hoặc tạo mới)
                AudioSource[] sources = player.GetComponents<AudioSource>();
                AudioSource jumpSource = null;
                
                // Tránh tạo đúp
                foreach(var s in sources) {
                    if (s.clip == jumpClip) jumpSource = s;
                }
                
                if (jumpSource == null) 
                {
                    jumpSource = player.AddComponent<AudioSource>();
                    jumpSource.playOnAwake = false; // Rất quan trọng
                }
                
                jumpSource.clip = jumpClip;
                pc.jumpSound = jumpSource;
                
                EditorUtility.SetDirty(player);
                Debug.Log(">> Đã gắn xong nhạc Jump cho Player!");
            }
        }

        // --- 2. SETUP GAME MANAGER (WIN & LOSE) ---
        GameManager gm = GameObject.FindFirstObjectByType<GameManager>();
        if (gm != null)
        {
            // Thiết lập Lose
            if (loseClip != null)
            {
                if (gm.loseSound == null) 
                {
                    gm.loseSound = gm.gameObject.AddComponent<AudioSource>();
                    gm.loseSound.playOnAwake = false;
                }
                gm.loseSound.clip = loseClip;
            }

            // Thiết lập Win
            if (winClip != null)
            {
                if (gm.winSound == null) 
                {
                    gm.winSound = gm.gameObject.AddComponent<AudioSource>();
                    gm.winSound.playOnAwake = false; 
                }
                gm.winSound.clip = winClip;
            }

            EditorUtility.SetDirty(gm);
            Debug.Log(">> Đã gắn xong nhạc Win & Lose cho GameManager!");
        }

        // Lưu cảnh hiện hành
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        
        // Popup thông báo hoàn tất
        EditorUtility.DisplayDialog("Thành công!", "Đã cắm jack cắm Âm Thanh vào đúng hệ thống!\nBây giờ bạn có thể trải nghiệm âm thanh rồi.", "OK");
    }
}
