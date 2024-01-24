using TMPro;
using UnityEngine;

public class BombUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI bombCounterText;
    public int maxBombs = 3;
    private int bombCounter;
    public GameObject throwBombAlert; 

    void Start()
    {
        bombCounter = 0;
        UpdateBombCounter();
        throwBombAlert.SetActive(false);
    }

    private void Update()
    {
        if (bombCounter > 0)
        {
            throwBombAlert.SetActive(true);
        }
        else
        {
            throwBombAlert.SetActive(false);
        }
    }
    public void CollectBomb()
    {
        if (bombCounter < maxBombs)
        {
            bombCounter++;
            UpdateBombCounter();
            GameObject EnemyHealthBar = GameObject.FindGameObjectWithTag("EnemyHealthBar");
            EnemyHealthBar.GetComponent<HealthBar>().UpdateCollectedBombs();
        }
    }

    public void UseBomb()
    {
        if (bombCounter > 0)
        {
            bombCounter--;
            UpdateBombCounter();
        }
    }

    void UpdateBombCounter()
    {
        bombCounterText.text = $"Bombs: {bombCounter}/{maxBombs}";
    }
}