using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WashingHands : MonoBehaviour
{
    public GameObject Fish;  // 需要激活的 Fish 对象
    public GameObject Hands; // 手的 GameObject
    public bool WashHands = false;
    public GameObject tutorial;

    // 启动洗手流程的方法
    public void WashingHandsMethod()
    {
        Fish.gameObject.SetActive(false); // 先隐藏 Fish 对象
        WashHands = true;                 // 设置 WashHands 为 true
        CameratoBucket(-20,20);                    // 执行手的动画
    }
    private void CameratoBucket(float fromAngle, float toAngle)
    {
        Camera mainCamera = Camera.main;
        mainCamera.transform.DORotate(
               new Vector3(mainCamera.transform.eulerAngles.x, toAngle, mainCamera.transform.eulerAngles.z),
               1f  // 动画持续时间 1 秒
           ).SetEase(Ease.InOutQuad)  // 平滑的缓动曲线
            .OnComplete(() => HandsMoving()); // 旋转完成时激活 Fish
    }
    // 手的移动动画
    private void HandsMoving()
    {
        float currentY = Hands.transform.position.y; // 获取物体当前的Y坐标
        Hands.transform.DOMoveY(currentY -0.5f, 1f) // 从当前位置向上移动0.5个单位，持续1秒
            .SetLoops(2, LoopType.Yoyo)              // 设置循环2次，使用Yoyo类型（往返运动）
            .SetEase(Ease.InOutQuad)            // 平滑的缓动曲线
            .OnComplete(() => CameraRotation(20, -20)); // 动画完成后旋转相机
    }

    // 相机旋转方法
    private void CameraRotation(float fromAngle, float toAngle)
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            // 设置相机初始角度（只修改 Y 轴）
            mainCamera.transform.eulerAngles = new Vector3(
                mainCamera.transform.eulerAngles.x,
                fromAngle,
                mainCamera.transform.eulerAngles.z
            );

            // 执行相机旋转动画到目标角度
            mainCamera.transform.DORotate(
                new Vector3(mainCamera.transform.eulerAngles.x, toAngle, mainCamera.transform.eulerAngles.z),
                1f  // 动画持续时间 1 秒
            ).SetEase(Ease.InOutQuad)  // 平滑的缓动曲线
             .OnComplete(() => ActivateFish()); // 旋转完成时激活 Fish
        }
        else
        {
            Debug.LogError("主相机未找到！");
        }
    }

    // 激活 Fish 对象的方法
    private void ActivateFish()
    {
        Fish.gameObject.SetActive(true); // 将 Fish 设置为可见
        tutorial.gameObject.SetActive(true);
}
}