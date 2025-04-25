using UnityEngine;

public class medkit : MonoBehaviour
{
    public int heal=20;
    
//  private void OnControllerColliderHit(ControllerColliderHit other)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().TakeDamage(-heal);
            Destroy(gameObject);
            
        }

    }
   
}
