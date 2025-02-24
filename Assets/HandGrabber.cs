// HandGrabber.cs
using UnityEngine;

public class HandGrabber : MonoBehaviour
{
    public string grabButton = "Fire1"; // ץȡ������Ĭ������������
    public Transform holdPoint;         // ץȡ�������õ�λ�ã��ֲ���ĳ���Ӷ���
    public float grabRange = 2f;        // ץȡ��Χ
    public LayerMask grabbableLayer;    // ��ץȡ����Ĳ�

    private Grabbable grabbedObject = null; // ��ǰץȡ������

    void Update()
    {
        if (Input.GetButtonDown(grabButton))
        {
            if (grabbedObject == null)
            {
                TryGrabObject();
            }
            else
            {
                ReleaseObject();
            }
        }
    }

    public void TryGrabObject()
    {
        // ��ץȡ��Χ�ڽ������μ�⣬Ѱ�ҿ�ץȡ������
        Collider[] hits = Physics.OverlapSphere(transform.position, grabRange, grabbableLayer);
        foreach (var hit in hits)
        {
            Grabbable grabbable = hit.GetComponent<Grabbable>();
            if (grabbable != null)
            {
                GrabObject(grabbable);
                break;
            }
        }
    }

    void GrabObject(Grabbable grabbable)
    {
        grabbedObject = grabbable;

        // �������������Ч��
        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // ����������Ϊ�ֲ����Ӷ��󣬷����� holdPoint λ��
        grabbedObject.transform.SetParent(transform); // ����������Ϊ�ֲ����Ӷ���
        grabbedObject.transform.localPosition = Vector3.zero;
        grabbedObject.transform.localRotation = Quaternion.identity;

    }

    public void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            // ������ӹ�ϵ
            grabbedObject.transform.SetParent(null);

            // �������������Ч��
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            grabbedObject = null;
        }
    }

    // ��ѡ�����ӻ�ץȡ��Χ
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, grabRange);
    }
}
