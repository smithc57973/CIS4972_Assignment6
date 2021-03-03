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

public enum GameState { Start, Enemy, Player, Compare };

public class GameManager : MonoBehaviour
{
    public HeroCreator talonSpawner;
    public HeroCreator owSpawner;
    public GameState state;
    public int playerScore;
    public int enemyScore;
    /*public List<Hero> enemies;
    public List<Hero> allies;*/
    public GameObject enemy;
    public GameObject ally;
    public Text tutorial;
    public Text score;
    public Text heroList;
    public Text enemyList;

    // Start is called before the first frame update
    void Start()
    {
        //Setup
        state = GameState.Start;
        talonSpawner = new TalonCreator();
        owSpawner = new OverwatchCreator();
        /*enemies = new List<Hero>();
        allies = new List<Hero>();*/
        playerScore = 0;
        enemyScore = 0;
        heroList.text = "";
        enemyList.text = "";
        StartCoroutine(Tutorial());
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Player: " + playerScore + "\nEnemy: " + enemyScore;
        
        //Reset game
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void UpdateLists()
    {
        heroList.text = "";
        enemyList.text = "";
        /*foreach (Hero item in allies)
        {
            heroList.text += "\n" + item.type;
        }
        foreach (Hero item in enemies)
        {
            enemyList.text += "\n" + item.type;
        }*/
    }

    public GameObject Spawn(string type)
    {
        GameObject h = null;
        h = owSpawner.CreatePrefab(type);
        Vector3 spawnPos;
        if (h.GetComponent<Hero>().faction.Equals("Overwatch"))
        {
            spawnPos = new Vector3(-8, 0, 0);
        }
        else
        {
            spawnPos = new Vector3(8, 0, 0);
        }
        GameObject instance = Instantiate(h, spawnPos, Quaternion.identity);

        owSpawner.AddScript(instance, type);
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
                /*case 0:
                    enemies.Add(owSpawner.CreateHero("Reinhardt"));
                    break;
                case 1:
                    enemies.Add(talonSpawner.CreateHero("Sigma"));
                    break;
                case 2:
                    enemies.Add(owSpawner.CreateHero("Echo"));
                    break;
                case 3:
                    enemies.Add(talonSpawner.CreateHero("Widowmaker"));
                    break;
                case 4:
                    enemies.Add(owSpawner.CreateHero("Ana"));
                    break;
                case 5:
                    enemies.Add(talonSpawner.CreateHero("Moira"));
                    break;
                default:
                    break;*/
                case 0:
                    enemy = Spawn("Reinhardt");
                    break;
                case 1:
                    enemy = Spawn("Sigma");
                    break;
                case 2:
                    enemy = Spawn("Echo");
                    break;
                case 3:
                    enemy = Spawn("Widowmaker");
                    break;
                case 4:
                    enemy = Spawn("Ana");
                    break;
                case 5:
                    enemy = Spawn("Moira");
                    break;
                default:
                    break;
            }
            UpdateLists();
            state = GameState.Player;
        }
        else if (state == GameState.Player)
        {
            switch (type)
            {
                case "Reinhardt":
                    ally = Spawn("Reinhardt");
                    break;
                case "Sigma":
                    ally = Spawn("Sigma");
                    break;
                case "Echo":
                    ally = Spawn("Echo");
                    break;
                case "Widowmaker":
                    ally = Spawn("Widowmaker");
                    break;
                case "Ana":
                    ally = Spawn("Ana");
                    break;
                case "Moira":
                    ally = Spawn("Moira");
                    break;
                default:
                    break;
            }
            UpdateLists();
            state = GameState.Compare;
            Compare();
        }
    }

    public void Compare()
    {
        if (state == GameState.Compare)
        {
            /*foreach (Hero i in allies)
            {
                foreach (Hero j in enemies)
                {
                    if (i.counter.Equals(j.type))
                    {
                        playerScore++;
                        enemies.Remove(j);
                        //Destroy(j);
                    }
                }
            }
            UpdateLists();

            foreach (Hero i in enemies)
            {
                foreach (Hero j in allies)
                {
                    if (i.counter.Equals(j.type))
                    {
                        enemyScore++;
                        enemies.Remove(j);
                        //Destroy(j);
                    }
                }
            }
            UpdateLists();

            if (allies.Count == 0 && enemies.Count != 0)
            {
                score.text = "You Lose! Press R to restart";
            }
            else if (enemies.Count == 0 && allies.Count != 0)
            {
                score.text = "You Win! Press R to restart";
            }
            else
            {
                state = GameState.Enemy;
                Choose("");
            }*/

            if (ally.counter.Equals(enemy.type))
            {
                enemy = null;
                playerScore++;
            }
            else if (enemy.counter.Equals(ally.type))
            {
                ally = null;
                enemyScore++;
            }
            else
            {
                state = GameState.Enemy;
                Choose("");
            }
        }

        
    }
}
