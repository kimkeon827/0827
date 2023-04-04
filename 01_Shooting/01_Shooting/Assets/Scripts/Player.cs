using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public GameObject explosion;
    public GameObject rocket;
    public bool canShoot = true;
    float shootDelay = 0.5f;
    float shootTimer = 0;

    private void Start()
    {
        
    }

    void MoveControl()
    {
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        transform.Translate(moveX, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) // 충돌체 정보인자를 받는 함수 추가
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //Instantiate(프리팹, 생성위치, 생성시 방향)
            //Quaternion.idetity : 프리팹이 가지고있는 방향값을 그대로 사용한다는 의미
            Instantiate(explosion, transform.position, Quaternion.identity);

            SoundManager.instance.PlaySound();

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    void ShootControl()
    {
        if(canShoot == true)
        {
            // Rocket 프리팹을 0.5초간 생성시킨다.
            if (shootTimer > shootDelay)
            {
                Instantiate(rocket, transform.position, rocket.transform.rotation);
                shootTimer = 0;
            }
            shootTimer += Time.deltaTime;
        }
    }

    private void Update()
    {
        MoveControl();
        ShootControl();
    }
}
