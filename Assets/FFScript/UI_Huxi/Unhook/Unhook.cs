using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unhook : MonoBehaviour
{

    public float maxRotation = 40f; // 最大旋转角度
    public float maxSpeed = 100f; // 最大旋转速度
    private float screenCenterX;
    public float moveSpeed = 5f; // 移动速度
    void Start()
    {
        screenCenterX = Screen.width / 2; // 获取屏幕中心 X 坐标
    }
    void Update()
    {
        // 获取水平（A/D）和垂直（W/S）输入
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.A)) moveX = -1f; // 按下 A 向左
        if (Input.GetKey(KeyCode.D)) moveX = 1f;  // 按下 D 向右
        if (Input.GetKey(KeyCode.W)) moveY = 1f;  // 按下 W 向上
        if (Input.GetKey(KeyCode.S)) moveY = -1f; // 按下 S 向下

        // 计算移动向量
        Vector3 moveDirection = new Vector3(moveX, moveY, 0).normalized;

        // 使用世界坐标进行移动
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.Space)) return;
        float mouseX = Input.mousePosition.x; // 获取鼠标 X 位置
        float distanceFromCenter = mouseX - screenCenterX; // 计算鼠标偏移量
        float percentFromCenter = Mathf.Clamp(distanceFromCenter / screenCenterX, -1f, 1f); // 归一化到 [-1, 1]

        // 计算目标旋转角度（最大 ±40°）
        float targetRotation = percentFromCenter * maxRotation;

        // 计算旋转速度，鼠标越远，旋转越快（最大 maxSpeed）
        float rotationSpeed = Mathf.Abs(percentFromCenter) * maxSpeed * Time.deltaTime;

        // 平滑插值旋转（限制在 Z 轴）
        float newZRotation = Mathf.LerpAngle(transform.eulerAngles.z, targetRotation, rotationSpeed);

        // 应用旋转，只允许 Z 轴旋转
        transform.rotation = Quaternion.Euler(0, 180, newZRotation);
    }
}





