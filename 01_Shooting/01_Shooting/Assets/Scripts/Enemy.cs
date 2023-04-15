using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int destroyScore = 100;

    public float moveSpeed = 0.5f;

    public GameObject explosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rocket")
        {
            // Rocket Tag와 접촉했을 때 이펙트 생성
            Instantiate(explosion, transform.position, Quaternion.identity);

            SoundManager.instance.PlaySound();

            GameManager.instance.AddScore(destroyScore);

            // Rocket 제거
            Destroy(collision.gameObject);

            // 자기 자신 제거
            Destroy(gameObject);
        }
    }

    void MoveControl()
    {
        float yMove = moveSpeed * Time.deltaTime;
        transform.Translate(0, -yMove, 0);
    }

    private void Update()
    {
        MoveControl();
    }
}
