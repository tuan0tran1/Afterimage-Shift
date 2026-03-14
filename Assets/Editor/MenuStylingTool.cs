using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.IO;

public class MenuStylingTool : EditorWindow
{
    [MenuItem("SHIFT/Làm Đẹp Giao Diện (MainMenu & LevelSelect)")]
    public static void BeautifyMenus()
    {
        // 1. Lưu Cảnh Hiện Tại
        string currentScene = EditorSceneManager.GetActiveScene().path;
        if (!string.IsNullOrEmpty(currentScene))
        {
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        }

        string bgPath = "Assets/Simple 2D Platformer BE2/Sprites/ChatGPT Image 22_32_40 13 thg 3, 2026.png";
        
        // 2. Chỉnh sửa MainMenu
        string mainMenuPath = "Assets/Scenes/MainMenu.unity";
        if (File.Exists(mainMenuPath))
        {
            EditorSceneManager.OpenScene(mainMenuPath);
            ApplyStyleToCurrentScene(bgPath);
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        }
        else
        {
            Debug.LogWarning("Không tìm thấy tệp MainMenu.unity");
        }

        // 3. Chỉnh sửa LevelSelect
        string levelSelectPath = "Assets/Scenes/LevelSelect.unity";
        if (File.Exists(levelSelectPath))
        {
            EditorSceneManager.OpenScene(levelSelectPath);
            ApplyStyleToCurrentScene(bgPath);
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        }
        else
        {
            Debug.LogWarning("Không tìm thấy tệp LevelSelect.unity");
        }

        // 4. Mở lại cảnh ban đầu
        if (!string.IsNullOrEmpty(currentScene))
        {
            EditorSceneManager.OpenScene(currentScene);
        }

        EditorUtility.DisplayDialog("Lột Xác Thành Công!", "Đã áp dụng hình nền tuyệt đẹp và thiết kế lại toàn bộ Nút Bấm trong Main Menu và Level Select trở nên góc cạnh, nổi bật hơn!", "Ghi nhận");
    }

    private static void ApplyStyleToCurrentScene(string bgPath)
    {
        // Xử lý Hình Nền (Background)
        Sprite bgSprite = AssetDatabase.LoadAssetAtPath<Sprite>(bgPath);
        if (bgSprite != null)
        {
            // Tắt chế độ nén hoặc blur nếu cần
            TextureImporter importer = AssetImporter.GetAtPath(bgPath) as TextureImporter;
            if (importer != null)
            {
                importer.textureType = TextureImporterType.Sprite;
                importer.filterMode = FilterMode.Bilinear; // Ảnh to dùng Bilinear sẽ mượt hơn
                importer.SaveAndReimport();
            }

            Canvas mainCanvas = GameObject.FindFirstObjectByType<Canvas>();
            if (mainCanvas != null)
            {
                Transform bgTransform = mainCanvas.transform.Find("MainMenu_Background_AI");
                GameObject bgObj;
                if (bgTransform == null)
                {
                    bgObj = new GameObject("MainMenu_Background_AI");
                    bgObj.transform.SetParent(mainCanvas.transform, false);
                    bgObj.transform.SetAsFirstSibling(); // Đẩy xuống dưới cùng
                }
                else
                {
                    bgObj = bgTransform.gameObject;
                }

                Image bgImg = bgObj.GetComponent<Image>();
                if (bgImg == null) bgImg = bgObj.AddComponent<Image>();

                bgImg.sprite = bgSprite;
                bgImg.color = Color.white;
                bgImg.type = Image.Type.Simple;
                
                // Trải rộng khắp màn hình
                RectTransform rt = bgObj.GetComponent<RectTransform>();
                rt.anchorMin = Vector2.zero;
                rt.anchorMax = Vector2.one;
                rt.offsetMin = Vector2.zero;
                rt.offsetMax = Vector2.zero;
            }
        }
        else
        {
            Debug.LogError("Cảnh báo: Không thể nạp được hình ảnh nền từ đường dẫn: " + bgPath);
        }

        // Thiết kế lại Nút bấm (Buttons)
        Button[] buttons = GameObject.FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Button btn in buttons)
        {
            // Tạo phong cách Glassmorphism / Phẳng hiện đại hiện đại cho nút
            Image btnImg = btn.GetComponent<Image>();
            if (btnImg != null)
            {
                btnImg.color = new Color(0.12f, 0.14f, 0.2f, 0.85f); // Màu xanh đen trong suốt
                
                // Gắn Outline phát sáng cạnh nút
                Outline outline = btn.gameObject.GetComponent<Outline>();
                if (outline == null) outline = btn.gameObject.AddComponent<Outline>();
                outline.effectColor = new Color(0.2f, 0.8f, 1f, 0.5f); // Viền lấp lánh màu lam
                outline.effectDistance = new Vector2(3, -3);

                ColorBlock cb = btn.colors;
                cb.normalColor = Color.white;
                cb.highlightedColor = new Color(0.3f, 0.9f, 1f, 1f); // Xanh dương sáng khi di chuột
                cb.pressedColor = new Color(0.1f, 0.5f, 0.8f, 1f); // Tối màu lại khi bấm
                cb.selectedColor = Color.white;
                btn.colors = cb;
            }

            // Xử lý Text thường
            UnityEngine.UI.Text txt = btn.GetComponentInChildren<UnityEngine.UI.Text>(true);
            if (txt != null)
            {
                txt.color = new Color(0.9f, 0.95f, 1f, 1f); // Trắng hơi xanh
                txt.fontStyle = FontStyle.Bold;
                txt.fontSize = (int)(txt.fontSize * 1.1f); // To hơn tí
                
                Shadow shadow = txt.GetComponent<Shadow>();
                if (shadow == null) shadow = txt.gameObject.AddComponent<Shadow>();
                shadow.effectColor = Color.black;
                shadow.effectDistance = new Vector2(2, -2);
            }

            // Xử lý TextMeshPro
            var tmpTxt = btn.GetComponentInChildren<TMPro.TextMeshProUGUI>(true);
            if (tmpTxt != null)
            {
                tmpTxt.color = new Color(0.9f, 0.95f, 1f, 1f);
                tmpTxt.fontStyle = TMPro.FontStyles.Bold;
                
                // Mặc định TextMeshPro tự xịn, ta chỉ cần kích thước nổi bật
                btn.transform.localScale = Vector3.one; 
            }
            
            EditorUtility.SetDirty(btn.gameObject);
        }
        
        // Thêm một lớp phủ tối (Dark Overlay) sau lưng các chữ Text thường nằm ngoài UI để dễ đọc
        UnityEngine.UI.Text[] texts = GameObject.FindObjectsByType<UnityEngine.UI.Text>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var t in texts)
        {
            // Bỏ qua Text của Button
            if (t.GetComponentInParent<Button>() == null)
            {
                t.color = Color.white;
                t.fontStyle = FontStyle.Bold;
                Shadow shadow = t.GetComponent<Shadow>();
                if (shadow == null) shadow = t.gameObject.AddComponent<Shadow>();
                shadow.effectColor = Color.black;
                shadow.effectDistance = new Vector2(3, -3);
                EditorUtility.SetDirty(t.gameObject);
            }
        }
        
        var tmps = GameObject.FindObjectsByType<TMPro.TextMeshProUGUI>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var t in tmps)
        {
            if (t.GetComponentInParent<Button>() == null)
            {
                t.color = Color.white;
                t.fontStyle = TMPro.FontStyles.Bold;
                EditorUtility.SetDirty(t.gameObject);
            }
        }
    }
}
