using UnityEngine;

public class FishStruggle : MonoBehaviour
{
    [Header("��������")]
    public float struggleForce = 5f; // ��ֱ��Ծ����
    public float horizontalMoveForce = 2f; // ˮƽ�ƶ�����
    public float struggleFrequency = 1f; // ����Ƶ��
    public float groundLevel = 0f; // ����߶�
    public float maxHorizontalMovement = 5f; // ˮƽ�ƶ���Χ
    public float maxRotationZ = 80f; // Z ����ת�Ƕ�����

    private Vector3 initialPosition;
    private Rigidbody parentRb;
    private bool isOnGround = true;
    private float timer;
    private float zRotation;

    void Start()
    {
        // ��ȡ�������ϵ� Rigidbody
        parentRb = GetComponentInParent<Rigidbody>();

        // ��ȡ��ʼλ��
        initialPosition = parentRb.transform.position;

        // ��ʼ����ʱ��
        timer = 0f;
    }

    void Update()
    {
        // ����ʱ��
        timer += Time.deltaTime;

        // ������ڵ�����
        if (isOnGround)
        {
            // ÿ������Ƶ��ʱ��ִ��һ������
            if (timer >= struggleFrequency)
            {
                Struggle();
                timer = 0f; // ���ü�ʱ��
            }
        }

        // ���� Z �����ת
        zRotation = Mathf.Clamp(transform.localEulerAngles.z, -maxRotationZ, maxRotationZ);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, zRotation);
    }

    // ������Ϊ
    void Struggle()
    {
        // ���һ������Ĵ�ֱ����������Ծ��
        parentRb.AddForce(Vector3.up * struggleForce, ForceMode.Impulse);

        // ���һ�������ˮƽ�������������ƶ���
        float randomDirection = Random.Range(-1f, 1f);
        Vector3 horizontalForce = new Vector3(randomDirection * horizontalMoveForce, 0, 0);
        parentRb.AddForce(horizontalForce, ForceMode.Impulse);

        // ���ˮƽλ���Ƿ񳬳���Χ���������ˮƽ�ƶ�
        Vector3 clampedPosition = parentRb.transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, initialPosition.x - maxHorizontalMovement, initialPosition.x + maxHorizontalMovement);
        parentRb.transform.position = clampedPosition;
    }

    // ��ײ��⣬����Ƿ��ŵ�
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            isOnGround = false;
        }
    }
}
