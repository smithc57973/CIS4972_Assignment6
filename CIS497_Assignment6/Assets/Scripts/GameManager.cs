/*
 * Chris Smith
 * GameManager
 * Assignment 6
 * A script to manage game state.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public enum GameState { Start, Enemy, Player, Compare };

public class GameManager : MonoBehaviour
{
    public HeroCreator talonSpawner;
    public HeroCreator owSpawner;
    public List<GameObject> enemies;
    public List<GameObject> allies;
    public GameState state;
    public GameState prevState;
    public int playerScore;
    public Text tutorial;
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        //Setup
        state = GameState.Start;
        prevState = state;
        talonSpawner = new TalonCreator();
        owSpawner = new OverwatchCreator();
        playerScore = 0;
        StartCoroutine(Tutorial());
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + playerScore;
        
        //Reset game
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public GameObject Spawn(string type, HeroCreator hc)
    {
        GameObject h = null;
        h = hc.CreatePrefab(type);
        Vector3 spawnPos;
        if (state == GameState.Player)
        {
            spawnPos = new Vector3(-8, 10 - (2 * allies.Count), 10);
        }
        else
        {
            spawnPos = new Vector3(8, 10 - (2 * enemies.Count), 10);
        }
        GameObject instance = Instantiate(h, spawnPos, Quaternion.identity);
        hc.AddScript(instance, type);
        return instance;
    }

    //Plays the tutorial messages and transfers to Enemy state
    public IEnumerator Tutorial()
    {
        /*tutorial.text = "Tutorial:\nThe enemy is going to spawn a hero.";
        yield return new WaitForSeconds(3f);
        tutorial.text = "Tutorial:\nPush one of these buttons to spawn your own hero.";
        yield return new WaitForSeconds(3f);
        tutorial.text = "Tutorial:\nAfterwards, any hero that is counterd will be destroyed.";
        yield return new WaitForSeconds(3f);
        tutorial.text = "Tutorial:\nAbove the buttons shows each of the counters. This is read as Left counters Right.";
        yield return new WaitForSeconds(3f);
        tutorial.text = "Tutorial:\nCounter all of your opponents heros without losing all of yours.";
        yield return new WaitForSeconds(3f);*/
        tutorial.text = "Tutorial:\nPress R at any time to reset the game.";
        yield return new WaitForSeconds(3f);
        tutorial.enabled = false;

        prevState = state;
        state = GameState.Enemy;
        Choose("");
    }

    public void Choose(string type)
    {
        if (state == GameState.Enemy)
        {
            int r = Random.Range(0, 6);
            switch (r)
            {
                case 0:
                    enemies.Add(Spawn("Reinhardt", owSpawner));
                    break;
                case 1:
                    enemies.Add(Spawn("Sigma", talonSpawner));
                    break;
                case 2:
                    enemies.Add(Spawn("Echo", owSpawner));
                    break;
                case 3:
                    enemies.Add(Spawn("Widowmaker", talonSpawner));
                    break;
                case 4:
                    enemies.Add(Spawn("Ana", owSpawner));
                    break;
                case 5:
                    enemies.Add(Spawn("Moira", talonSpawner));
                    break;
                default:
                    break;
            }
            prevState = state;
            state = GameState.Compare;
            Compare();
        }

        if (state == GameState.Player)
        {
            switch (type)
            {
                case "Reinhardt":
                    allies.Add(Spawn("Reinhardt", owSpawner));
                    break;
                case "Sigma":
                    allies.Add(Spawn("Sigma", talonSpawner));
                    break;
                case "Echo":
                    allies.Add(Spawn("Echo", owSpawner));
                    break;
                case "Widowmaker":
                    allies.Add(Spawn("Widowmaker", talonSpawner));
                    break;
                case "Ana":
                    allies.Add(Spawn("Ana", owSpawner));
                    break;
                case "Moira":
                    allies.Add(Spawn("Moira", talonSpawner));
                    break;
                default:
                    break;
            }
            prevState = state;
            state = GameState.Compare;
            Compare();
        }
    }

    public void Compare()
    {
        List<GameObject> toRemove = new List<GameObject>();
        if (prevState == GameState.Player)
        {
            foreach (GameObject i in allies)
            {
                foreach (GameObject j in enemies)
                {
                    if (i.GetComponent<Hero>().counter.Equals(j.GetComponent<Hero>().type))
                    {
                        //enemies.Remove(j);
                        toRemove.Add(j);
                        playerScore++;
                    }
                }
                enemies = enemies.Except(toRemove).ToList();
                /*foreach (GameObject item in toRemove)
                {
                    Destroy(item);
                }
                toRemove.Clear();*/
            }
            foreach (GameObject item in toRemove)
            {
                Destroy(item);
            }
            toRemove.Clear();
            prevState = state;
            state = GameState.Player;
            Choose("");
        }

        if (prevState == GameState.Enemy)
        { 
            foreach (GameObject i in enemies)
            {
                foreach (GameObject j in allies)
                {
                    if (i.GetComponent<Hero>().counter.Equals(j.GetComponent<Hero>().type))
                    {
                        //allies.Remove(j);
                        toRemove.Add(j);
                    }
                }
                allies = allies.Except(toRemove).ToList();
                /*foreach (GameObject item in toRemove)
                {
                    Destroy(item);
                }
                toRemove.Clear();*/
            }
            foreach (GameObject item in toRemove)
            {
                Destroy(item);
            }
            toRemove.Clear();

            prevState = state;
            state = GameState.Enemy;
            Choose("");
        }

    }
}
