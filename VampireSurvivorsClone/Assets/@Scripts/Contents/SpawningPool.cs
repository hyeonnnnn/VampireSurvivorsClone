using System.Collections;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    float _spawnInterval = 2.0f;
    int _maxMonsterCount = 100;
    Coroutine _CoUpdateSpawnPool;

    void Start()
    {
        _CoUpdateSpawnPool = StartCoroutine(CoUpdateSpawnPool());
    }

    IEnumerator CoUpdateSpawnPool()
    {
        while(true)
        {
            TrySpawn();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void TrySpawn()
    {
        int monsterCount = Managers.Object.Monsters.Count;
        if (monsterCount >= _maxMonsterCount)
            return;

        MonsterController mc = Managers.Object.Spawn<MonsterController>(Random.Range(0, 2));
        mc.transform.position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
    }
}
