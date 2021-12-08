using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 m_tPos;
    Rigidbody m_rigidbody;
    Animator m_animator;
    private Collider m_swordCollider;
    [SerializeField] float m_movingSpeed = 5f;
    [SerializeField] float m_isGroundedLength = 1f;
    [SerializeField] float m_jumpPower = 5f;
    [SerializeField] float m_rotion = 10;
    [SerializeField] float m_stepAngle;
    [SerializeField] float m_speedRate;
    [SerializeField] Collider m_attackCollider;
    [SerializeField] int m_maxHp = 20;
    [SerializeField] int m_currentHp;
    [SerializeField] private Item item;
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        m_currentHp = m_maxHp;
        m_swordCollider = GameObject.Find("Sword14_Blue").GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        m_rigidbody.velocity = new Vector3(h * m_movingSpeed, m_rigidbody.velocity.y, v * m_movingSpeed);

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

        m_rigidbody.velocity = new Vector3(h * m_movingSpeed, m_rigidbody.velocity.y, v * m_movingSpeed);

        if (Input.GetButtonDown("Jump") && IsGround())
        {
            m_rigidbody.AddForce(Vector3.up * m_jumpPower, ForceMode.Impulse);
            m_animator.SetTrigger("Jump");
        }

        if (Input.GetMouseButtonDown(0))
        {
            m_animator.SetBool("Attack", true);
            m_swordCollider.enabled = true;
            Invoke("ColliderReset", 0.3f);
        }

        if (Input.GetKeyDown(KeyCode.R)) Step();
    }
    void Attack1Start()
    {
        m_attackCollider.enabled = true;
    }

    void Attack1End()
    {
        m_attackCollider.enabled = false;
        m_animator.SetBool("Attack", false);
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
            transform.LookAt(m_tPos);

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

    private void ColliderReset()
    {
        m_swordCollider.enabled = false;
    }
}
