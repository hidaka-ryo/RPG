using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 m_tPos;
    Rigidbody m_rigidbody;
    Animator m_animator;
    [SerializeField] float m_movingSpeed = 5f;
    [SerializeField] float m_isGroundedLength = 1f;
    [SerializeField] float m_jumpPower = 5f;
    [SerializeField] float m_rotion = 10;
    [SerializeField] float m_stepAngle;
    [SerializeField] float m_speedRate;
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        m_rigidbody.velocity = new Vector3(h * m_movingSpeed, m_rigidbody.velocity.y, v * m_movingSpeed );

        if (v != 0 || h != 0)
        {
            var dir = new Vector3(h, 0, v);
            Quaternion targetlot = Quaternion.LookRotation(dir);

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetlot, m_rotion * Time.deltaTime);
            m_animator.SetBool("Run", true);
        }
        else
        {
            m_animator.SetBool("Run", false);
        }

        if (Input.GetButtonDown("Jump") && IsGround())
        {
            m_rigidbody.AddForce(Vector3.up * m_jumpPower, ForceMode.Impulse);
            m_animator.SetTrigger("Jump");
        }

        if (Input.GetMouseButtonDown(1))
        {
            m_animator.SetBool("Attack", true);
        }

        if (Input.GetKeyDown(KeyCode.R)) Step();
        if (Input.GetKeyDown(KeyCode.L)) Step();
    }

    void Step()
    {
        m_tPos = GameObject.FindWithTag("Enemy").transform.position;
        Vector3 mPos = transform.position;

        float diffX = mPos.x - m_tPos.x;
        float diffZ = mPos.z - m_tPos.z;

        float setAngle = Mathf.Atan2(diffZ, diffX) * Mathf.Rad2Deg;
        float dis = Vector3.Distance(m_tPos, mPos);

        StartCoroutine(Move(setAngle, dis));
    }

    IEnumerator Move(float getAngle, float distance)
    {
        float time = 0;
        float rate = 0;

        while (rate < 1)
        {
            time += Time.deltaTime;
            rate = time / m_speedRate;
            float set = Mathf.Lerp(getAngle, getAngle + m_stepAngle, rate);

            // 現在の自身のベクトルを算出
            float rad = set * (Mathf.PI / 180);
            float x = distance * Mathf.Cos(rad);
            float z = distance * Mathf.Sin(rad);

            Vector3 setVec = new Vector3(x + m_tPos.x, transform.position.y, z + m_tPos.z);
            m_rigidbody.MovePosition(setVec);

            yield return null;
        }
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
