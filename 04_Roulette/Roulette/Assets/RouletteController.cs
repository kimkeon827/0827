using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteController : MonoBehaviour
{
    float rotSpeed = 0.0f;  // 회전속도

    void Update()
    {
        // 클릭하면 회전 속도를 설정한다.
        if(Input.GetMouseButtonDown(0))
        {
            this.rotSpeed = 100.0f;
        }

        // 회전 속도만큼 룰렛을 회전시킨다.
        transform.Rotate(0, 0, this.rotSpeed);

        // 룰렛을 감속시킨다.
        this.rotSpeed *= 0.96f;
    }
}
