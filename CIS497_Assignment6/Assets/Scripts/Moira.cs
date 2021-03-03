/*
 * Chris Smith
 * Ana
 * Assignment 6
 * A class to define Moira Talon Heros.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moira : Hero
{
    public Moira()
    {
        this.type = "Moira";
        this.faction = "Talon";
        this.role = "Support";
        this.weapon = "Biotic Grasp";
        this.ultimate = "Coalescence";
        this.counter = "Reinhardt";
    }
}
