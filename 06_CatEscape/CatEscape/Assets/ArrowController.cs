using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    GameObject player;
    
    private void Start()
    {
        this.player = GameObject.Find("player");
    }

    void Update()
    {
        transform.Translate(0, -0.1f, 0);   // 프레임마다 등속으로 낙하

        if (transform.position.y < -5.0f)   // 땅보다 아래로 떨어지면
        {
            Destroy(gameObject);       // 이 오브젝트롤 파괴
        }

        // 충돌 판정
        Vector2 p1 = transform.position;                // 화살의 중심 좌표
        Vector2 p2 = this.player.transform.position;    // 플레이어의 중심 좌표
        Vector2 dir = p1 - p2;
        float d = dir.magnitude;
        float r1 = 0.5f;                                // 화살의 반경
        float r2 = 1.0f;                                // 플레이어의 반경

        if (d < r1 + r2)            // 충돌하면
        {
            // GameDirector에 플레이어와 화살 충돌 전달
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().DecreaseHp();

            Destroy(gameObject);    // 화살 삭제
        }
    }
}
