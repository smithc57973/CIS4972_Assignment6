/*
 * Chris Smith
 * Ana
 * Assignment 6
 * A class to define Ana Overwatch Heros.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ana : Hero
{
    public Ana()
    {
        this.type = "Ana";
        this.faction = "Overwatch";
        this.role = "Support";
        this.weapon = "Biotic Rifle";
        this.ultimate = "Nano Boost";
        this.counter = "Moira";
    }
}
