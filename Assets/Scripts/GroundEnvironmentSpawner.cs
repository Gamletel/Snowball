using UnityEngine;

public class GroundEnvironmentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _environments;
    [SerializeField] private Transform[] _spawnPoints;

    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < _spawnPoints.Length-1; i++)
        {
            int y = Random.Range(0, _environments.Length * 2);
            try
            {
                Instantiate(_environments[y], _spawnPoints[i]);
                Debug.Log($"Объект #{y} заспавнен в точке #{i}");
            }
            catch (System.Exception)
            {
                continue;
            }
            
        }
    }
}
