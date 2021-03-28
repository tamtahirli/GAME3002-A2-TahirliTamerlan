using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool m_bIsActive = true;
    [SerializeField]
    private int m_iScore = 0;
    [SerializeField]
    private float m_fReflectScalar = 2f;
    [SerializeField]
    private float m_fLightDelay = 0.1f;
    [SerializeField]
    private bool m_bIsMoving = false;
    [SerializeField]
    private float m_fMoveAmount = 0;
    [SerializeField]
    private float m_fMovementSpeed = 0;

    private BallScript ball;

    private Light m_lPoint = null;
    private float m_fLastHit = -1;
    private Vector3 m_vStartScale;
    private Vector3 m_vStartPos;
    private bool m_bMovingLeft = false;

    private void Start()
    {
        ball = BallScript.instance;
        m_lPoint = GetComponentInChildren<Light>();
        DisableLight();
        m_vStartScale = transform.localScale;
        m_vStartPos = transform.position;
        if (m_bIsMoving)
        {
            m_vStartPos = transform.position;
            m_bMovingLeft = true;
        }
    }

    float m_fDeltaTime;
    private void Update()
    {
        m_fDeltaTime += Time.deltaTime;
        if(m_bIsMoving)
        {
            if(m_bMovingLeft)
            {
                if(transform.position.x <= m_vStartPos.x - m_fMoveAmount)
                {

                }
            }
            else
            {

            }
        }
        if (m_fLastHit != -1)
        {
            if ((Time.time - m_fLastHit) > m_fLightDelay)
            {
                m_fLastHit = -1;
                DisableLight();
            }
        }
    }

    Vector3 normal = Vector3.zero;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            normal = -collision.contacts[0].normal;
            if (m_bIsActive)
            {
                ball.m_rBody.AddForce(normal * m_fReflectScalar, ForceMode.Impulse); // Add force based on normal and scalar for bounce.
                EnableLight(); // Enable light for effect.
                if(m_iScore > 0)
                {
                    BallScript.instance.AddScore(m_iScore);
                    transform.DOComplete();
                    transform.DOPunchScale(m_vStartScale * 1.02f, m_fLightDelay);
                }
            }
            else
            {
                ball.m_rBody.velocity = ball.m_rBody.velocity * 0.9f; // Really slow it down but not completely.
                //ball.m_rBody.AddForce(normal * 0.1f, ForceMode.Impulse); // Add a tiny bit of force so it doesn't get stuck (playability)
            }
        }
    }

    private void EnableLight()
    {
        if (m_lPoint == null) return;
        m_lPoint.enabled = true;
        m_fLastHit = Time.time;
    }

    private void DisableLight()
    {
        if (m_lPoint == null) return;
        m_lPoint.enabled = false;
    }
}
