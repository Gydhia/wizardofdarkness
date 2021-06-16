using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupPreset : MonoBehaviour
{
    List<EnemyEntity> DistanceEnemies;
    List<EnemyEntity> MeleeEnemies;

    private void Start()
    {
        EnemyEntity[] entities = this.GetComponentsInChildren<EnemyEntity>();

        foreach(EnemyEntity entity in entities)
        {
            if (entity.Type == EnemyType.Distance)
                DistanceEnemies.Add(entity);
            if(entity.Type == EnemyType.Melee)
                MeleeEnemies.Add(entity);
        }
    }

    public void SpawnEnemies(Transform parent)
    {
        foreach (EnemyEntity entity in DistanceEnemies) {
            Instantiate(entity.EnemyPrefab, parent);
        }
        foreach (EnemyEntity entity in MeleeEnemies) {
            Instantiate(entity.EnemyPrefab, parent);
        }
    }
}
