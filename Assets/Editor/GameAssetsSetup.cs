using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.SceneManagement;

public class GameAssetsSetup : EditorWindow
{
    [MenuItem("SHIFT/Cập nhật Hình ảnh Khớp Thiết Kế (Reference Art)")]
    public static void SetupGraphics()
    {
        string dir = "Assets/Project/Sprites";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        // 1. TẠO HÌNH ẢNH TỪ MÃ ASCII (PIXEL ART MATCHING THE REFERENCE IMAGE)
        
        // --- Player - Boy with Red Cap ---
        string[] playerArt = {
            "......RRRR......",
            ".....RRRRRR.....",
            "....RRffffRR....",
            "...RffXffXffR...",
            "...ffffffffff...",
            "....ffffffff....",
            "....RRRRRRRR....",
            "...RRbRRRRbRR...",
            "...bbRbbbbRbb...",
            "....bbbbbbbb....",
            "....BB....BB....",
            "...BBB....BBB...",
            "....ll....ll....",
            "...lll....lll...",
            "................",
            "................"
        };
        CreateSprite(dir + "/spr_player.png", playerArt);

        // --- Ghost - White Bedsheet ---
        string[] ghostArt = {
            "................",
            "......WWWW......",
            ".....WWWWWW.....",
            "....WWWWWWWW....",
            "...WWwXWWwXWW...",
            "...WWWWWWWWWW...",
            "...WWWWWWWWWW...",
            "...WWWWWWWWWW...",
            "...WWWWWWWWWW...",
            "...W.WW..WW.W...",
            "..WW.WW..WW.WW..",
            "................",
            "................",
            "................",
            "................",
            "................"
        };
        CreateSprite(dir + "/spr_ghost.png", ghostArt);

        // --- Ground - Grass top with stone bricks ---
        string[] groundArt = {
            "vvvvvvvvvvvvvvvv",
            "vmmmmmmmmmmmmmmv",
            "mddddddddddddddm",
            "ddGGddGGddGGddGG",
            "ddGGddGGddGGddGG",
            "dddddddddddddddd",
            "dGGddGGddGGddGGd",
            "dGGddGGddGGddGGd",
            "dddddddddddddddd",
            "ddGGddGGddGGddGG",
            "ddGGddGGddGGddGG",
            "dddddddddddddddd",
            "dGGddGGddGGddGGd",
            "dGGddGGddGGddGGd",
            "dddddddddddddddd",
            "ddGGddGGddGGddGG"
        };
        CreateSprite(dir + "/spr_ground.png", groundArt);

        // --- Moving Platform - Grass top block ---
        string[] platformArt = {
            "................",
            "................",
            "................",
            "................",
            "................",
            "................",
            "................",
            "................",
            "vvvvvvvvvvvvvvvv",
            "vmmmmmmmmmmmmmmv",
            "mddddddddddddddm",
            "ddGGddGGddGGddGG",
            "ddGGddGGddGGddGG",
            "dGddGGddGGddGGdG",
            "dGddGGddGGddGGdG",
            "dddddddddddddddd"
        };
        CreateSprite(dir + "/spr_platform.png", platformArt);

        // --- Door - Yellow Wood with Arched Top (16x32) ---
        string[] doorArt = {
            "......YYYY......",
            "....YYyyyyYY....",
            "...YyyyyyyyyY...",
            "..YyyyYYYYyyyY..",
            "..YyyYYyyYYyyY..",
            ".YyyYYyyyyYYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyKYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            ".YyyYyyyyyyYyyY.",
            "..YyyYYyyYYyyY..",
            "..YyyyYYYYyyyY..",
            "...YYYYYYYYYY...",
            "................"
        };
        CreateSprite(dir + "/spr_door.png", doorArt);

        // --- Spike Trap - Metal Spikes on Grass/Stone Base ---
        string[] spikeArt = {
            "................",
            "................",
            "................",
            "................",
            "................",
            "................",
            "................",
            ".......WW.......",
            "......WddW......",
            ".....WddddW.....",
            ".....WWWWWW.....",
            "...WWWWWWWWWW...",
            "..WddddddddddW..",
            "..WWWWWWWWWWWW..",
            "vvvvvvvvvvvvvvvv",
            "dGddGGddGGddGGdG"
        };
        CreateSprite(dir + "/spr_spike.png", spikeArt);

        // --- Switch - Green Base, Red Pad ---
        string[] switchArt = {
            "................",
            "................",
            "................",
            "................",
            "................",
            "................",
            "................",
            "................",
            "................",
            "......RRRR......",
            ".....RRRRRR.....",
            "....RRRRRRRR....",
            "...vvvvvvvvvv...",
            "..vvvvvvvvvvvv..",
            "..vmmmmmmmmmmv..",
            "..dddddddddddd.."
        };
        CreateSprite(dir + "/spr_switch.png", switchArt);

        // --- Lever - Grey base, Red Stick ---
        string[] leverArt = {
            "................",
            "................",
            ".....RR.........",
            "....RRRR........",
            "....RRRR........",
            ".....RR.........",
            "......rr........",
            "......rr........",
            ".......rr.......",
            ".......rr.......",
            "......GGGG......",
            ".....GddddG.....",
            ".....GddddG.....",
            ".....GddddG.....",
            ".....GGGGGG.....",
            "................"
        };
        CreateSprite(dir + "/spr_lever.png", leverArt);

        // --- Goal - Glowing Blue Portal (16x32) ---
        string[] goalArt = {
            "......MMMM......",
            "....MMCCCCMM....",
            "...MCCCCCCCCM...",
            "..MCCDDDDDDCCM..",
            "..MCDD####DDCM..",
            ".MCDD######DDCM.",
            ".MCDD######DDCM.",
            ".MCDD##**##DDCM.",
            ".MCDD######DDCM.",
            ".MCDD######DDCM.",
            ".MCDD######DDCM.",
            ".MCDD##*###DDCM.",
            ".MCDD######DDCM.",
            ".MCDD######DDCM.",
            ".MCDD######DDCM.",
            ".MCDD###*##DDCM.",
            ".MCDD######DDCM.",
            ".MCDD######DDCM.",
            ".MCDD######DDCM.",
            ".MCDD#*####DDCM.",
            ".MCDD######DDCM.",
            ".MCDD######DDCM.",
            ".MCDD######DDCM.",
            ".MCDD######DDCM.",
            ".MCDD######DDCM.",
            ".MCDD##**##DDCM.",
            ".MCDD######DDCM.",
            "..MCDD####DDCM..",
            "..MCCDDDDDDCCM..",
            "...MCCCCCCCCM...",
            "....MMMMMMMM....",
            "................"
        };
        CreateSprite(dir + "/spr_goal.png", goalArt);

        AssetDatabase.Refresh();

        // 2. TỰ ĐỘNG CÀI ĐẶT THÔNG SỐ SPRITE (Tắt Blur)
        ConfigureSpriteSettings(dir + "/spr_player.png");
        ConfigureSpriteSettings(dir + "/spr_ghost.png");
        ConfigureSpriteSettings(dir + "/spr_ground.png");
        ConfigureSpriteSettings(dir + "/spr_platform.png");
        ConfigureSpriteSettings(dir + "/spr_door.png");
        ConfigureSpriteSettings(dir + "/spr_spike.png");
        ConfigureSpriteSettings(dir + "/spr_switch.png");
        ConfigureSpriteSettings(dir + "/spr_lever.png");
        ConfigureSpriteSettings(dir + "/spr_goal.png");

        // 3. GÁN VÀO CÁC OBJECT TRONG SCENE HIỆN TẠI VÀ CHỈNH SỬA TỶ LỆ
        ApplySpriteToTagOrName("Player", dir + "/spr_player.png", false);
        ApplySpriteToTagOrName("Ground_Left", dir + "/spr_ground.png", true);
        ApplySpriteToTagOrName("Ground_Right", dir + "/spr_ground.png", true);
        ApplySpriteToTagOrName("Ground", dir + "/spr_ground.png", true);
        ApplySpriteToTagOrName("MovingPlatform", dir + "/spr_platform.png", true);
        ApplySpriteToTagOrName("Door", dir + "/spr_door.png", false);
        ApplySpriteToTagOrName("SpikeTrap", dir + "/spr_spike.png", true);
        ApplySpriteToTagOrName("Switch", dir + "/spr_switch.png", false);
        ApplySpriteToTagOrName("Lever", dir + "/spr_lever.png", false);
        ApplySpriteToTagOrName("Goal", dir + "/spr_goal.png", false);

        // Ghost Prefab update via GameManager
        GameManager gm = GameObject.FindFirstObjectByType<GameManager>();
        if (gm != null && gm.ghostPrefab != null)
        {
            SpriteRenderer ghostSr = gm.ghostPrefab.GetComponent<SpriteRenderer>();
            if (ghostSr != null)
            {
                ghostSr.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(dir + "/spr_ghost.png");
                ghostSr.color = new Color(1f, 1f, 1f, 0.8f); // Trắng trong suốt
                EditorUtility.SetDirty(gm.ghostPrefab);
            }
        }

        // 4. SỬ DỤNG HÌNH NỀN MỚI TỪ THƯ MỤC CỦA BẠN (ChatGPT Image 22_33_46 13 thg 3, 2026.png)
        string bgPath = "Assets/Simple 2D Platformer BE2/Sprites/ChatGPT Image 22_33_46 13 thg 3, 2026.png";
        ConfigureSpriteSettings(bgPath, false); // Không ép kích Pixel gắt gao quá đối với Background mượt
        SetupNewBackground(bgPath);

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        
        EditorUtility.DisplayDialog("Vẽ Xong!", "Đã cập nhật toàn bộ đồ họa Game về phong cách Nhóc Cậu Bé (Mario Style), có Cổng Xanh, Cửa Vàng, Nền Cỏ Đá và Hầm Động xịn xò như Hình Thiết Kế!", "Quá đẹp");
    }

