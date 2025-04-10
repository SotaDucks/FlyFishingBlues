using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManGoWithShark : MonoBehaviour
{
    public GameObject objectA; // 物体A
    public GameObject objectB; // 物体B
    public float moveSpeed = 2f; // 控制物体A向物体B移动的速度

    private bool isFollowing = false;

    void Update()
    {
        // 当按下空格键时，物体A开始向物体B移动
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFollowing = true;
        }

        // 如果正在跟随物体B，物体A缓慢移动到物体B的位置
        if (isFollowing)
        {
            // 使用Lerp进行平滑的移动
            objectA.transform.position = Vector3.Lerp(objectA.transform.position, objectB.transform.position, moveSpeed * Time.deltaTime);
        }
    }
}
