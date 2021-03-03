/*
 * Chris Smith
 * Hero
 * Assignment 6
 * A class to define a hero.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public string type;
    public string faction;
    public string role;
    public string weapon;
    public string ultimate;
    public string counter;

    //Displays hero info
    public override string ToString()
    {
        return "Hero " + this.type + " of faction " + this.faction + " with role " + this.role + " uses weapon " + this.weapon + " and ultimate " + this.ultimate + ".\n";
    }
}


