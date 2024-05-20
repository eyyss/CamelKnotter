using UnityEngine;

public class Wheat : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Camel camel))
        {
            if (camel.canEat)
            {
                Destroy(gameObject);
                GameEvents.OnWheatDestroyEvent?.Invoke(this,transform.position);
            }
        }
    }
}
