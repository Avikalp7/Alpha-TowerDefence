using UnityEngine;
using System.Collections.Generic;
using System;

public class Guiders : MonoBehaviour {

	public static Transform[] points;
    public static float[] vstar;
    public static int[] wayPointVisits;
    private static System.Random rnd = new System.Random();
    public static float explorationProbabilityStart = 0.9f;
    public static float decayRate = 0.03f;
    //int rand = generator.RandomNumber(5, 100);

    public static List<List<int>> validGoToStates = new List<List<int>>();


	void Awake ()
	{
		points = new Transform[transform.childCount];
		for (int i = 0; i < points.Length; i++)
		{
			points[i] = transform.GetChild(i);
		}
        makeValidGoToStates();
        initiateWayPointVisits();
        vstar = new float[points.Length];
        setVstar();
	}

    private void initiateWayPointVisits()
    {
        wayPointVisits = new int[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            wayPointVisits[i] = 0;
        }
    }

    private void makeValidGoToStates()
    {
        for (int i = 0; i < points.Length; i++)
        {
            validGoToStates.Add(new List<int>());
        }

        for (int i = 0; i < 5; i++)
        {
            validGoToStates[i].Add(i + 1);
        }

        validGoToStates[0].Add(6);

        for (int i = 6; i < 12; i++)
        {
            validGoToStates[i].Add(i + 1);
        }

        if (goToLevel.levelNum == 2)
        {
            validGoToStates[16].Add(5);
            validGoToStates[12].Add(13);
            validGoToStates[7].Add(14);
            validGoToStates[14].Add(15);
            validGoToStates[15].Add(16);
            validGoToStates[6].Add(17);
            validGoToStates[17].Add(18);
        }
    }

    private void setVstar()
    {
        for (int i = 0; i < points.Length; i++)
        {
            vstar[i] = 0;
        }
        // Final state reward
        vstar[points.Length - 1] = 100;
        // Any other state that is also final:
        vstar[5] = 100;
        if (goToLevel.levelNum == 2)
        {
            vstar[13] = 100;
        }
    }

    void start ()
    {
        setVstar();
    }

    public static int GetNextWaypoint(int currentWayPoint, float healthFraction, bool isDecoy, int waveIndex)
    {
        float maxVstar = -1 * Mathf.Infinity;
        int nextWayPointIndex = -1;

        int rand100 = rnd.Next(1, 101);
        float explorationProbability = Mathf.Max(explorationProbabilityStart - decayRate * waveIndex, 0.1f);

        if (isDecoy || rand100 < (int)(explorationProbability*100))
        {
            int numPossibleStates = validGoToStates[currentWayPoint].Count;
            int randomState = rnd.Next(0, numPossibleStates);
            nextWayPointIndex = validGoToStates[currentWayPoint][randomState];
        }

        else {
            foreach (int wayPointIndex in validGoToStates[currentWayPoint])
            {
                if (vstar[wayPointIndex] > maxVstar)
                {
                    maxVstar = vstar[wayPointIndex];
                    nextWayPointIndex = wayPointIndex;
                }
            }
        }

        wayPointVisits[currentWayPoint] += 1;
        vstar[currentWayPoint] = (vstar[currentWayPoint] + healthFraction*vstar[nextWayPointIndex]) / (float)wayPointVisits[currentWayPoint];
        return nextWayPointIndex;
    }
}
