using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class Level2Builder
{
    [MenuItem("SHIFT/Auto-Build Level 2 (Map 2)")]
    public static void GenerateMap()
    {
        // 1. Mở Level 1 và Save As sang Level 2
        Scene openScene = EditorSceneManager.OpenScene("Assets/Scenes/Level_01.unity", OpenSceneMode.Single);
        EditorSceneManager.SaveScene(openScene, "Assets/Scenes/Level_02.unity");

        // 2. Tìm các vật thể sẵn có
        GameObject player = GameObject.Find("Player") ?? GameObject.FindGameObjectWithTag("Player");
        GameObject doorObj = GameObject.Find("Door");
        GameObject switchObj = GameObject.Find("Switch") ?? GameObject.FindFirstObjectByType<Switch>()?.gameObject;
        GameObject goalObj = GameObject.Find("Goal") ?? GameObject.FindFirstObjectByType<Goal>()?.gameObject;
        GameObject groundBlock = GameObject.Find("Ground");
        GameObject respawnPoint = GameObject.Find("RespawnPoint");

        if (groundBlock == null) { Debug.LogError("Không tìm thấy Ground từ Level 1, hãy kiểm tra lại!"); return; }

        float groundY = -3f;
        float groundTopY = -2.5f; // Giả sử trung bình bề mặt là -2.5 (sát đất hơn)

        // 3. Tái cấu trúc Mặt đất (Tạo vực: Gap X=0 đến X=5)
        groundBlock.name = "Ground_Left";
        groundBlock.transform.position = new Vector3(-6f, groundY, 0);
        groundBlock.transform.localScale = new Vector3(12f, 1f, 1f); // Kéo dài từ X=-12 tới X=0
        
        // Tạo mặt đất bên phải
        GameObject groundRight = GameObject.Instantiate(groundBlock);
        groundRight.name = "Ground_Right";
        groundRight.transform.position = new Vector3(11f, groundY, 0); 
        groundRight.transform.localScale = new Vector3(12f, 1f, 1f); // Kéo dài từ X=5 tới X=17
        
        // 4. Sắp xếp Vị trí
        Vector3 spawnPos = new Vector3(-10f, groundTopY + 0.5f, 0); // Hạ Y sát bề mặt
        if (player != null) player.transform.position = spawnPos;
        if (respawnPoint != null) respawnPoint.transform.position = spawnPos;

        // Đặt Switch (Cách Spawn 4 unit -> Target X = -6)
        if (switchObj != null) switchObj.transform.position = new Vector3(-6f, groundTopY + 0.3f, 0);
        
        // 5. Cài đặt Moving Platform (Khoảng giữa 0 và 5)
        GameObject platformObj = GameObject.Find("MovingPlatform");
        if (platformObj == null)
        {
            platformObj = new GameObject("MovingPlatform");
            platformObj.tag = "Ground"; // Để nhảy được
            
            SpriteRenderer sr = platformObj.AddComponent<SpriteRenderer>();
            sr.sprite = groundBlock.GetComponent<SpriteRenderer>().sprite; // Dùng ảnh của mặt đất
            sr.color = Color.cyan; // Cho khác màu chút cho ngầu

            platformObj.transform.localScale = new Vector3(2f, 0.5f, 1f);
            platformObj.transform.position = new Vector3(1.5f, groundTopY, 0);
            
            platformObj.AddComponent<BoxCollider2D>();
            Rigidbody2D rb = platformObj.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;

            MovingPlatform mpScript = platformObj.AddComponent<MovingPlatform>();
            mpScript.speed = 3f;

            // Thiết lập Điểm đi/về
            GameObject pA = new GameObject("PointA");
            pA.transform.position = new Vector3(1f, groundTopY, 0); 
            GameObject pB = new GameObject("PointB");
            pB.transform.position = new Vector3(4f, groundTopY, 0);
            
            mpScript.pointA = pA.transform;
            mpScript.pointB = pB.transform;
        }

        // 6. Đặt lại Cửa và Đích 
        if (doorObj == null) // Nếu chưa có cửa, tạo tạm
        {
            doorObj = new GameObject("Door");
            SpriteRenderer dSr = doorObj.AddComponent<SpriteRenderer>();
            dSr.sprite = groundBlock.GetComponent<SpriteRenderer>().sprite;
            dSr.color = Color.red;
            doorObj.transform.localScale = new Vector3(0.5f, 3f, 1f);
            doorObj.AddComponent<BoxCollider2D>();
        }
        doorObj.transform.position = new Vector3(8f, groundTopY + 1.5f, 0);

        if (goalObj != null) goalObj.transform.position = new Vector3(13f, groundTopY + 0.5f, 0);

        // 7. Kết nối Dây/Cáp
        if (switchObj != null)
        {
            Switch sw = switchObj.GetComponent<Switch>();
            if (sw != null) sw.platform = platformObj.GetComponent<MovingPlatform>();
            if (sw != null) sw.door = doorObj;
        }

        GameManager gm = GameObject.FindFirstObjectByType<GameManager>();
        if (gm != null) {
            gm.respawnPoint = respawnPoint.transform;
        }

        // 8. Tạo Vùng Chết (Death Zone) dưới vực
        GameObject deathZone = GameObject.Find("DeathZone");
        if (deathZone == null)
        {
            deathZone = new GameObject("DeathZone");
            deathZone.tag = "Trap"; // Dùng tag Trap để tự động kích hoạt hàm Die()
            deathZone.transform.position = new Vector3(2.5f, -7f, 0);
            
            BoxCollider2D deathCol = deathZone.AddComponent<BoxCollider2D>();
            deathCol.isTrigger = true;
            deathCol.size = new Vector2(25f, 3f); // Trải rộng khắp phía dưới
        }

        // Lưu thay đổi
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

        Debug.Log(">>> ĐÃ TỰ ĐỘNG KHỞI TẠO MÀN 2 THÀNH CÔNG! HÃY BẤM NÚT PLAY <<<");
    }
}
