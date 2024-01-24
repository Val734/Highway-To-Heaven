using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTexture : MonoBehaviour
{
    //En este script desplazo la textura del objeto con un render a lo largo del eje Y con una velocidad concreta, básicamente para darle movimiento a la lava de mi escenario
    public float scrollSpeed = 0.015f;
    Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float moveThis = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, moveThis));

    }
}