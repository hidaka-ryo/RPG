using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody m_rigidbody;
    [SerializeField] float m_movingSpeed = 5f;
    [SerializeField] float m_isGroundedLength = 1f;
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        m_rigidbody.velocity = new Vector3(v * m_movingSpeed, m_rigidbody.velocity.y, h * m_movingSpeed);
    }

    bool IsGround()
    {
        Vector3 start = this.transform.position;
        Vector3 end = start + Vector3.down * m_isGroundedLength;
        Debug.DrawLine(start, end);
        bool isGrounded = Physics.Linecast(start, end);
        return isGrounded;
    }
}
