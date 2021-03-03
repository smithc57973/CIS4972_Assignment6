/*
 * Chris Smith
 * Sigma
 * Assignment 6
 * A class to define Sigma Talon Heros.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sigma : Hero
{
    public Sigma()
    {
        this.type = "Sigma";
        this.faction = "Talon";
        this.role = "Tank";
        this.weapon = "Hyperspheres";
        this.ultimate = "Gravitic Flux";
        this.counter = "Ana";
    }
}
