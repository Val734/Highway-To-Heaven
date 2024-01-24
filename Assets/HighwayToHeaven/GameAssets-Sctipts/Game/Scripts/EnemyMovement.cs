 using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    //Establezco la velocidad del movimiento del enemigo así como la dirección del giro, true sería a la derecha y false a la izquierda
    //También necesitaré la la fuerza aplicada al enemigo y la fuerza de salto 
    //TAmbién he agregado los componentes necesarios del enemigo
    public float speed;
    private bool turnDirection = false;
    [SerializeField] Vector3 force;
    [SerializeField] float jumpForce = 200f;
    [SerializeField] GameObject soundManager;
    [SerializeField] ParticleSystem ExplosionEffect;

    private AudioSource laughSound;
    private AudioSource hurtSound;
    private AudioSource explosionSound;

    private Animator _anim;


    Rigidbody rb;

    private void Awake()
    {
        //Aquí obtengo los componentes  y sonido
        rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        laughSound = soundManager.transform.Find("Demon_Laugh").GetComponent<AudioSource>();
        hurtSound = soundManager.transform.Find("Demon_Hurt").GetComponent<AudioSource>();
        explosionSound = soundManager.transform.Find("ExplosionSound").GetComponent<AudioSource>();





    }
    private void Update()
    {
        // Con esto simplemente hago que se mueva al enemigo hacia adelante

        Vector3 newVelocity;
        newVelocity = transform.forward * speed;
        newVelocity.y = rb.velocity.y;
        rb.velocity = newVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si la colisión es con un objeto etiquetado como "Flip"

        if (other.gameObject.CompareTag("Flip"))
        {
            Debug.Log("Enemigo giraaa");
            //En el caso de colisionar con un bloque que sea "Flip" el peersonaje gira en la dirección  correcta y ajusto la posición para que siempre esté en el centro del bloque

            if (!turnDirection)
                {
                    transform.Rotate(0.0f, 90.0f, 0.0f);
                    turnDirection = true;
                    transform.position=new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z)); 
                }
                else if (turnDirection)
                {
                    transform.Rotate(0.0f, -90.0f, 0.0f);
                    turnDirection = false;
                    transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
            } 
        }
        //Si está en un bloque de salto aplica la fuerza impuesta hacía arriba
        else if (other.gameObject.CompareTag("Jump"))
        {
            Debug.Log("Enemigo saltaaaa");
            rb.AddForce(Vector3.up * jumpForce);
            _anim.SetTrigger("jump");

        }

    }
    //Cuando se llama a este método desde otros Scripts se detiene el movimiento del personaje con su animación y sonido
    public void DemonBehaviour()
    {
        speed = 0.0f;
        _anim.SetTrigger("celebrate");
        laughSound.Play(); 
        Debug.Log(" c murio"); 
    }
    public void Hurt()
    {
        _anim.SetTrigger("hurt");
        StartCoroutine(BombDamage());
        if (ExplosionEffect != null)
        {
            ExplosionEffect.gameObject.SetActive(true);
            ExplosionEffect.Play();
            hurtSound.Play();
            explosionSound.Play();
        }
    }

    IEnumerator BombDamage()
    {
        yield return new WaitForSeconds(0.5f);
        _anim.SetTrigger("run");
        ExplosionEffect.gameObject.SetActive(false);

    }
    public void Die()
    {
        speed= 0.0f;
        _anim.SetTrigger("death");
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerBehaviour>().Victory();
    }
}
