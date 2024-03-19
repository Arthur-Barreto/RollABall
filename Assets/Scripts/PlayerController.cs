using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject gameOverTextObject;
    // timer variables
    public float timeRemaining = 149;
    public bool timerIsRunning;
    public TextMeshProUGUI timeText;
    
    private Rigidbody _rb;
    private int _count;
    private bool _isTen;
    private float _movementX;
    private float _movementY;
    private bool _isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _count = 0;
        _isTen = false;
        
        SetCountText();
        winTextObject.SetActive(false);
        gameOverTextObject.SetActive(false);

        timerIsRunning = true;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _movementX = movementVector.x;
        _movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + _count.ToString();
        if (_count >= 10 && _isTen && !_isGameOver)
        {
            winTextObject.SetActive(true);
            timerIsRunning = false;
        }
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                gameOverTextObject.SetActive(true);
                timeRemaining = 0;
                timerIsRunning = false;
                _isGameOver = true;
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(_movementX, 0.0f, _movementY);
        _rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            _count += 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Portal") && _count >=10)
        {
            _isTen = true;
            SetCountText();
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = "T- " + $"{minutes:00}:{seconds:00}";
    }
}
