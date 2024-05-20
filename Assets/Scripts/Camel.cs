using UnityEngine;

public class Camel : MonoBehaviour
{
    private Vector3 point;
    public float moveSpeed;
    public bool canEat= true;
    private void Awake()
    {
        point = GetRandomPoint();
    }
    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position,point, Time.deltaTime * moveSpeed);
        float distance = Vector2.Distance(transform.position,point);
        if (distance < 0.2)
        {
            point = GetRandomPoint();
        }
        Vector3 direction = (point - transform.position ).normalized;
        var scale = transform.localScale;
        if (direction.x > 0) scale.x = 1;
        if (direction.x < 0) scale.x = -1;
        transform.localScale = scale;
    }
    public Vector3 GetRandomPoint()
    {
        float x = Random.Range(-4, 4);
        float y = Random.Range(-4, 4);
        return new Vector3(x,y);
    }
    public void PlayerCatch()
    {
        canEat = false;
    }
}
