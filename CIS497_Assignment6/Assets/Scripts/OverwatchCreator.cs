/*
 * Chris Smith
 * OverwatchCreator
 * Assignment 6
 * A class to handle creation of Overwatch heros.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverwatchCreator : HeroCreator
{
    public Hero heroToSpawn;
    public GameObject h;

    public override Hero CreateHero(string type)
    {
        heroToSpawn = null;

        switch (type)
        {
            case "Reinhardt":
                //ow = Resources.Load<GameObject>("Cube - Reinhardt");
                heroToSpawn = new Reinhardt();
                break;
            case "Echo":
                //ow = Resources.Load<GameObject>("Capsule - Echo");
                heroToSpawn = new Echo();
                break;
            case "Ana":
                //ow = Resources.Load<GameObject>("Sphere - Ana");
                heroToSpawn = new Ana();
                break;
            default:
                break;
        }

        return heroToSpawn;
    }

    public override GameObject CreatePrefab(string type)
    {
        switch (type)
        {
            case "Reinhardt":
                h = Resources.Load<GameObject>("Cube - Reinhardt");
                break;
            case "Echo":
                h = Resources.Load<GameObject>("Capsule - Echo");
                break;
            case "Ana":
                h = Resources.Load<GameObject>("Sphere - Ana");
                break;
            default:
                break;
        }

        return h;
    }

    public override GameObject AddScript(GameObject prefab, string type)
    {
        if (type.Equals("Reinhardt"))
        {
            if (prefab.GetComponent<Reinhardt>() == null)
            {
                prefab.AddComponent<Reinhardt>();
            }
        }
        else if (type.Equals("Echo"))
        {
            if (prefab.GetComponent<Echo>() == null)
            {
                prefab.AddComponent<Echo>();
            }

        }
        else if (type.Equals("Ana"))
        {
            if (prefab.GetComponent<Ana>() == null)
            {
                prefab.AddComponent<Ana>();
            }
        }

        return prefab;
    }
}
