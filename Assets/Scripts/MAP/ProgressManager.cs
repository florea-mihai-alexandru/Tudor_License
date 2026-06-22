using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;

    public int currentStage = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void CompleteStage()
    {
        currentStage++;
    }
}