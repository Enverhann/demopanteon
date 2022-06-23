using UnityEngine;

public class Runners : MonoBehaviour
{
    public Transform lastWaypoint; //Transform reference to the waypoint behind the players.
    private int currentWaypoint; //This variable will be incremented with each waypoint reached.
    private int currentLap; //If the game has more than one lap, it will also need to increment.

    private int waypontMultiplier = 1000; //This variable will only multiply the amount of waypoint by 1000.
    private int lapMultiplier = 10000; //For the return, it will be for 10000

    private void Start()
    {
        currentWaypoint = 0;
        currentLap = 0;
    }
    private float GetDistance()
    {
        return (transform.position - lastWaypoint.position).magnitude + currentWaypoint * waypontMultiplier + currentLap * lapMultiplier;
    }
    public int GetCarPosition(Runners[] allRunners)
    {
        float distance = GetDistance();
        int position = 1;
        foreach (Runners runner in allRunners)
        {
            if (runner.GetDistance() > distance)
                position++;
        }
        return position;
    }
}
