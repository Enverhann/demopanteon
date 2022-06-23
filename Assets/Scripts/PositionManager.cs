using UnityEngine;

public class PositionManager : MonoBehaviour
{
    [SerializeField] private Position _position;
    public Runners[] allRunners;
    public Runners[] runnerOrder;
    public static int cars;
    public void Start()
    {
        cars = allRunners.Length;
        runnerOrder = new Runners[allRunners.Length];
        InvokeRepeating("ManualUpdate", 0.5f, 0.5f);
    }

    public void ManualUpdate()
    {
        foreach (Runners runner in allRunners)
        {
            runnerOrder[runner.GetCarPosition(allRunners) - 1] = runner;
        }
        _position.PositionUpdate(allRunners[0].GetComponent<Runners>().GetCarPosition(allRunners) + "/" + cars);
        _position.FinishPosition("You have finished " + allRunners[0].GetComponent<Runners>().GetCarPosition(allRunners) + "/" + cars + " this game.");
    }
}
