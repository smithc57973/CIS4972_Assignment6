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
    //Unused creation method
    //public abstract Hero CreateHero(string type);

    //Creation methods
    public abstract GameObject CreatePrefab(string type);
    public abstract GameObject AddScript(GameObject prefab, string type);
}
