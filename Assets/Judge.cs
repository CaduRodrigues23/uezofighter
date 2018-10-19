using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : MonoBehaviour {
    public Player[] players;
    public int[] scores;
    public Vector3[] positions;
    public int toWin = 3;
    private static bool canHit = true;

    public static bool HitPermission()
    {
        return canHit;
    }

	void Start () {
        scores = new int[players.Length];
        positions = new Vector3[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            scores.SetValue(toWin, i);
            positions.SetValue(players[i].transform.position, i);
        }
	}
	
    public void UpdateScore(GameObject loser)
    {
        scores[loser.GetComponent<Player>().id]--;
        if (scores[loser.GetComponent<Player>().id] <= 0)
        {
            Destroy(loser);
            if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
            {
                DeclareWinner(GameObject.FindGameObjectWithTag("Player"));
            }
        } else
        {
            StartCoroutine(TimeForPlayers());
        }
    }

    IEnumerator TimeForPlayers()
    {
        canHit = false;
        yield return new WaitForSeconds(3);
        ResetPositions();
        canHit = true;
    }

    private void ResetPositions()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = (Vector3) positions.GetValue(i);
        }
    }

    private void DeclareWinner(GameObject winner)
    {
        Debug.Log("The winner is: " + winner.GetComponent<Player>().name);
    }

    public int getScore(int player)
    {
        return scores[player];
    }
}