    private static void SetupNewBackground(string bgPath)
    {
        Sprite bgSprite = AssetDatabase.LoadAssetAtPath<Sprite>(bgPath);
        if (bgSprite == null) 
        {
            Debug.LogError("Không tìm thấy tệp ảnh làm nền: " + bgPath);
            return;
        }

        GameObject bgObj = GameObject.Find("DungeonBackground");
        if (bgObj == null)
        {
            bgObj = new GameObject("DungeonBackground");
            SpriteRenderer sr = bgObj.AddComponent<SpriteRenderer>();
            sr.drawMode = SpriteDrawMode.Simple; // CHỈ HIỂN THỊ 1 HÌNH KHỔNG LỒ, KHÔNG LẶP LẠI (Tiled)
            sr.sortingOrder = -100;
            bgObj.transform.position = new Vector3(0, 0, 50f);
        }

        SpriteRenderer curSr = bgObj.GetComponent<SpriteRenderer>();
        if (curSr != null)
        {
            curSr.sprite = bgSprite;
            curSr.drawMode = SpriteDrawMode.Simple; // Không lặp lại
            curSr.color = Color.white; // Màu vẽ gốc chân thực nhất
            
            // Tăng kích thước Transform thật to để bao quát toàn bản đồ (Scale x40)
            bgObj.transform.localScale = new Vector3(80f, 60f, 1f); 
        }
        
        Camera cam = Camera.main;
        if (cam != null)
        {
            cam.backgroundColor = new Color(0.05f, 0.08f, 0.15f); // Xanh đen sâu thẳm
            cam.clearFlags = CameraClearFlags.SolidColor;
            EditorUtility.SetDirty(cam);
        }
    }

