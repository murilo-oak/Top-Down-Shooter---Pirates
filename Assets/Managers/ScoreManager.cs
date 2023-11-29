using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score { get; private set; } = 0;
    [Min(0)][SerializeField] private int pointsPerEnemyDeath = 1;

    public void IncreaseScore()
    {
        score += pointsPerEnemyDeath;
    }

    public void Reset()
    {
        score = 0;
    }
}
