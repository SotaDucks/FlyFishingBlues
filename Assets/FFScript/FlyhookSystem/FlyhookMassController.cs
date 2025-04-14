using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyhookMassController : MonoBehaviour
{
    public BoxCollider waterSurfaceCollider;  // ����WaterSurfaceCollider��Box Collider
    public float defaultMass = 1.0f;          // flyhook��Ĭ������
    public float waterMass = 0.5f;            // flyhook��ˮ�е�����
    public float castingMass = 0.8f;          // flyhook���׸�ʱ������
    private Rigidbody flyhookRigidbody;       // flyhook�ĸ������
    public bool isInWater = false;           // ���flyhook�Ƿ���ˮ��
    private Animator characterAnimator;       // Character�Ķ������

    void Start()
    {
        // ��ȡflyhook�ĸ������
        flyhookRigidbody = GetComponent<Rigidbody>();
        if (flyhookRigidbody == null)
        {
            Debug.LogError("Flyhook��ȱ��Rigidbody�����");
        }

        // ���ó�ʼ����ΪĬ������
        flyhookRigidbody.mass = defaultMass;

        // ��ȡ��Ϊ"Character"��GameObject����ȡ��Animator���
        GameObject characterObject = GameObject.Find("autoriggedmainch");
        if (characterObject != null)
        {
            characterAnimator = characterObject.GetComponent<Animator>();
            if (characterAnimator == null)
            {
                Debug.Log("Character������ȱ��Animator�����");
            }
        }
        else
        {
            Debug.Log("������δ�ҵ���Ϊ'Character'��GameObject��");
        }
    }

    void Update()
    {
        // ���flyhook��ˮ�У�����ʹ��waterMass
        if (isInWater)
        {
            flyhookRigidbody.mass = waterMass;
        }
        // ���Character���ڲ���"CastIntoWater"������ʹ��castingMass
        else if (characterAnimator != null && characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("CastIntoWater"))
        {
            flyhookRigidbody.mass = castingMass;
            Debug.Log($"Flyhook �����׸ͣ���������Ϊ {castingMass}");
        }
        // ����ʹ��Ĭ������
        else
        {
            flyhookRigidbody.mass = defaultMass;
        }
    }

    // ������������ˮ��
    private void OnTriggerEnter(Collider other)
    {
        // �ж�flyhook�Ƿ����WaterSurfaceCollider
        if (other == waterSurfaceCollider)
        {
            isInWater = true;
            flyhookRigidbody.mass = waterMass;
            Debug.Log($"Flyhook ����ˮ�У���������Ϊ {waterMass}");
        }
    }

    // ����������뿪ˮ��
    private void OnTriggerExit(Collider other)
    {
        // �ж�flyhook�Ƿ��뿪WaterSurfaceCollider
        if (other == waterSurfaceCollider)
        {
            isInWater = false;
            flyhookRigidbody.mass = defaultMass;
           
        }
    }
}
