/*
 * Chris Smith
 * Ana
 * Assignment 6
 * A class to define Widowmaker Talon Heros.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Widowmaker : Hero
{
    public Widowmaker()
    {
        this.type = "Widowmaker";
        this.faction = "Talon";
        this.role = "DPS";
        this.weapon = "Widow's Kiss";
        this.ultimate = "Infrasight";
        this.counter = "Echo";
    }
}
