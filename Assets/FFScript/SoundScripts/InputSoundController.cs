using UnityEngine;

/// <summary>
/// 管理拖拽持续音效以及根据 Animator 指定图层播放动画触发音效。
/// </summary>
public class InputSoundController : MonoBehaviour
{
    [Header("拖拽持续音效设置")]
    [Tooltip("在拖拽或挣扎时播放的持续性音效")]  
    public AudioClip dragSoundEffect;

    [Tooltip("用于播放拖拽音效的 AudioSource")]  
    public AudioSource dragAudioSource;

    [Tooltip("检测拖拽状态的脚本引用")]  
    public FishDragLine fishDragLine;

    [Header("动画音效设置")]
    [Tooltip("用于播放动画触发音效的 AudioSource")]
    public AudioSource animationAudioSource;

    [Tooltip("Animator 中应检查的图层索引，用于获取动画剪辑信息")]  
    public int animationLayerIndex = 0;

    [Tooltip("动画剪辑与一次性音效的映射，长度为 5")]
    public AnimationSoundPair[] animationSoundPairs = new AnimationSoundPair[5];

    [System.Serializable]
    public class AnimationSoundPair
    {
        [Tooltip("动画剪辑名称，需与 Animator Clip 名称一致")]  
        public string animationClipName;

        [Tooltip("对应播放的音效")]
        public AudioClip soundEffect;
    }

    [Tooltip("Animator 组件引用，用于获取当前动画剪辑信息")]  
    public Animator animator;

    // 记录上一次播放时的剪辑名称，避免重复触发
    private string currentAnimationClip = string.Empty;

    void Update()
    {
        // —— 拖拽或挣扎状态下的持续音效 ——
        if (fishDragLine != null && dragSoundEffect != null && dragAudioSource != null)
        {
            if (fishDragLine.isDragging || fishDragLine.isStruggling)
            {
                if (!dragAudioSource.isPlaying)
                {
                    dragAudioSource.clip = dragSoundEffect;
                    dragAudioSource.Play();
                }
            }
            else if (dragAudioSource.isPlaying)
            {
                dragAudioSource.Stop();
            }
        }

        // —— 动画触发一次性音效 ——
        if (animator != null && animationAudioSource != null)
        {
            var clips = animator.GetCurrentAnimatorClipInfo(animationLayerIndex);
            if (clips.Length > 0)
            {
                string newClip = clips[0].clip.name;
                if (newClip != currentAnimationClip)
                {
                    currentAnimationClip = newClip;
                    foreach (var pair in animationSoundPairs)
                    {
                        if (pair.animationClipName == newClip && pair.soundEffect != null)
                        {
                            animationAudioSource.PlayOneShot(pair.soundEffect);
                            break;
                        }
                    }
                }
            }
        }
    }
}