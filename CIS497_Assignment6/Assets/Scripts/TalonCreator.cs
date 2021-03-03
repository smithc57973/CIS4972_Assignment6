/*
 * Chris Smith
 * TalonCreator
 * Assignment 6
 * A class to handle creation of Talon heros.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalonCreator : HeroCreator
{
    public Hero heroToSpawn;
    public GameObject h;

    public override Hero CreateHero(string type)
    {
        heroToSpawn = null;

        switch (type)
        {
            case "Sigma":
                heroToSpawn = new Sigma();
                break;
            case "Widowmaker":
                heroToSpawn = new Widowmaker();
                break;
            case "Moira":
                heroToSpawn = new Moira();
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
            case "Sigma":
                h = Resources.Load<GameObject>("Cube - Sigma");
                break;
            case "Widowmaker":
                h = Resources.Load<GameObject>("Capsule - Widowmaker");
                break;
            case "Moira":
                h = Resources.Load<GameObject>("Sphere - Moira");
                break;
            default:
                break;
        }

        return h;
    }

    public override GameObject AddScript(GameObject prefab, string type)
    {
        if (type.Equals("Sigma"))
        {
            if (prefab.GetComponent<Sigma>() == null)
            {
                prefab.AddComponent<Sigma>();
            }
        }
        else if (type.Equals("Widowmaker"))
        {
            if (prefab.GetComponent<Widowmaker>() == null)
            {
                prefab.AddComponent<Widowmaker>();
            }

        }
        else if (type.Equals("Moira"))
        {
            if (prefab.GetComponent<Moira>() == null)
            {
                prefab.AddComponent<Moira>();
            }
        }

        return prefab;
    }
}
