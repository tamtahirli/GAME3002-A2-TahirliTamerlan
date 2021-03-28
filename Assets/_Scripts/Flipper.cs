using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField]
    private KeyCode m_kButton;
    [SerializeField]
    private float m_fSpring = 3000;
    [SerializeField]
    private float m_fDamping = 0;
    [SerializeField]
    private float m_fTargetPos = 0;

    private float m_fOriginalPos = 0;

    private HingeJoint m_hJoint = null;
    private JointSpring m_jSpring;

    void Start()
    {
        m_hJoint = GetComponent<HingeJoint>();
        m_hJoint.useSpring = true;

        m_jSpring = new JointSpring();
        m_jSpring.spring = m_fSpring;
        m_jSpring.damper = m_fDamping;

        m_hJoint.spring = m_jSpring;

        m_fOriginalPos = m_jSpring.targetPosition;
    }


    void Update()
    {
        if (Input.GetKeyDown(m_kButton))
        {
            m_jSpring.targetPosition = m_fTargetPos;
            m_hJoint.spring = m_jSpring;
        }

        if (Input.GetKeyUp(m_kButton))
        {
            m_jSpring.targetPosition = m_fOriginalPos;
            m_hJoint.spring = m_jSpring;
        }
    }
}
