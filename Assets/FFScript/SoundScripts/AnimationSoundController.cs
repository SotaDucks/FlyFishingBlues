using UnityEngine;

/// <summary>
/// 根据 Animator 当前播放的动画剪辑以及 FishDragLine 的拖拽状态
/// 播放对应的音效（一次性音效与持续音效）。
/// </summary>
public class AnimationSoundController : MonoBehaviour
{
    [Header("动画与音效关联")]
    [Tooltip("用于获取当前播放的动画剪辑")]  
    public Animator animator; // Animator 组件引用，用于获取当前动画剪辑信息

    [Tooltip("播放一次性动作音效的 AudioSource")]  
    public AudioSource audioSource; // 用于播放与动画剪辑关联的音效

    [System.Serializable]
    public class AnimationSoundPair
    {
        [Tooltip("动画剪辑名称，与 Animator 中的 Clip 名保持一致")]  
        public string animationClipName; // 动画剪辑名称，用于匹配

        [Tooltip("对应的音效文件")]  
        public AudioClip soundEffect; // 与该动画剪辑对应播放的音效
    }

    [Tooltip("动画剪辑与音效的映射数组")]  
    public AnimationSoundPair[] animationSoundPairs; // 存储多个动画与音效的对应关系


    [Header("拖拽持续音效设置")]
    [Tooltip("拖拽时播放的持续音效")]  
    public AudioClip dragSoundEffect; // 在拖线或挣扎状态下播放的持续性音效

    [Tooltip("播放拖拽音效的 AudioSource")]  
    public AudioSource dragAudioSource; // 用于播放 dragSoundEffect 的 AudioSource

    [Tooltip("检测拖拽状态的脚本引用")]  
    public FishDragLine fishDragLine; // 引用 FishDragLine 脚本，用于判断是否正在拖拽或挣扎


    // 用于记录上一次播放音效时对应的动画剪辑名称，避免重复触发
    private string currentClipName = "";

    void Update()
    {
        // —— 动画触发一次性音效 ——

        // 获取 Animator 在主动画层(0)当前播放的所有剪辑信息
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        if (clipInfo.Length > 0)
        {
            // 取第一个剪辑的名称
            string newClipName = clipInfo[0].clip.name;

            // 如果与上次不同，则说明动画已切换，触发音效
            if (currentClipName != newClipName)
            {
                currentClipName = newClipName; // 更新记录

                // 遍历所有映射信息，查找匹配的动画剪辑
                foreach (AnimationSoundPair pair in animationSoundPairs)
                {
                    if (pair.animationClipName == newClipName)
                    {
                        // 找到匹配后，将对应音效赋给 audioSource 并播放
                        audioSource.clip = pair.soundEffect;
                        audioSource.Play();
                        break; // 播放后跳出循环
                    }
                }
            }
        }

        // —— 拖拽或挣扎时播放持续音效 ——

        // 确保所有引用都不为 null
        if (fishDragLine != null && dragSoundEffect != null && dragAudioSource != null)
        {
            // 当处于拖拽或挣扎状态
            if (fishDragLine.isDragging || fishDragLine.isStruggling)
            {
                // 如果音频尚未播放，则开始播放持续音效
                if (!dragAudioSource.isPlaying)
                {
                    dragAudioSource.clip = dragSoundEffect;
                    dragAudioSource.Play();
                }
            }
            else
            {
                // 当不再拖拽/挣扎，且音效还在播放，则停止播放
                if (dragAudioSource.isPlaying)
                {
                    dragAudioSource.Stop();
                }
            }
        }
    }
}