using UnityEngine;
using Obi;

public class FishBiteHook : MonoBehaviour
{
    public Transform flyhook; // �ɹ��� Transform ����
    public Transform exit1; // Exit1 �� Transform ����
    public float moveSpeed = 3f; // ����ɹ��ƶ����ٶ�?
    public float stopDistance = 0.5f; // ��ֹͣ�ƶ�����С����
    public float waitTime = 1f; // �ȴ�ʱ��
    public float escapeSpeed = 5f; // �����ܵ��ٶ�
    public bool isFishBite = false; // ���Ƿ�ҧ��

    private Animator fishAnimator; // ���? Animator ���?
    private Animator characterAnimator; // ������ Character �� Animator ���? 
    private bool isMovingToHook = true; // ����Ƿ�����ɹ��ƶ�
    private float waitTimer = 0f; // �ȴ���ʱ��
    private FishDragLine fishDragLine; // ���� FishDragLine �ű�

    void Start()
    {
        GameObject flyhookObject = GameObject.Find("flyhook");
        if (flyhookObject != null)
        {
            flyhook = flyhookObject.transform;
        }
        else
        {
            Debug.LogError("δ�ҵ���Ϊ 'flyhook' �� GameObject��");
        }

        fishAnimator = GetComponent<Animator>(); // �Զ��������? Animator ���?
        if (fishAnimator == null)
        {
            Debug.LogError("δ�ҵ����? Animator �����?");
        }

        GameObject characterObject = GameObject.Find("autoriggedmainch"); // �Զ����ҳ����е� Character
        if (characterObject != null)
        {
            characterAnimator = characterObject.GetComponent<Animator>();
            if (characterAnimator == null)
            {
                Debug.LogError("δ�ҵ� Character �� Animator �����?");
            }
        }
        else
        {
            Debug.LogError("δ�ҵ���Ϊ 'Character' �� GameObject��");
        }

        fishDragLine = GameObject.Find("FlyLine").GetComponent<FishDragLine>(); // ��ȡ FishDragLine ���?
        if (fishDragLine == null)
        {
            Debug.LogError("δ�ҵ� FishDragLine �������ȷ�ϸýű�������? FlyLine �ϣ�");
        }
    }

    void Update()
    {
        if (isMovingToHook)
        {
            MoveToHook();
        }
        else
        {
            EscapeToExit();
        }
    }

    private void MoveToHook()
    {
        float distanceToHook = Vector3.Distance(transform.position, flyhook.position);
        if (distanceToHook > stopDistance)
        {
            Vector3 direction = (flyhook.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, flyhook.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            AttachFishToFlyline(); // ���㵽 FlyLine
            isMovingToHook = false; // ������С���룬��ʼ�ȴ�
            waitTimer = waitTime; // ���õȴ���ʱ��

            // ������TroutBite������
            if (fishAnimator != null)
            {
                fishAnimator.SetTrigger("TroutBite");
            }
        }
    }


    private void EscapeToExit()
    {
        isFishBite = true;
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime; // ���ٵȴ�ʱ��
        }
        else
        {
            // ���? Character �Ƿ񲥷��� "SetTheHook" ����
            if (characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("SetTheHook"))
            {
                if (fishDragLine != null)
                {
                    fishDragLine.StopDragging();
                }

                // �������? Rigidbody �� IsKinematic Ϊ false
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false; // ���? Kinematic ״̬
                }

                this.enabled = false; // ���õ�ǰ�ű�
                GetComponent<FishLanding>().enabled = true; // ���� FishLanding �ű�
                return; // ������ǰ����
            }

            // ���ܿ�ʼʱ�л�����
            
            characterAnimator.SetBool("FishOn", true); // ���� Character ������ FishOn Ϊ true
  

            // ��ʼ��������
            if (fishDragLine != null)
            {
                fishDragLine.StartDragging();
            }

            Vector3 direction = (exit1.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, exit1.position, escapeSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(direction); // ���� Exit1

            // ����Ƿ񵽴�? Exit1
            if (Vector3.Distance(transform.position, exit1.position) < stopDistance)
            {
                Destroy(transform.parent.gameObject); // ������ĸ�����?
                Debug.Log("��ĸ�����? 'Trout1SpawnPrefab2' �ѱ����٣�");
                // ������ߵ��ӳ�?
                if (fishDragLine != null)
                {
                    fishDragLine.StopDragging();
                }
            }
        }
    }

    private void AttachFishToFlyline()
    {
        // �Զ�������Ϊ "FlyLine" �Ķ���
        GameObject flyLine = GameObject.Find("FlyLine");

        if (flyLine != null)
        {
            // ��ȡ FlyLine �����ϵ����� ObiParticleAttachment ���?
            ObiParticleAttachment[] attachments = flyLine.GetComponents<ObiParticleAttachment>();

            if (attachments.Length >= 3) // ȷ������������ Obi Particle Attachment
            {
                // �������� Obi Particle Attachment ����Ŀ��Ϊ������
                attachments[2].target = this.transform; // �����? Transform ��ΪĿ��
                Debug.Log("���Ѿ����󶨵� FlyLine �ĵ����� Obi Particle Attachment��");
            }
            else
            {
                Debug.LogError("FlyLine ��û���㹻�� Obi Particle Attachment �����?");
            }
        }
        else
        {
            Debug.LogError("δ�ҵ���Ϊ 'FlyLine' �Ķ���");
        }
    }
}
