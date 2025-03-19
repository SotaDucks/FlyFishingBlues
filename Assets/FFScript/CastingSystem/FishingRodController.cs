using UnityEngine;

public class FishingRodController : MonoBehaviour
{
    public Transform rightHand;
    public Transform bone1;
    public float followSpeed = 10f;
    public float rotationSpeed = 10f;
    
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (bone1 == null)
        {
            bone1 = transform.Find("bone_1");
        }
    }

    void Update()
    {
        if (rightHand != null && bone1 != null)
        {
            // 平滑跟随右手位置
            bone1.position = Vector3.Lerp(bone1.position, rightHand.position, Time.deltaTime * followSpeed);
            
            // 平滑跟随右手旋转
            bone1.rotation = Quaternion.Lerp(bone1.rotation, rightHand.rotation, Time.deltaTime * rotationSpeed);
        }
    }
} 