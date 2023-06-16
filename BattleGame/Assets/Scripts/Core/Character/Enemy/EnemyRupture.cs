using UnityEngine;
using System.Collections;
using System.ComponentModel;

namespace Core.Character.Enemy
{
    public class EnemyRupture : MonoBehaviour
    {
        private float gradationTime = 1;

        public void Rupture(GameObject ruptureObj, GameObject baseObj, Transform container)
        {
            Vector3 spornPosition = baseObj.transform.position;

            GameObject obj = Instantiate(ruptureObj, spornPosition, Quaternion.identity, container);
            StartCoroutine(Gradation(obj));
        }

        private void ObjDestroy(GameObject obj)
        {
            Destroy(obj);
        }

        private IEnumerator Gradation(GameObject obj)
        {
            float t = 0f;
            Color startColor = Color.clear;
            Color endColor = Color.white;

            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();

            while (t < 1f)
            {
                t += Time.unscaledDeltaTime / gradationTime;
                spriteRenderer.color = Color.Lerp(startColor, endColor, t);
                yield return null;
            }

            ObjDestroy(obj);
        }
    }
}
