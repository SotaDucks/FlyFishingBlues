using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LureFishingController : MonoBehaviour
{
    private Animator animator;
    private bool isControlling = false;
    private bool isCharging = false;

    [Header("Casting Settings")]
    public float maxChargeTime = 2f; // 最大蓄力时间
    private float currentChargeTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 更新PressingSpace参数
        animator.SetBool("PressingSpace", Input.GetKey(KeyCode.Space));

        // 只有在按住空格键时才能操作
        if (Input.GetKey(KeyCode.Space))
        {
            isControlling = true;

            // 蓄力
            if (Input.GetKey(KeyCode.A))
            {
                if (!isCharging)
                {
                    StartCharging();
                }
                else
                {
                    ContinueCharging();
                }
            }
            // 释放蓄力，执行抛投
            else if (Input.GetKeyDown(KeyCode.D) && isCharging)
            {
                ExecuteCast();
            }
            else if (Input.GetKeyUp(KeyCode.A) && isCharging)
            {
                ResetCharging();
            }
        }
        else
        {
            isControlling = false;
            if (isCharging)
            {
                ResetCharging();
            }
        }
    }

    private void StartCharging()
    {
        isCharging = true;
        currentChargeTime = 0f;
        // 设置蓄力状态
        animator.SetBool("SideCastCharge", true);
    }

    private void ContinueCharging()
    {
        if (currentChargeTime < maxChargeTime)
        {
            currentChargeTime += Time.deltaTime;
            // 更新动画播放速度以反映蓄力进度
            float chargeProgress = currentChargeTime / maxChargeTime;
            animator.SetFloat("ChargeProgress", chargeProgress);
        }
    }

    private void ExecuteCast()
    {
        float chargePercentage = currentChargeTime / maxChargeTime;
        // 设置动画参数，控制抛投力度
        animator.SetFloat("CastPower", chargePercentage);
        
        // 播放侧抛动画
        animator.SetTrigger("SideCastRelease");

        ResetCharging();
    }

    private void ResetCharging()
    {
        isCharging = false;
        currentChargeTime = 0f;
        animator.SetBool("SideCastCharge", false);
        animator.SetFloat("ChargeProgress", 0f);
        animator.SetFloat("CastPower", 0f);
    }
} 