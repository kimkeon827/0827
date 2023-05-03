using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public void LButtonDown()
    {        
        transform.Translate(-3, 0, 0);  // 왼쪽으로 3 이동        
    }
        
    public void RButtonDown()
    {
        transform.Translate(3, 0, 0);   // 오른쪽으로 3 이동
    }        
}
