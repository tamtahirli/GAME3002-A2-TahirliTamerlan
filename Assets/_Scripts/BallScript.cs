using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public static BallScript instance;

    [SerializeField]
    private Vector3 m_vForce = Vector3.zero;
    [SerializeField]
    private float m_fPushForce = 3f;
    [SerializeField]
    private GameObject m_gCloseCollider; // Collider that closes off the entrypoint

    [HideInInspector]
    public Rigidbody m_rBody;

    private Vector3 m_vStartPos = Vector3.zero;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        m_rBody = GetComponent<Rigidbody>();
        m_vStartPos = transform.position;
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
    }
}
