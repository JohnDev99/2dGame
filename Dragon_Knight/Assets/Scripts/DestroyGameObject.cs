using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    [SerializeField] float timeToDestroy = 0f;
    private void DestroyObj()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
