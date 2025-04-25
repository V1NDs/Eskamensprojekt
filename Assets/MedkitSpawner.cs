using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MedkitSpawner : MonoBehaviour
{
    public GameObject medkit;

    public void SpawnMedkits(Vector3 position)
    {
        position.x += 2f;
        position.z += 2f;
        Instantiate(medkit, position, Quaternion.identity);

    }
}
