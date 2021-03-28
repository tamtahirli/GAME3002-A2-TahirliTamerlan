using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour
{
    [SerializeField]
    private float m_fZPosMin = 0, m_fDamping = 0;

    private Rigidbody m_rBody = null;

    private float m_fMass = 0, m_fSpring = 0, m_fZPosMax = 0;

    private Vector3 m_vForce = Vector3.zero, m_vPrevVel = Vector3.zero, m_vStartPos = Vector3.zero;

    private void Start()
    {
        m_rBody = GetComponent<Rigidbody>();
        m_fMass = m_rBody.mass;
        m_vStartPos = transform.position;
        m_fZPosMax = transform.position.z;
    }

    bool m_oPlunger = false;
    float m_fStartMouseZ = 0, m_fMouseDist = 0;
    private void Update()
    {
        if(transform.position.z >= m_fZPosMax - 0.02f) // Check if above clamped position.
        {
            m_rBody.velocity = Vector3.zero; // Set velocity to zero to avoid ball pushing plunger back.
            transform.position = m_vStartPos; // Put to start pos.
        }

        // Plunger movement logic. If pressed on plunger's knob, then player can pull it back.
        if(Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(mouseRay, out RaycastHit hit))
            {
                if(hit.collider.CompareTag("Plunger"))
                {
                    m_oPlunger = true;
                    m_fStartMouseZ = Input.mousePosition.y;
                }
            }
        }

        if (!m_oPlunger) return; // Dont do any mouse checks if plunger wasnt pressed.

        if(Input.GetMouseButton(0))
        {
            // Clamped movement of plunger.
            Vector3 pos = transform.position;
            m_fMouseDist = Mathf.Clamp01((m_fStartMouseZ - Input.mousePosition.y) / 50); // 0-1 value of mouse pull back, max 50
            pos.z = Mathf.Clamp(m_vStartPos.z - m_fMouseDist, m_fZPosMin, m_fZPosMax); // Subtract on the Z to pull it back
            transform.position = pos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_fSpring = CalculateSpringConstant(); // Calculate constant on release.
            m_oPlunger = false;
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.z < m_fZPosMax - 0.02f && !m_oPlunger) // Check if below rest position & released plunger
        {
            UpdateSpringForce(m_fMouseDist); // Update spring force if so.
        }
    }

    private float CalculateSpringConstant()
    {
        float fDistance = (m_vStartPos - transform.position).magnitude;

        if (fDistance <= 0f)
        {
            return Mathf.Epsilon;
        }

        return (m_fMass * Physics.gravity.y) / (fDistance);
    }

    private void UpdateSpringForce(float scalar)
    {
        Debug.Log("Scalar: " + scalar);
        float damping = m_fDamping * scalar; // Scalar to damping, depending on how much it was pulled back.

        m_vForce = -m_fSpring * (m_vStartPos - transform.position) -
            damping * (m_rBody.velocity - m_vPrevVel);

        m_rBody.AddForce(m_vForce, ForceMode.Acceleration);

        m_vPrevVel = m_rBody.velocity;
    }
}
