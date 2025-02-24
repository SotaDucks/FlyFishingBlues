using DynamicMeshCutter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerForEscape : MonoBehaviour
{
    public fishstruggling fishstruggling;
    public float escapeInterval = 2f; // ÿ��2�����һ��EscapeJump
    private float timer = 0f;

 
    private void OnTriggerEnter(Collider other)
    {

        // ����Ƿ��Ǵ��� "fish" ��ǩ������
        if (other.gameObject.tag == "fish")
        {
            // ��ȡ fishstruggling ���
            if (fishstruggling != null)
            {
                Debug.Log("there you are");
                fishstruggling.EscapeJump();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // ����Ƿ��Ǵ��� "fish" ��ǩ������
        if (other.gameObject.tag == "fish" )
        {
            // ���¼�ʱ��
            timer += Time.deltaTime;

            // ����ʱ���ﵽ���Ѽ��ʱ������ EscapeJump ����
            if (timer >= escapeInterval)
            {
                Debug.Log("runrunforyourlife");
                fishstruggling.EscapeJump();
                timer = 0f; // ���ü�ʱ��
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �����뿪��������ʱ������ fishstruggling �ͼ�ʱ��
        if (other.gameObject.tag == "fish" )
        {
            fishstruggling = null;
            timer = 0f;
            Debug.Log("Fish left the area");
        }
    }
}
