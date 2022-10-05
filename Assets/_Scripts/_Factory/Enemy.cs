using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract string Name { get; }

    public abstract GameObject Create(GameObject prefab);
}

public class Crab : Enemy
{
    public override string Name => "crab";
    //public override string Name { get { return "crab"; } }

    public override GameObject Create(GameObject prefab)
    {
        GameObject enemy = Instantiate(prefab);
        Debug.Log("creating crab enemy");
        return enemy;
    }
}

public class Monster : Enemy
{
    public override string Name => "nster";

    public override GameObject Create(GameObject prefab)
    {
        GameObject enemy = Instantiate(prefab);
        Debug.Log("creating mmonster enemy");
        return enemy;
    }
}