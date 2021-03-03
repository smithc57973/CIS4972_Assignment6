/*
 * Chris Smith
 * Ana
 * Assignment 6
 * A class to define Echo Overwatch Heros.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : Hero
{
    public Echo()
    {
        this.type = "Echo";
        this.faction = "Overwatch";
        this.role = "DPS";
        this.weapon = "Tri-Shot";
        this.ultimate = "Duplicate";
        this.counter = "Sigma";
    }
}