    private static void CreateSprite(string path, string[] art)
    {
        int width = art[0].Length;
        int height = art.Length;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        tex.filterMode = FilterMode.Point;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                char c = art[height - 1 - y][x];
                Color color = Color.clear;
                
                switch (c)
                {
                    case '.': color = Color.clear; break;
                    case 'R': color = Color.red; break; // Red Cap/Shirt
                    case 'r': color = new Color(0.6f, 0.2f, 0.2f); break; // Dark Red stick
                    case 'f': color = new Color(1f, 0.8f, 0.6f); break; // Skin Face
                    case 'X': color = Color.black; break; // Black Eye
                    case 'b': color = new Color(0.3f, 0.15f, 0.05f); break; // Brown Hair / Backpack
                    case 's': color = new Color(0.9f, 0.7f, 0.5f); break; // Skin hand
                    case 'B': color = new Color(0.1f, 0.3f, 0.8f); break; // Blue Pants
                    case 'l': color = new Color(0.2f, 0.1f, 0.05f); break; // Shoes
                    
                    case 'W': color = Color.white; break; // Ghost body
                    case 'w': color = new Color(0.9f, 0.9f, 0.9f); break; // Ghost shading
                    
                    case 'v': color = new Color(0.4f, 0.9f, 0.3f); break; // Light Green Grass
                    case 'm': color = new Color(0.2f, 0.6f, 0.1f); break; // Dark Green Grass
                    case 'd': color = new Color(0.3f, 0.3f, 0.35f); break; // Dark Stone
                    case 'G': color = new Color(0.5f, 0.5f, 0.55f); break; // Light Stone / Steel
                    case '1': color = new Color(0.12f, 0.12f, 0.15f); break; // Đá Hang Động 1
                    case '2': color = new Color(0.16f, 0.16f, 0.2f); break; // Đá Hang Động 2
                    case '0': color = new Color(0.08f, 0.08f, 0.1f); break; // Rãnh Đá Tối
                    
                    case 'Y': color = new Color(0.9f, 0.7f, 0.1f); break; // Orange Yellow Door
                    case 'y': color = new Color(1f, 0.8f, 0.3f); break; // Light Yellow Wood
                    case 'K': color = new Color(0.2f, 0.1f, 0f); break; // Knob
                    
                    case 'M': color = new Color(0.2f, 0.6f, 1f); break; // Portal Cyan edge
                    case 'C': color = Color.cyan; break; // Cyan ring
                    case 'D': color = new Color(0f, 0.3f, 0.7f); break; // Dark Blue Portal Core
                    case '#': color = new Color(0.05f, 0.1f, 0.3f); break; // Deep Galaxy
                    case '*': color = Color.white; break; // Galaxy Stars
                }
                tex.SetPixel(x, y, color);
            }
        }
        tex.Apply();
        File.WriteAllBytes(path, tex.EncodeToPNG());
    }

    private static void ConfigureSpriteSettings(string path, bool isPixelArt = true)
    {
        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
        if (importer != null)
        {
            importer.textureType = TextureImporterType.Sprite;
            if (isPixelArt) 
            {
                importer.spritePixelsPerUnit = 16;
                importer.filterMode = FilterMode.Point;
                importer.textureCompression = TextureImporterCompression.Uncompressed;
            }
            importer.SaveAndReimport();
        }
    }

    private static void ApplySpriteToTagOrName(string targetName, string spritePath, bool isTiled)
    {
        Sprite loadedSprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
        if (loadedSprite == null) return;

        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject go in allObjects)
        {
            if (go.name == targetName || go.CompareTag(targetName))
            {
                SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sprite = loadedSprite;
                    sr.color = Color.white; 
                    
                    if (isTiled && sr.drawMode != SpriteDrawMode.Tiled)
                    {
                        Vector2 scale = go.transform.localScale;
                        if (scale.x != 1f || scale.y != 1f)
                        {
                            sr.drawMode = SpriteDrawMode.Tiled;
                            sr.size = scale;
                            go.transform.localScale = Vector3.one;

                            BoxCollider2D bc = go.GetComponent<BoxCollider2D>();
                            if (bc != null)
                            {
                                bc.size = new Vector2(bc.size.x * scale.x, bc.size.y * scale.y);
                            }
                        }
                    }
                    else if (!isTiled)
                    {
                        // Sửa lỗi hiển thị một nửa ảnh: Ép về Simple Mode
                        sr.drawMode = SpriteDrawMode.Simple;
                        
                        // Những vật thể không lát gạch (Door, Goal, Player...) thì trả về tỉ lệ 1:1, tự bung dựa vào kích thước bản thân
                        Vector2 scale = go.transform.localScale;
                        if (scale.x != 1f || scale.y != 1f)
                        {
                            go.transform.localScale = Vector3.one;
                            BoxCollider2D bc = go.GetComponent<BoxCollider2D>();
                            if (bc != null)
                            {
                                bc.size = new Vector2(bc.size.x * scale.x, bc.size.y * scale.y);
                            }
                        }
                    }
                    EditorUtility.SetDirty(go);
                }
            }
        }
    }
}
