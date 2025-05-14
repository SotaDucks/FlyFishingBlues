using System.Collections;
using UnityEngine;
using Obi;

[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(ObiRope), typeof(ObiParticleAttachment))]
public class ObiAttachmentInitWithFreeze : MonoBehaviour
{
    public Transform target;  // 鱼竿顶点 Transform

    ObiRope               rope;
    ObiParticleAttachment attachment;
    Rigidbody             rb;

    void Awake()
    {
        rope       = GetComponent<ObiRope>();
        attachment = GetComponent<ObiParticleAttachment>();
        rb         = target.GetComponent<Rigidbody>();
    }

    void Start()
    {
        StartCoroutine(FreezeResetRebind());
    }

    IEnumerator FreezeResetRebind()
    {
        // 等一帧物理更新，让所有 Awake/Start 都跑完
        yield return new WaitForFixedUpdate();

        // 1) 冻结目标刚体
        if (rb != null) rb.isKinematic = true;

        // 2) 禁用 Rope + Attachment
        rope.enabled = false;
        attachment.enabled = false;

        // 3) 重置粒子到 Blueprint 初始状态
        rope.ResetParticles();

        // 4) 重新启用 Rope + Attachment
        rope.enabled      = true;
        attachment.enabled = true;

        // 5) 解冻目标刚体，恢复正常物理
        if (rb != null) rb.isKinematic = false;
    }
}
