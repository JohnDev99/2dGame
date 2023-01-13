using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : EnemyBase
{
    public float speed = 2f;
    //Clamp entre 2 vetores
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

    }

}
