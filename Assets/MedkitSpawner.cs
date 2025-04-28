using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MedkitSpawner : MonoBehaviour
{
    public GameObject medkit;

    public void SpawnMedkits(Vector3 position)
    {
        position.x += Random.Range(-3f, 3f);
        position.z += Random.Range(-3f, 3f);
        position.y += 1f;
        Instantiate(medkit, position, Quaternion.identity);

    }
}
