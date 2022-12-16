using UnityEngine;

public class Character : MonoBehaviour
{
    public PlayerStats Strength;
    public PlayerStats EnemiesKilled;
    public PlayerStats DistanceTravialed;
    public PlayerStats DamageDelt;
    public PlayerStats DamageTaken;
    public PlayerStats Deaths;
    public PlayerStats ItemCollected;
    public PlayerStats Armor;
    
    public float stats;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            
        }
    }
}
