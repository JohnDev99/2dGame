using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    Transform entityScale;
    protected bool isEntityFacingRight;
    // Start is called before the first frame update

    private void Awake()
    {
        entityScale = GetComponent<Transform>();
    }
    void Start()
    {
        isEntityFacingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected Vector3 EntityLocalScale(Transform entity)
    {
        Vector3 flipScale;
        Vector3 newScale = entity.localScale;
        if (isEntityFacingRight)
        {
            newScale.x *= -1;
            flipScale = newScale;
            isEntityFacingRight = false;
        }
        else
        {
            newScale.x *= 1;
            flipScale = newScale;
            isEntityFacingRight = true;
        }

        return newScale;
    }
}
