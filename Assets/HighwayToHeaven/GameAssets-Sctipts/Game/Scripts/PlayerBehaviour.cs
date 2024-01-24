using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] InputActionReference flip;
    [SerializeField] InputActionReference jump;


    [SerializeField] GameObject soundManager;
    private AudioSource jumpSound;
    private AudioSource hurtSound;
    private AudioSource dieSound;
    private AudioSource boostSound;
    private AudioSource fallingSound;
    private AudioSource victorySound;
    private AudioSource explosionSound;





    [SerializeField] Vector3 force;

    Rigidbody rb;
    [SerializeField] float jumpForce = 200f;

    public float speed;
    private bool canTurn = false;
    private bool canJump = false;
    public bool isAlive = true;
    private bool turnDirection = false;
    private bool hasCollidedWithEnemy = false;
    public UnityEvent DieMenu; 
    private Animator _anim;
    
    //falso por lo tanto empieza en 90º


    private void Awake()
    {
        flip.action.Enable();
        jump.action.Enable();

        rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        //_anim.SetBool("Run", true);

        jumpSound = soundManager.transform.Find("JumpSound").GetComponent<AudioSource>();
        hurtSound = soundManager.transform.Find("HurtSound").GetComponent<AudioSource>();
        dieSound = soundManager.transform.Find("DieSound").GetComponent<AudioSource>();
        boostSound = soundManager.transform.Find("BoostSound").GetComponent<AudioSource>(); 
        fallingSound= soundManager.transform.Find("FallingSound").GetComponent<AudioSource>();
        victorySound = soundManager.transform.Find("VictorySound").GetComponent<AudioSource>();
        explosionSound= soundManager.transform.Find("ExplosionSound").GetComponent<AudioSource>();




    }

    private void Update()
    {
        //Aquí gestiono el movimiento del jugador que siempre se mueva hacia adelante, con giros y saltos en respuesta a las acciones del jugador.
        if (!hasCollidedWithEnemy)
        { 
            //probarlo con el rigidbody

            //transform.Translate(Vector3.forward * Time.deltaTime * speed);
            Vector3 newVelocity; 
            newVelocity= transform.forward*speed;
            newVelocity.y=rb.velocity.y;
            rb.velocity = newVelocity;

            if (flip.action.triggered)
            {
                TurnCharacter();
            }
            

            if (jump.action.triggered && canJump)
            {
                JumpCharacter();
            }
        }


    }
    // Aquí el jugador gira al personaje izquierda o derecha en función de la dirección del bloque en el que se encuentra
    public void TurnCharacter()
    {

        if (canTurn)
        {
            canTurn = false;
            if (!turnDirection)
            {
                transform.Rotate(0.0f, 90.0f, 0.0f);
                turnDirection = true;
                transform.position=new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));

            }
            else if(turnDirection)
            {
                transform.Rotate(0.0f, -90.0f, 0.0f);
                turnDirection = false;
                transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));

            }

        }

    }
    //Aquí salta aplicando la fuerza establecida hacia arrba
    public void JumpCharacter()
    {
        rb.AddForce(Vector3.up * jumpForce);

        _anim.SetTrigger("Jump"); 
        jumpSound.Play();

        canJump = false;

    }
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("Jump"))
        {
            Debug.Log("Puedes SALTAR salu2");

            canJump = true;
            canTurn = false;
        }
        else if (collision.gameObject.CompareTag("Flip"))
        {
            Debug.Log("Puedes GIRAR salu2");
            canTurn = true;
            canJump = false;

        }
        else if (collision.gameObject.CompareTag("FirePlatform"))
        {
            Debug.Log("Te has morido quemao");
            speed = 0.0f;
            canJump = false;
            canTurn = false;
            isAlive = false;
            _anim.SetTrigger("Die");
            dieSound.Play();

            GameObject Enemy = GameObject.FindGameObjectWithTag("Enemy");
            Enemy.GetComponent<EnemyMovement>().DemonBehaviour();

            GameObject Time = GameObject.FindGameObjectWithTag("Time");
            Time.GetComponent<TimeBehaviour>().StopTime();

            StartCoroutine(ShowDieMenu());

        }
        else if (collision.gameObject.CompareTag("SpikePlatform"))
        {
            Debug.Log("Te has pinchau");
            _anim.SetTrigger("Hurt");
            StartCoroutine(SlowVelocity());
            hurtSound.Play();

            //speed = 0.0f; relentizar velocidad
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            hasCollidedWithEnemy = true;
            speed = 0.0f;
            GameObject Enemy = GameObject.FindGameObjectWithTag("Enemy");
            Enemy.GetComponent<EnemyMovement>().DemonBehaviour();
            _anim.SetTrigger("Die");
            dieSound.Play();
            GameObject Time = GameObject.FindGameObjectWithTag("Time");
            Time.GetComponent<TimeBehaviour>().StopTime();
            canJump = false;
            isAlive= false;
            canTurn = false;
            StartCoroutine(ShowDieMenu());

        }
    }
    IEnumerator ShowDieMenu()
    {
        yield return new WaitForSeconds(5f);
        DieMenu.Invoke();

    }
    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.CompareTag("hollow"))
        {
            Debug.Log("te caes al vaciooooooooo");
            speed = 0.0f;
            canJump = false;
            isAlive=false;
            canTurn = false;
            _anim.SetTrigger("Burned");
            fallingSound.Play();


            GameObject Enemy = GameObject.FindGameObjectWithTag("Enemy");
            Enemy.GetComponent<EnemyMovement>().DemonBehaviour();

            GameObject Time = GameObject.FindGameObjectWithTag("Time");
            Time.GetComponent<TimeBehaviour>().StopTime();
            StartCoroutine(ShowDieMenu());
        }
    }
    IEnumerator SlowVelocity()
    {
        speed = 1.5f;
        _anim.SetTrigger("Run");
        yield return new WaitForSeconds(3f);
        speed = 2f;
    }
    public void ActivateSpeedBar()
    {

            StartCoroutine(IncreaseVelocity());
        

    }

    IEnumerator IncreaseVelocity()
    {
        speed = 2.5f;
        boostSound.Play();
        yield return new WaitForSeconds(3f);
        speed = 2f; 
    }
    public void Attack()
    {

        if (isAlive)
        {
            _anim.SetTrigger("ThrowBomb");
            StartCoroutine(BombAttack());
        }

    }

    IEnumerator BombAttack()
    {
        yield return new WaitForEndOfFrame();
        _anim.SetTrigger("Run"); 
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public void Victory()
    {
        speed= 0.0f;
        _anim.SetTrigger("Celebrate");
        victorySound.Play();
        isAlive = false;
        canJump= false;
        canTurn= false;
    }
}
