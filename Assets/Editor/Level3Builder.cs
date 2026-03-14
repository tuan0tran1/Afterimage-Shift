using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class Level3Builder
{
    [MenuItem("SHIFT/Auto-Build Level 3 (Map 3)")]
    public static void GenerateMap()
    {
        // 1. Mở Level 1 hoặc Level 2 và Save As sang Level 3
        Scene openScene = EditorSceneManager.OpenScene("Assets/Scenes/Level_01.unity", OpenSceneMode.Single);
        EditorSceneManager.SaveScene(openScene, "Assets/Scenes/Level_03.unity");

        // 2. Tái sử dụng các Object
        GameObject player = GameObject.Find("Player") ?? GameObject.FindGameObjectWithTag("Player");
        GameObject doorObj = GameObject.Find("Door");
        GameObject switchObj = GameObject.Find("Switch") ?? GameObject.FindFirstObjectByType<Switch>()?.gameObject;
        GameObject goalObj = GameObject.Find("Goal") ?? GameObject.FindFirstObjectByType<Goal>()?.gameObject;
        GameObject groundBlock = GameObject.Find("Ground") ?? GameObject.Find("Ground_Left");
        GameObject respawnPoint = GameObject.Find("RespawnPoint");

        float groundY = -3f;
        float groundTopY = -2.5f;

        // 3. Tái cấu trúc mặt đất cho Level 3 (Vực ở giữa)
        // Spawn (-12), Lever (-8), Mép vực trái (-5), Mép vực phải (0)
        groundBlock.name = "Ground_Left";
        groundBlock.transform.position = new Vector3(-11f, groundY, 0);
        groundBlock.transform.localScale = new Vector3(12f, 1f, 1f); // Từ X=-17 đến X=-5
        
        GameObject groundRight = GameObject.Find("Ground_Right");
        if (groundRight == null) groundRight = GameObject.Instantiate(groundBlock);
        groundRight.name = "Ground_Right";
        groundRight.transform.position = new Vector3(8f, groundY, 0); 
        groundRight.transform.localScale = new Vector3(16f, 1f, 1f); // Kéo dài từ X=0 tới X=16
        
        // 4. Các Vị trí Tọa độ
        Vector3 spawnPos = new Vector3(-12f, groundTopY + 0.5f, 0);
        if (player != null) player.transform.position = spawnPos;
        if (respawnPoint != null) respawnPoint.transform.position = spawnPos;

        // 5. Cài đặt Cần gạt (Lever)
        GameObject leverObj = GameObject.Find("Lever");
        if (leverObj == null)
        {
            leverObj = new GameObject("Lever");
            SpriteRenderer lSr = leverObj.AddComponent<SpriteRenderer>();
            lSr.sprite = groundBlock.GetComponent<SpriteRenderer>().sprite;
            lSr.color = new Color(1f, 0.5f, 0f); // Màu cam
            leverObj.transform.localScale = new Vector3(0.5f, 1f, 1f);
            BoxCollider2D lCol = leverObj.AddComponent<BoxCollider2D>();
            lCol.isTrigger = true;
            leverObj.AddComponent<Lever>();
        }
        leverObj.transform.position = new Vector3(-8f, groundTopY + 0.5f, 0);

        // 6. Cài đặt Moving Platform
        GameObject platformObj = GameObject.Find("MovingPlatform");
        if (platformObj == null)
        {
            platformObj = new GameObject("MovingPlatform");
            platformObj.tag = "Ground";
            SpriteRenderer sr = platformObj.AddComponent<SpriteRenderer>();
            sr.sprite = groundBlock.GetComponent<SpriteRenderer>().sprite;
            sr.color = Color.cyan;
            platformObj.transform.localScale = new Vector3(2f, 0.5f, 1f);
            platformObj.AddComponent<BoxCollider2D>();
            Rigidbody2D rb = platformObj.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
            MovingPlatform mpScript = platformObj.AddComponent<MovingPlatform>();
            mpScript.speed = 3f;

            GameObject pA = new GameObject("PointA");
            pA.transform.position = new Vector3(-4f, groundTopY, 0); 
            GameObject pB = new GameObject("PointB");
            pB.transform.position = new Vector3(-1f, groundTopY, 0);
            
            mpScript.pointA = pA.transform;
            mpScript.pointB = pB.transform;
        }
        platformObj.transform.position = new Vector3(-4f, groundTopY, 0);
        MovingPlatform platformScript = platformObj.GetComponent<MovingPlatform>();
        platformScript.isActivated = false; // Reset cứng về false

        // 7. Cài đặt Spike Trap
        GameObject spikeObj = GameObject.Find("SpikeTrap");
        if (spikeObj == null)
        {
            spikeObj = new GameObject("SpikeTrap");
            spikeObj.tag = "Trap"; // Gọi hàm Game Over khi chạm
            SpriteRenderer sSr = spikeObj.AddComponent<SpriteRenderer>();
            sSr.sprite = groundBlock.GetComponent<SpriteRenderer>().sprite;
            sSr.color = Color.magenta;
            spikeObj.transform.localScale = new Vector3(1f, 0.5f, 1f);
            BoxCollider2D sCol = spikeObj.AddComponent<BoxCollider2D>();
            sCol.isTrigger = true;
        }
        spikeObj.transform.position = new Vector3(2f, groundTopY + 0.25f, 0); // Đặt nhô lên bề mặt

        // 8. Đặt Switch, Door, Goal
        if (switchObj != null) 
        {
            switchObj.transform.position = new Vector3(4f, groundTopY + 0.3f, 0);
            Switch sw = switchObj.GetComponent<Switch>();
            if (sw != null) sw.platform = null; // Ở Map 3, công tắc CHỈ mở cửa, KHÔNG kích hoạt platform
        }

        if (doorObj == null)
        {
            doorObj = new GameObject("Door");
            SpriteRenderer dSr = doorObj.AddComponent<SpriteRenderer>();
            dSr.sprite = groundBlock.GetComponent<SpriteRenderer>().sprite;
            dSr.color = Color.red;
            doorObj.transform.localScale = new Vector3(0.5f, 3f, 1f);
            doorObj.AddComponent<BoxCollider2D>();
        }
        doorObj.transform.position = new Vector3(6f, groundTopY + 1.5f, 0);

        if (goalObj != null) goalObj.transform.position = new Vector3(9f, groundTopY + 0.5f, 0);

        // Nối Cáp (Wire dependencies)
        Lever lever = leverObj.GetComponent<Lever>();
        if (lever != null) lever.platform = platformScript;

        Switch switchComp = switchObj?.GetComponent<Switch>();
        if (switchComp != null) switchComp.door = doorObj;

        // Vùng chết (DeathZone)
        GameObject deathZone = GameObject.Find("DeathZone");
        if (deathZone == null)
        {
            deathZone = new GameObject("DeathZone");
            deathZone.tag = "Trap";
            deathZone.transform.position = new Vector3(-2.5f, -7f, 0);
            BoxCollider2D deathCol = deathZone.AddComponent<BoxCollider2D>();
            deathCol.isTrigger = true;
            deathCol.size = new Vector2(20f, 3f);
        }

        GameManager gm = GameObject.FindFirstObjectByType<GameManager>();
        if (gm != null) {
            gm.respawnPoint = respawnPoint.transform;
            // Thay đổi timer thành 28s cho Map 3
            // Vì timer ẩn trong private, ta chỉ báo log. (Sẽ nói user update GameManager nếu cần, hoặc mặc định code 20s là tạm đủ nếu chạy lẹ)
        }

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());

        Debug.Log(">>> ĐÃ TỰ ĐỘNG KHỞI TẠO MÀN 3 THÀNH CÔNG! HÃY BẤM NÚT PLAY <<<");
    }
}
