using UnityEngine;

public class DesertBall : MonoBehaviour
{
    public Transform[] desertBalls;
    public float rotateSpeed;
    public float minMoveSpeed,maxMoveSpeed;
    private float moveSpeed;
    private Vector2 moveDirection = Vector2.right;
    public void Initialize()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        Destroy(gameObject, 7);
    }
    private void FixedUpdate()
    {
        foreach (Transform t in desertBalls)
        {
            t.Rotate(-Vector3.forward * rotateSpeed * Time.fixedDeltaTime);
        }
        transform.Translate(moveDirection*moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMovement playerMovement))
        {
            Debug.Log("tesss");
            if (playerMovement.IsGrounded()) GameEvents.OnGameLose?.Invoke();
        }
    }
}
