using UnityEngine;

public class SeaweedWave : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("BlendShape Indexes")]
    public int key1Index = 0; // 往右
    public int key2Index = 1; // 往左
    public int upIndex = 2;   // 往上

    [Header("Animation Settings")]
    public float cycleDuration = 2.0f; // 一个完整左右循环的总时间（单位：秒）
    public float upSpeed = 0.5f;
    public float upAmount = 50f;

    private void Update()
    {
        float time = Time.time;
        float cycleTime = time % cycleDuration;

        float halfCycle = cycleDuration / 2f;
        float progress;

        if (cycleTime < halfCycle)
        {
            // 第一个半周期：key1 从0到100再到0
            progress = cycleTime / halfCycle; // 0~1
            float weight = Mathf.Sin(progress * Mathf.PI); // 0 ➝ 1 ➝ 0
            skinnedMeshRenderer.SetBlendShapeWeight(key1Index, weight * 100);
            skinnedMeshRenderer.SetBlendShapeWeight(key2Index, 0);
        }
        else
        {
            // 第二个半周期：key2 从0到100再到0
            progress = (cycleTime - halfCycle) / halfCycle; // 0~1
            float weight = Mathf.Sin(progress * Mathf.PI); // 0 ➝ 1 ➝ 0
            skinnedMeshRenderer.SetBlendShapeWeight(key1Index, 0);
            skinnedMeshRenderer.SetBlendShapeWeight(key2Index, weight * 100);
        }

        // 上下浮动：持续正弦波
        float up = Mathf.Sin(time * upSpeed) * upAmount + upAmount / 2;
        skinnedMeshRenderer.SetBlendShapeWeight(upIndex, Mathf.Clamp(up, 0, 100));
    }
}

