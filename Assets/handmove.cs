using UnityEngine;

public class handmove : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float height;
    public float originalHeight = 1.5f;
    public float lowdownhand = 0.6f;
    public float distance = 0.5f; // Z��ľ��루������������õ�����
    private Animator animator;
    private bool isGrabbing = false; // ����Ƿ����������ץȡ״̬��

    public bool allowHandMovementWhileGrabbing = true; // ����ץȡʱ�Ƿ������ֲ��ƶ�

    private HandGrabber handGrabber;

    void Start()
    {
        // ��ȡ Animator ���
        animator = GetComponent<Animator>();

        // ��ȡ HandGrabber �ű�
        handGrabber = GetComponent<HandGrabber>();
    }

    void Update()
    {
        // �������������
        if (Input.GetMouseButtonDown(0))
        {
            // ����ץ�Ķ���
            animator.Play("GrabHold");

            // ���Ϊץȡ״̬
            isGrabbing = true;

            // ���ֵ�Y�����0.6
            MoveHandDown();

            // ����ץȡ����
            if (handGrabber != null)
            {
                handGrabber.TryGrabObject();
            }
        }

        // ����������ɿ�
        if (Input.GetMouseButtonUp(0))
        {
            // ���ŷ��ֵĶ���
            animator.Play("GrabRelease");

            // �ָ���Ĭ�ϵ�Y��߶�
            ResetHandY();

            // ����ץȡ״̬
            isGrabbing = false;

            // �ͷ�����
            if (handGrabber != null)
            {
                handGrabber.ReleaseObject();
            }
        }

        // �� GameObject ������ƶ�
        MoveWithMouse();
    }

    // �� GameObject ��Ϊ����ƶ�
    void MoveWithMouse()
    {
        // ���������ץȡʱ�ƶ�������ץȡ���򲻸����ֲ�λ��
        if (isGrabbing && !allowHandMovementWhileGrabbing)
        {
            return;
        }

        // ��ȡ���λ�ò�����ת��Ϊ��������
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = distance; // ����Z����루������������õ�����

        // ��������Ļ����ת��Ϊ��������
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // �����ֲ��ĸ߶�
        if (!isGrabbing)
        {
            worldPosition.y = originalHeight;  // �ڷ�ץȡ״̬ʱ������Y��Ϊԭ�߶�
        }
        else
        {
            worldPosition.y = Mathf.Lerp(worldPosition.y, height, Time.deltaTime * moveSpeed);
        }

        // �����ֲ���λ��
        transform.position = worldPosition;
    }

    // ���ֵ�Y�����0.6
    void MoveHandDown()
    {
        // ����Y��߶�
        height = originalHeight - lowdownhand;
    }

    // �ָ��ֵ�Ĭ��Y��߶�
    void ResetHandY()
    {
        height = originalHeight; // �ָ�����ʼ��Y��߶�
    }
}
