using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class HealthBar : MonoBehaviour
{
    [SerializeField] InputActionReference UseItem;



    public Slider HealthLeft;
    public int maxHealth = 100;
    private int collectedBombs = 0;

    private PlayerBehaviour playerBehaviour;

    private void OnEnable()
    {
        UseItem.action.Enable();
    }

    private void OnDisable()
    {
        UseItem.action.Disable();
    }

    private void Awake()
    {
        HealthLeft.value = 100f;
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerBehaviour = player.GetComponent<PlayerBehaviour>();
        }
    }

    private void Update()
    {
        if (collectedBombs > 0 && UseItem.action.triggered)
        {
            collectedBombs--;
            GameObject UIBomb = GameObject.FindGameObjectWithTag("BombsUI");
            UIBomb.GetComponent<BombUIUpdater>().UseBomb();

            if (playerBehaviour != null && playerBehaviour.IsAlive())
            {
                playerBehaviour.Attack();
                Debug.Log(playerBehaviour.IsAlive());

                GameObject Enemy = GameObject.FindGameObjectWithTag("Enemy");
                Enemy.GetComponent<EnemyMovement>().Hurt();
            }

            EnemyHealth enemyHealth = FindObjectOfType<EnemyHealth>();
            if (enemyHealth != null && playerBehaviour != null && playerBehaviour.IsAlive())
            {
                enemyHealth.TakeDamage(20); 
                float fillValue = (float)enemyHealth.GetCurrentHealth() / enemyHealth.GetMaxHealth();
                UpdateHealthBar(fillValue); 
            }
            if(HealthLeft.value ==0) 
            {
                Debug.Log("samorioooo");
                //poner un unity event para que se muestre el panel de you win 
                //en el unity event ocultar todo lo demás llamar a las animaciones 
            }

               //GameObject Player = GameObject.FindGameObjectWithTag("Player");
               //Player.GetComponent<PlayerBehaviour>().Attack();
            
        }
    }


    public void UpdateHealthBar(float fillValue)
    {
        HealthLeft.value = fillValue;
    }

    public void UpdateCollectedBombs()
    {
        collectedBombs++;
        Debug.Log(collectedBombs);
    }
}
