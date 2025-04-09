using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CuttingMan : MonoBehaviour
{
    public float minForce = 5f;  // 最小力的大小
    public float maxForce = 10f; // 最大力的大小
    public float interval = 2f;  // 每隔多少秒施加一次力
    public Image image1;
    public TextMeshProUGUI textMeshProUGUI1;

    private Rigidbody rb;  // 物体的 Rigidbody 组件

    void Start()
    {
        // 获取物体的 Rigidbody 组件
        rb = GetComponent<Rigidbody>();

        // 每隔一段时间调用 ApplyRandomForce 方法
        InvokeRepeating("ApplyRandomForce", 0f, interval);
    }

    void ApplyRandomForce()
    {
        // 生成一个随机的方向（单位向量）
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // 生成一个随机的力的大小
        float randomForce = Random.Range(minForce, maxForce);

        // 在该随机方向上施加力
        rb.AddForce(randomDirection * randomForce, ForceMode.Impulse);

        // 输出施加的力，方便调试
        Debug.Log("Applied Random Force: " + randomDirection * randomForce);
    }
    private void OnTriggerEnter(Collider other)
    {
        // 检查碰撞体是否是鱼的碰撞体
        if (other.CompareTag("Player"))  // 假设鱼的碰撞体设置了 Tag 为 "Fish"
        {
            // 如果是鱼的碰撞体，则执行 Bye 方法
            Bye();
        }
    }

    // Bye 方法
    private void Bye()
    {
        image1.DOFade(1f, 0.2f).OnComplete(() =>
        {
            // 动画完成后，调用 EnableFollowLine 方法
            textMeshProUGUI1.DOFade(1f, 1f);
            Debug.Log("这里切换场景");
        });;
        
    }
}
