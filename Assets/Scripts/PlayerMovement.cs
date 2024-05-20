using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public Vector3 velocity = Vector3.zero;
    public SpriteRenderer characterShadow;
    public float shadowSpeed = 1f;
    public float jumpDuration = 0.3f;



    private float inputX,inputY;
    private bool isGrounded = true;
    private bool jumpLeft = false;
    private bool jumpRight = false;
    private float moveAnimationTimer;
    
    public Animator animator;
    public SpriteRenderer sp;
    public Sprite shadow1, shadow2;
    public ParticleSystem footParticle;
    public AudioSource footSource;

    private RopeThrow ropeThrowScript;

    private bool canMove = true;


    private void Awake()
    {
        ropeThrowScript = GetComponent<RopeThrow>();
    }

    private void Start()
    {
        GameEvents.OnGameLose.AddListener(GameLoseEvent);
    }
    private void OnDestroy()
    {
        GameEvents.OnGameLose.RemoveListener(GameLoseEvent);

    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
    private void FixedUpdate()
    {
        UpdateVelocity();
        velocity = new Vector3(inputX, inputY).normalized * moveSpeed * Time.fixedDeltaTime;

        
    }


    private void UpdateVelocity()
    {
        if (!canMove) return;
        if (ropeThrowScript.Action()) return;

        transform.position += velocity;

        
        if (velocity.magnitude > 0 && velocity.y==0) // move1 animation
        {
            moveAnimationTimer += Time.deltaTime;
            if(moveAnimationTimer > 2)
            {
                moveAnimationTimer = 0;
                animator.Play("Move1");
            }
        }
        else if(velocity.magnitude>0 && velocity.x == 0)  // move2 animation
        {
            moveAnimationTimer += Time.deltaTime;
            if (moveAnimationTimer > 2)
            {
                moveAnimationTimer = 0;
                animator.Play("Move2");
            }
        }

        sp.flipX = velocity.x < 0 ? true : false;

    }

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");


        jumpLeft = inputX < 0;
        jumpRight = inputX > 0;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !ropeThrowScript.Action()
            && canMove)
        {
            Jump();
        }

        ApplyFootAudio();


    }

    private void ApplyFootAudio()
    {
        if (inputX != 0 || inputY != 0)
        {
            if (isGrounded)
            {
                footSource.pitch = Random.Range(0.95f, 1.05f);
                if (!footSource.isPlaying) footSource.Play();
            }
            else
            {
                footSource.Pause();
            }

        }
        else
        {
            footSource.Pause();
        }
    }

    private void Jump()
    {
        GameEvents.OnPlayerJumpEvent?.Invoke();

        isGrounded = false;
        characterShadow.sprite = shadow2;
        if (!jumpLeft && !jumpRight)
        {
            animator.Play("NormalJump");
        }
        if (jumpLeft || jumpRight)
        {
            animator.Play("Jump");
        }

        footParticle.Stop();
        sp.transform.DOLocalMoveY(1f, jumpDuration).OnComplete(delegate
        {
            sp.transform.DOLocalMoveY(0, jumpDuration).OnComplete(delegate
            {
                footParticle.Play();
                animator.Play("Idle");
                characterShadow.sprite = shadow1;
                isGrounded = true;
            });
        });

    }
    private void GameLoseEvent()
    {
        canMove = false;
        sp.transform.SetParent(null);
        gameObject.SetActive(false);
        animator.Play("Die");

    }

}
