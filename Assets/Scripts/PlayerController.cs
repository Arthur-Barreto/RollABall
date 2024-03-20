using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject gameOverTextObject;
    
    public TextMeshProUGUI timeText;
    
    private Rigidbody _rb;
    private int _count;
    private bool _isTen;
    private float _movementX;
    private float _movementY;
    private bool _isGameOver;
    private float _timeRemaining = 137;
    private bool _timerIsRunning;
    private float _speed = 10;

    private AudioManager _audioManager;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _count = 0;
        _isTen = false;
        
        SetCountText();
        winTextObject.SetActive(false);
        gameOverTextObject.SetActive(false);

        _timerIsRunning = true;
    }

    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnMove(InputValue movementValue)
    {
        var movementVector = movementValue.Get<Vector2>();
        _movementX = movementVector.x;
        _movementY = movementVector.y;
    }

    private void SetCountText()
    {
        countText.text = "Count: " + _count;
        if (_count < 10 || !_isTen || _isGameOver) return;
        Win();
    }

    private void Update()
    {
        if (!_timerIsRunning) return;
        if (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;
            DisplayTime(_timeRemaining);
        }
        else
        {
            GameOver();
        }
    }

    private void FixedUpdate()
    {
        var movement = new Vector3(_movementX, 0.0f, _movementY);
        if (_isGameOver || (_isTen)) return;
        _rb.AddForce(movement * _speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            _count += 1;
            SetCountText();
        }

        if (!other.gameObject.CompareTag("Portal") || _count < 10) return;
        _isTen = true;
        SetCountText();
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = "T- " + $"{minutes:00}:{seconds:00}";
    }

    private void Win()
    {
        winTextObject.SetActive(true);
        _timerIsRunning = false;
        
        PlayWinMusic();

        _speed = 0;
        
    }

    private void GameOver()
    {
        gameOverTextObject.SetActive(true);
        _timeRemaining = 0;
        _timerIsRunning = false;
        _isGameOver = true;
        
        PlayGameOverMusic();
    }

    private void PlayWinMusic()
    {
        
        _audioManager.PlaySfx(_audioManager.victory);
    }

    private void PlayGameOverMusic()
    {
        _audioManager.PlaySfx(_audioManager.gameOver);
    }
}