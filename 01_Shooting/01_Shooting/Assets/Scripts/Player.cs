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

    private void OnTriggerEnter2D(Collider2D collision) // �浹ü �������ڸ� �޴� �Լ� �߰�
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //Instantiate(������, ������ġ, ������ ����)
            //Quaternion.idetity : �������� �������ִ� ���Ⱚ�� �״�� ����Ѵٴ� �ǹ�
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
            // Rocket �������� 0.5�ʰ� ������Ų��.
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
