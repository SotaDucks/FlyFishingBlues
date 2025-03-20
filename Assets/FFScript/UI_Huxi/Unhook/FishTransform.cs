using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTransform : MonoBehaviour
{
    [Header("移动区间配置")]
    
  private Vector3 startPoint;
    [Tooltip("终点位置（世界坐标系）")]
    public Vector3 endPoint;
    private void Start()
    {
       startPoint = transform.position;
        Debug.Log(startPoint);
    }
    [Header("运动参数")]
    [Tooltip("完成一次往返所需时间（秒）")]
    public float cycleTime = 0.1f;
    [Tooltip("是否显示运动轨迹")]
    public bool showPath = true;

    private Vector3 currentPosition;
    private float timer;

    void Update()
    {
        // 计算时间比例（0-1之间循环）
        timer += Time.deltaTime;
        float ratio = Mathf.PingPong(timer / cycleTime, 1f);

        // 插值计算当前位置
        currentPosition = Vector3.Lerp(startPoint, endPoint, ratio);
        transform.position = currentPosition;
    }

 
   
}
