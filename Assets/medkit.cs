using UnityEngine;

public class medkit : MonoBehaviour
{
    public int heal=20;
    
//  private void OnControllerColliderHit(ControllerColliderHit other)
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            other.GetComponent<Health>().TakeDamage(-heal);
            Destroy(gameObject);
        }

    }
   
}
