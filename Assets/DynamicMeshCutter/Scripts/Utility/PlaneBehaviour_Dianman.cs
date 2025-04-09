using System.Collections;
using DG.Tweening;
using DialogueEditor;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DynamicMeshCutter
{
    public class PlaneBehaviour_Dianman : CutterBehaviour
    {
        public float DebugPlaneLength = 2;

        public MeshTarget[] targetsToCut;
        public KnifeController KnifeController;

        public Vector3 targetScale = new Vector3(1.5f, 1.5f, 0.5f);
        public GameObject Bucket;
        public GameObject BloodEffectPrefab;
        private Transform bloodTransform;
        public NPCConversation Conversation;
        public Image targetImage; // 在Inspector中拖入你的Image组件
        public TextMeshProUGUI Dialogue;
        public void Start()
        {
            
        }
        public void Cut()
        {
            if (targetsToCut != null && targetsToCut.Length > 0)
            {
                foreach (var target in targetsToCut)
                {

                    Cut(target, transform.position, transform.forward, null, OnCreated);
                    Debug.Log("!!!!!!!");
                    Debug.Log($"Cutting target: {target.gameObject.name}");
                    StartCoroutine(FadeIn());


                }
            }
            else
            {
                Debug.LogWarning("没有指定要被切割的目标对象！");
            }
        }


        void OnCreated(Info info, MeshCreationData cData)
        {
            // 指定位置和旋转
            Vector3 position = new Vector3(-0.05f, 0.55f, 0.05f);
            Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);

            // 实例化预制体
            GameObject instance = Instantiate(BloodEffectPrefab, position, rotation);

            // 手动设置缩放
            instance.transform.localScale = new Vector3(0.05f, 0.05f, 1f); 
            bloodTransform = instance.transform;
            MeshCreation.TranslateCreatedObjects(info, cData.CreatedObjects, cData.CreatedTargets, Separation);
            // 启动协程，放大血迹效果
            StartCoroutine(ScaleBloodEffect());
            foreach (var target in cData.CreatedTargets)
            {
                if (target != null)
                {
                    // 添加 Grabbable 脚本
                    target.gameObject.AddComponent<Grabbable>();
                     
                }
            }
            Bucket.SetActive(true);
        }
        IEnumerator ScaleBloodEffect()
        {

            float duration = 3f; // 放大持续时间
            float elapsedTime = 0f;
            Vector3 initialScale = bloodTransform.localScale; // 初始大小为 0
                                                                  // 目标大小，根据需要调整

            // 将血迹效果的初始大小设置为 0
            bloodTransform.localScale = initialScale;

            // 放大血迹效果
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                bloodTransform.localScale = Vector3.Lerp(initialScale, targetScale, t);
                yield return null;
            }
            KnifeController.deleteTHeknife();
        }
        IEnumerator FadeIn()
        {
            yield return targetImage.DOFade(1f, 2)
           .SetEase(Ease.Linear) // 线性渐变
           .WaitForCompletion(); // 等待动画完成

            // 动画完成后执行
            PlayText();
        }

        private void PlayText()
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(Dialogue.DOFade(1f, 1f).SetEase(Ease.Linear)); // 1秒渐变
            seq.AppendInterval(3f); // 等待3秒
            seq.OnComplete(() => SceneManager.LoadScene("LureScene"));
            seq.Play();

        }
    }
}
