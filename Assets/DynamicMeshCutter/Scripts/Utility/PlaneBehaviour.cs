using System.Collections;
using UnityEngine;

namespace DynamicMeshCutter
{
    public class PlaneBehaviour : CutterBehaviour
    {
        public float DebugPlaneLength = 2;

        public MeshTarget[] targetsToCut;
        public KnifeController KnifeController;

        public Vector3 targetScale = new Vector3(1.5f, 1.5f, 0.5f);
        public GameObject Bucket;
        public GameObject BloodEffectPrefab;
        private Transform bloodTransform;

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

                }
            }
            else
            {
                Debug.LogWarning("û��ָ��Ҫ���и��Ŀ�����");
            }
        }


        void OnCreated(Info info, MeshCreationData cData)
        {
            // ָ��λ�ú���ת
            Vector3 position = new Vector3(-0.05f, 0.55f, 0.05f);
            Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);

            // ʵ����Ԥ����
            GameObject instance = Instantiate(BloodEffectPrefab, position, rotation);

            // �ֶ���������
            instance.transform.localScale = new Vector3(0.05f, 0.05f, 1f); 
            bloodTransform = instance.transform;
            MeshCreation.TranslateCreatedObjects(info, cData.CreatedObjects, cData.CreatedTargets, Separation);
            // ����Э�̣��Ŵ�Ѫ��Ч��
            StartCoroutine(ScaleBloodEffect());
            foreach (var target in cData.CreatedTargets)
            {
                if (target != null)
                {
                    // ��� Grabbable �ű�
                    target.gameObject.AddComponent<Grabbable>();
                     
                }
            }
            Bucket.SetActive(true);
        }
        IEnumerator ScaleBloodEffect()
        {

            float duration = 3f; // �Ŵ����ʱ��
            float elapsedTime = 0f;
            Vector3 initialScale = bloodTransform.localScale; // ��ʼ��СΪ 0
                                                                  // Ŀ���С��������Ҫ����

            // ��Ѫ��Ч���ĳ�ʼ��С����Ϊ 0
            bloodTransform.localScale = initialScale;

            // �Ŵ�Ѫ��Ч��
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                bloodTransform.localScale = Vector3.Lerp(initialScale, targetScale, t);
                yield return null;
            }
            KnifeController.deleteTHeknife();
        }
    }
}
