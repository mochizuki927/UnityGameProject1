using Interface.Clients;
using Interface.Implementations;
using Interface.Interfaces;
using UnityEngine;

namespace Core.Character.Enemy.Parasitoid
{
    public class EnemyInstanceParasitoid : MonoBehaviour, IEnemy
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private GameObject rupturePrefab;
        [SerializeField] private int instanceNum;
        [SerializeField] private float spawnInterval;

        private float lastInstaceTime;
        private float spawnOffset = 5f;

        private GameObject[] existingEnemys;
        private EnemyRupture enemyRupture;
        private EnemyInstanceAttribute enemyInstance;
        private IIncrement increment;
        private EnemyInstanceIncrementHandler instanceIncrementHandler;

        private void Start()
        {

            Reference();
            Init();

        }

        private void Update()
        {
            Instantiate();
            Init();
        }

        private void Instantiate()
        {
            if (Time.time - lastInstaceTime <= spawnInterval) return;

            existingEnemys = GameObject.FindGameObjectsWithTag("Enemy");

            if (existingEnemys == null) return;
            if (existingEnemys.Length == 0) return;
            GameObject randomEnemy = existingEnemys[Random.Range(0, existingEnemys.Length)];

            for (int i = 0; i < instanceNum; i++)
            {
                Vector3 spawnPosition = randomEnemy.transform.position + Random.insideUnitSphere * spawnOffset;

                Instantiate(prefab, spawnPosition, Quaternion.identity, enemyInstance.Container);

                CounterIncrement();
            }

            enemyRupture.Rupture(rupturePrefab, randomEnemy, enemyInstance.Container);

            Destroy(randomEnemy);

        }

        public void CounterIncrement()
        {
            instanceIncrementHandler.InstanceIncrement(increment);
        }

        private void Init()
        {

            lastInstaceTime = Time.time;
        }

        public void Reference()
        {
            increment = new EnemyInstanceCounterIncrement();
            instanceIncrementHandler = new EnemyInstanceIncrementHandler();

            enemyInstance = GetComponent<EnemyInstanceAttribute>();

            GameObject container = GameObject.Find("Scripts");
            enemyRupture = container.GetComponentInChildren<EnemyRupture>();
            if (enemyRupture == null)
            {
                enemyRupture = container.AddComponent<EnemyRupture>();
            }
        }
    }
}
