using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishTransform : MonoBehaviour
{
    [Header("�ƶ���������")]
    
  private Vector3 startPoint;
    [Tooltip("�յ�λ�ã���������ϵ��")]
    public Vector3 endPoint;
    private void Start()
    {
       startPoint = transform.position;
        Debug.Log(startPoint);
    }
    [Header("�˶�����")]
    [Tooltip("���һ����������ʱ�䣨�룩")]
    public float cycleTime = 0.1f;
    [Tooltip("�Ƿ���ʾ�˶��켣")]
    public bool showPath = true;

    private Vector3 currentPosition;
    private float timer;

    void Update()
    {
        // ����ʱ�������0-1֮��ѭ����
        timer += Time.deltaTime;
        float ratio = Mathf.PingPong(timer / cycleTime, 1f);

        // ��ֵ���㵱ǰλ��
        currentPosition = Vector3.Lerp(startPoint, endPoint, ratio);
        transform.position = currentPosition;
    }

 
   
}
