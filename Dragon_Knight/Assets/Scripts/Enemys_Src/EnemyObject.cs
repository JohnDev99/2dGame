using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable_Object/New_Enemy")]

public class EnemyObject : ScriptableObject
{
    public string type;
    public int health;
    public float speed;
    public float damageToPlayer;
}
