using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaAnimate : MonoBehaviour
{
    public float animSpeed;
    Material m;

    void Start()
    {
        m = GetComponent<Renderer>().material;
    }

    void Update()
    {
        m.mainTextureOffset = new Vector2(m.mainTextureOffset.x + animSpeed*Time.deltaTime, m.mainTextureOffset.y);
    }
}
