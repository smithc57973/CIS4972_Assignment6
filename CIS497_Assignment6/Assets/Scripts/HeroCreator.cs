/*
 * Chris Smith
 * HeroCreator
 * Assignment 6
 * An abstract class to create heros.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroCreator
{
    public abstract Hero CreateHero(string type);

    public abstract GameObject CreatePrefab(string type);
    public abstract GameObject AddScript(GameObject prefab, string type);
}
