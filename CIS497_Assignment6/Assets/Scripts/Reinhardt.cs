/*
 * Chris Smith
 * Ana
 * Assignment 6
 * A class to define Reinhardt Overwatch Heros.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reinhardt : Hero
{
    // Start is called before the first frame update
    public Reinhardt()
    {
        this.type = "Reinhardt";
        this.faction = "Overwatch";
        this.role = "Tank";
        this.weapon = "Rocket Hammer";
        this.ultimate = "Earth Shatter";
        this.counter = "Widowmaker";
    }
}
