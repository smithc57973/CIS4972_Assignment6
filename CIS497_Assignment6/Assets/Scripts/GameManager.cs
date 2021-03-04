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

public enum GameState { Start, Enemy, Player, Compare, End };

public class GameManager : MonoBehaviour
{
    //Vars
    public HeroCreator talonSpawner;
    public HeroCreator owSpawner;
    public List<GameObject> enemies;
    public List<GameObject> allies;
    public GameState state;
    public GameState prevState;
    public int playerScore;
    public Text tutorial;
    public Text score;
    public Text status;
    public bool gameOver;
    public bool firstPlay;
    public IEnumerator co;

    // Start is called before the first frame update
    void Start()
    {
        //Setup
        state = GameState.Start;
        prevState = state;
        talonSpawner = new TalonCreator();
        owSpawner = new OverwatchCreator();
        playerScore = 0;
        gameOver = false;
        firstPlay = true;
        co = Tutorial();
        StartCoroutine(co);
    }

    // Update is called once per frame
    void Update()
    {
        //Update score text
        score.text = "Score: " + playerScore;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StopCoroutine(co);
            tutorial.enabled = false;
            prevState = state;
            state = GameState.Enemy;
            Choose("");
        }

        //Determine Win/Loss
        if (allies.Count == 0 && enemies.Count != 0 && !firstPlay)
        {
            gameOver = true;
            score.text += "\nYou Lose! Press R to restart.";
        }
        if (playerScore >= 10)
        {
            gameOver = true;
            score.text += "\nYou Win! Press R to restart.";
        }

        //Stop gameplay on gameOver
        if (gameOver)
        {
            state = GameState.End;
        }
        
        //Reset game
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    //Requests spawning and instantiates prefabs
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
        status.text = "Spawned: " + instance.GetComponent<Hero>().ToString();
        return instance;
    }

    //Plays the tutorial messages and transfers to Enemy state
    public IEnumerator Tutorial()
    {
        tutorial.text = "Tutorial(Press enter to skip):\nThe enemy is going to spawn a hero. Push one of these buttons to spawn your own hero.";
        yield return new WaitForSeconds(3f);
        tutorial.text = "Tutorial(Press enter to skip):\nAfterwards, any hero that is counterd will be destroyed.";
        yield return new WaitForSeconds(3f);
        tutorial.text = "Tutorial(Press enter to skip):\nAbove the buttons shows each of the counters. This is read as Left counters Right.";
        yield return new WaitForSeconds(3f);
        tutorial.text = "Tutorial(Press enter to skip):\nReach 10 points before you are completely countered in order to win.";
        yield return new WaitForSeconds(3f);
        tutorial.text = "Tutorial(Press enter to skip):\nPress R at any time to reset the game.";
        yield return new WaitForSeconds(3f);
        tutorial.enabled = false;

        prevState = state;
        state = GameState.Enemy;
        Choose("");
    }

    //Player/Enemy method for picking their hero, requests spawns
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
            bool hasPicked = false;
            switch (type)
            {
                case "Reinhardt":
                    allies.Add(Spawn("Reinhardt", owSpawner));
                    hasPicked = true;
                    break;
                case "Sigma":
                    allies.Add(Spawn("Sigma", talonSpawner));
                    hasPicked = true;
                    break;
                case "Echo":
                    allies.Add(Spawn("Echo", owSpawner));
                    hasPicked = true;
                    break;
                case "Widowmaker":
                    allies.Add(Spawn("Widowmaker", talonSpawner));
                    hasPicked = true;
                    break;
                case "Ana":
                    allies.Add(Spawn("Ana", owSpawner));
                    hasPicked = true;
                    break;
                case "Moira":
                    allies.Add(Spawn("Moira", talonSpawner));
                    hasPicked = true;
                    break;
                default:
                    break;
            }
            if (hasPicked)
            {
                firstPlay = false;
                prevState = state;
                state = GameState.Compare;
                Compare();
            }
            /*else
            {
                Choose("");
            }*/
        }
    }

    //Based on which side just picked, checks to see if any heros are countered on the opposing team
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
                        toRemove.Add(j);
                        playerScore++;
                    }
                }
                enemies = enemies.Except(toRemove).ToList();
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

        if (prevState == GameState.Enemy)
        {
            foreach (GameObject i in enemies)
            {
                foreach (GameObject j in allies)
                {
                    if (i.GetComponent<Hero>().counter.Equals(j.GetComponent<Hero>().type))
                    {
                        toRemove.Add(j);
                    }
                }
                allies = allies.Except(toRemove).ToList();
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
    }
}
