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
            // Rocket Tag�� �������� �� ����Ʈ ����
            Instantiate(explosion, transform.position, Quaternion.identity);

            SoundManager.instance.PlaySound();

            GameManager.instance.AddScore(destroyScore);

            // Rocket ����
            Destroy(collision.gameObject);

            // �ڱ� �ڽ� ����
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
