using UnityEngine;

public class player : MonoBehaviour
{
    enum MovingDirection
    {
        Idle, Up, Down
    }

    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float pushForce = 1;

    public GameObject Ceiling;
    public GameObject Floor;

    private MovingDirection movingDirection;
    private Rigidbody2D rb2D;

    private bool isPaused = false;
    private Vector2 velocityBeforePause;

    private void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        movingDirection = MovingDirection.Idle;
    }

    void Start()
    {
        MoveUp();
    }

    void Update()
    {
        Vector2 movementVector2D = Vector2.zero;
        movementVector2D.x = Input.GetAxisRaw("Horizontal");
        movementVector2D.y = Input.GetAxisRaw("Vertical");

        if (movementVector2D != Vector2.zero)
        {
            if (movementVector2D == Vector2.left)
            {
                rb2D.AddForce(Vector2.left * pushForce);
            }
            if (movementVector2D == Vector2.right)
            {
                rb2D.AddForce(Vector2.right * pushForce);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space) == true)
        {
            if (isPaused)
            {
                rb2D.AddForce(velocityBeforePause * moveSpeed);
                isPaused = false;
            }
            else
            {
                velocityBeforePause = rb2D.velocity;
                rb2D.velocity = Vector2.zero;
                isPaused = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetInstanceID() == Ceiling.GetInstanceID())
        {
            MoveDown();
        }
        else if (collision.gameObject.GetInstanceID() == Floor.GetInstanceID())
        {
            MoveUp();
        }
    }

    private void MoveDown()
    {
        rb2D.AddForce(Vector2.down * moveSpeed);
        movingDirection = MovingDirection.Down;
    }

    private void MoveUp()
    {
        rb2D.AddForce(Vector2.up * moveSpeed);
        movingDirection = MovingDirection.Up;
    }
}
