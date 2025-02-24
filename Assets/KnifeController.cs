using DynamicMeshCutter;
using System.Collections;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    [Header("�ƶ�����")]
    public float moveSpeed = 5f;          // W��A��S��D�����ƶ��ٶ�

    [Header("λ������")]
    public float moveDownDistance = 0.8f; // ���¿ո�ʱ������Y�����Ƶľ���
    public float moveDownDuration = 0.4f; // ���Ƶĳ���ʱ��

    private Vector3 originalPosition;     // ����ԭʼλ��

    private bool isSpacePressed = false;  // �Ƿ��¿ո��
    private bool isAnimating = false;     // �Ƿ�����ִ�ж���

    private Coroutine currentCoroutine = null;  // ��ǰ���е�Э��
    private PlaneBehaviour planeBehaviour;

    void Start()
    {
        // �洢����ԭʼλ��
        originalPosition = transform.position;

        planeBehaviour = GetComponentInChildren<PlaneBehaviour>(); // ��ȡ�Ӷ����е�PlaneBehaviour���
    }

    void Update()
    {
        HandleMovement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSpacePressed = true;
            StartCutAction();
        }
    }

    // ����W��A��S��D�����ƶ�
    void HandleMovement()
    {
        if (isSpacePressed || isAnimating)
            return;  // ����ո��»򶯻�����ִ�У�������ƶ�

        float moveX = 0f;
        float moveZ = 0f;

        // ����������Ұ����Ƿ񱻰���
        if (Input.GetKey(KeyCode.W))
        {
            moveZ += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveZ -= 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX -= 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX += 1f;
        }

        // ִ���ƶ�����
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }

    // ��ʼ�ո��º�Ķ����������и���Ϊ
    void StartCutAction()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(SpacePressedRoutine());
    }

    // Э�̣����¿ո��ʱ�Ķ���
    IEnumerator SpacePressedRoutine()
    {
        isAnimating = true;

        // ���ƣ���Y�ᣩ
        Vector3 targetDownPosition = originalPosition + new Vector3(0, -moveDownDistance, 0);
        yield return StartCoroutine(MoveToY(targetDownPosition.y, moveDownDuration));

        // �и�����ڵ�������ɺ����
        planeBehaviour.Cut();

        // �ȴ�һС��ʱ�䣨��ѡ��
        yield return new WaitForSeconds(0.1f);

        // �Զ���λλ�ã���Y�ᣩ
        yield return StartCoroutine(MoveToY(originalPosition.y, moveDownDuration * 2));

        isAnimating = false;
        isSpacePressed = false; // ���ÿո��״̬����ѡ��������Ҫ��
    }

    // Э�̣�ƽ���ƶ���Ŀ��Yλ�ã���Y�ᣩ
    IEnumerator MoveToY(float targetY, float duration)
    {
        float startY = transform.position.y;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float newY = Mathf.Lerp(startY, targetY, elapsed / duration);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }
    public void deleteTHeknife()
    {

        KnifeController knifeController = GetComponentInChildren<KnifeController>();
        // ��� MeshRenderer ���Ӷ�����
        MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.enabled = false;
        knifeController.enabled = false;
    }
}
