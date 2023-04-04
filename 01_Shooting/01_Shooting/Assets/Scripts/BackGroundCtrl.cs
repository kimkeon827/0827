using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundCtrl : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    Material myMaterial;

    private void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        float newOffsetY = myMaterial.mainTextureOffset.y + scrollSpeed * Time.deltaTime;
        Vector2 newOffset = new Vector2(0, newOffsetY);

        myMaterial.mainTextureOffset = newOffset;
    }
}
