using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;

    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    float threshold = 0.2f;

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && rigid2D.velocity.y == 0) // 화면을 탭하고 위로 점프 중이 아니라면
        {
            animator.SetTrigger("JumpTrigger");                     // Jump 애니메이션 실행
            rigid2D.AddForce(transform.up * jumpForce);             // 위로 jumpForce만큼 힘을 가한다.
        }

        // 좌우 이동
        int key = 0;
        if (Input.acceleration.x > threshold) key = 1;              // 가속도 센서가 0.2보다 크면 오른쪽으로
        if (Input.acceleration.x < -threshold) key = -1;            // 가속도 센서가 -0.2보다 작으면 왼쪽으로 이동

        // 플레이어의 속도
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        // 스피드 제한
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        // 움직이는 방향에 따라 반전
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // 플레이어 속도에 따라 애니메이션 속도 변경
        if (rigid2D.velocity.y == 0)
        {
            animator.speed = speedx / 2.0f;        
        }
        else
        {
            animator.speed = 1.0f;
        }

        // 플레이어가 화면 밖으로 나가면 처음부터
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) // 골 도착
    {
        Debug.Log("골");
        SceneManager.LoadScene("ClearScene");
    }
}
