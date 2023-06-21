using UnityEngine;

namespace Core.Character.Enemy
{
    public class EnemyDeleteOutsideCameraView : MonoBehaviour
    {
        [SerializeField] GameObject enemyController;
        private float checkTimer = 1f;
        private EnemyInstanceStatus enemyInstance;

        private void Start()
        {
            enemyInstance = GameObject.Find("EnemyManager").GetComponent<EnemyInstanceStatus>();
            InvokeRepeating(nameof(CheckCameraView), checkTimer, checkTimer);
        }

        private void CheckCameraView()
        {
            if (IsInCameraView())
            {
                Destroy(gameObject);
                enemyInstance.InstanceCounter--;
            }
        }

        private bool IsInCameraView()
        {
            Vector3 viewPosition = Camera.main.WorldToViewportPoint(transform.position);

            if (viewPosition.y >= 1f) return true;
            if (viewPosition.y <= -1f) return true;
            if (viewPosition.x >= 1f) return true;
            if (viewPosition.x <= -1f) return true;
            return false;
        }
    }
}
