using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int health = 3;
    private SpriteRenderer spriteRenderer;
    private float minX, maxX;
    public Transform hook;  
    public LineRenderer lineRenderer;  
    private Vector3 hookStartPosition;  
    public bool isHooking = false;  
    private float hookSpeed = 50f;  
    private Rigidbody2D boatRigidbody;
    public int HookDirection = -1;
    private bool isControlEnabled = true;
    public UnityEvent onShipDestroyed; 
    public bool isAlive = true;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public int score = 0;
    public int highScore = 0;
    public GameObject gameOverPanel;
    public GameOverUI gameOverUI;




    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        hookStartPosition = hook.position;
        lineRenderer.positionCount = 2;

        boatRigidbody = GetComponent<Rigidbody2D>();
        

    }

    private void Update()
    {
        if (!isAlive) 
            return;

        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isHooking)
            {
                HookDirection = HookDirection * -1;
                
                isHooking = true;
                boatRigidbody.velocity = Vector2.zero;
                hookSpeed = 5f; 
            }
            else
            {
                
                isHooking = false;
                hookSpeed = 25f; 
            }
        }
    }


    private void FixedUpdate()
    {
        if (!isAlive) 
            return;

        if (isHooking)
        {
            
            hook.transform.Translate(Vector3.down * hookSpeed * Time.fixedDeltaTime);

            
            if (hook.position.y >= hookStartPosition.y)
            {
                isHooking = false;
                hookSpeed = 2f; 
            }
        }
        else
        {
            
            if (hook.position.y <= 8)
            {
                hook.transform.Translate(Vector3.down * hookSpeed * Time.fixedDeltaTime * -1 * 2);
            }
            if (hook.position.y > 8)
            {
                hook.position = Vector3.Lerp(hook.position, hookStartPosition, Time.fixedDeltaTime * hookSpeed);
            }
        }

        if (!isHooking)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 newPosition = transform.position + Vector3.right * horizontalInput * moveSpeed * Time.fixedDeltaTime;

            float clampedX = Mathf.Clamp(newPosition.x, minX, maxX);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

            
            hookStartPosition = hook.position;
        }

        
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, hook.position);

        
        if (Physics2D.OverlapCircle(hook.position, 1f, LayerMask.GetMask("Rocks")))
        {
            
            isHooking = false;
            hookSpeed = 2f; 
            if (hook.position.y <= 8)
            {
                hook.transform.Translate(Vector3.down * hookSpeed * Time.fixedDeltaTime * -1 * 2);
            }
            if (hook.position.y > 8)
            {
                hook.position = Vector3.Lerp(hook.position, hookStartPosition, Time.fixedDeltaTime * hookSpeed);
            }
        }
        // if (Physics2D.OverlapCircle(hook.position, 1f, LayerMask.GetMask("Fish")))
        //{
        // isHooking = false;
        //hookSpeed = 2f;
        // if (hook.position.y <= 8)
        // {
        //   hook.transform.Translate(Vector3.down * hookSpeed * Time.fixedDeltaTime * -1 * 2);
        // }
        // if (hook.position.y > 8)
        // {
        //    hook.position = Vector3.Lerp(hook.position, hookStartPosition, Time.fixedDeltaTime * hookSpeed);
        // }
        //}
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {

            transform.Rotate(Vector3.forward, 180f);
            isAlive = false;
            
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("HIGH_SCORE", highScore);
                PlayerPrefs.Save();
                UpdateHighScoreText();
            }
            gameOverPanel.SetActive(true);

        }
        UpdateHealthText();
    }
    public void UpdateHealthText()
    {
        if (healthText != null)
        {

            healthText.text = "HEALTH:" + health.ToString();
        }
    }
    public void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "CURRENT SCORE: " + score.ToString();
        }
    }
    public void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "HIGHT SCORE: " + highScore.ToString();
        }
    }
}













