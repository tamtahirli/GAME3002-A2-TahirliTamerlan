using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public static BallScript instance;

    [SerializeField]
    private TMP_Text ScoreText;
    [SerializeField]
    private TMP_Text HighScoreText;

    [SerializeField]
    private Vector3 m_vForce = Vector3.zero;
    [SerializeField]
    private float m_fPushForce = 3f;
    [SerializeField]
    private GameObject m_gCloseCollider; // Collider that closes off the entrypoint

    [HideInInspector]
    public Rigidbody m_rBody;

    private Vector3 m_vStartPos = Vector3.zero;

    private int m_iScore = 0, m_iHighScore = 0;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        m_rBody = GetComponent<Rigidbody>();
        m_vStartPos = transform.position;
        m_iHighScore = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0;
        UpdateHighScore(m_iHighScore);
        ResetScore();
    }

    void Update()
    {
        if(transform.position.x < 1.3f)
        {
            m_gCloseCollider.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            m_rBody.AddForce(m_vForce * m_fPushForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    public void Reset()
    {
        m_rBody.velocity = Vector3.zero;
        transform.position = m_vStartPos;
        m_gCloseCollider.SetActive(false);

        if (m_iScore > m_iHighScore)
        {
            UpdateHighScore(m_iScore);
            m_iHighScore = m_iScore;
        }
        ResetScore();
    }

    public void AddScore(int amount)
    {
        m_iScore += amount;
        ScoreText.text = "Score:\n" + m_iScore;
    }

    public void UpdateHighScore(int amount)
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            if(amount > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", amount);
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", amount);
        }

        HighScoreText.text = "High score: " + amount;
    }

    public void ResetScore()
    {
        m_iScore = 0;
        ScoreText.text = "Score:\n0";
    }
}
