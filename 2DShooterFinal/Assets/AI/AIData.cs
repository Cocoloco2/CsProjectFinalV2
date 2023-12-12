using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIData : MonoBehaviour
{
    public List<Transform> targets = null;
    public Collider2D[] obstacles = null;
    public Transform currentTarget;


//if target is null return 0 else return targets.Count
    public int getTargetsCount() => targets == null ? 0 : targets.Count;
}
