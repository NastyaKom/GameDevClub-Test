using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private float distance;
    private float minDistance = 12f;
    private Vector2 enemyPos;
    private Animator enemyAnim;
    private float speed;
    private float maxSpeed = 0.01f;

    [SerializeField] private Image hpBar;
    [SerializeField] private GameObject[] bonus; 
    

    // Start is called before the first frame update
    void Start()
    {
        EnemyStart();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAnim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnemyStop();
            enemyAnim.Play("Zombee Attack");

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hpBar.fillAmount -= 1f/3f;
            if(hpBar.fillAmount == 0)
            {
                enemyAnim.Play("Zombee Die");
                speed = 0;
                Shooting.instance.minDistanceZombee = 100;
                Instantiate(bonus[Random.Range(0, bonus.Length)], transform.position, Quaternion.identity);
                Destroy(GetComponent<BoxCollider2D>());
                Destroy(transform.parent.gameObject, 2f);
                Shooting.instance.zombees.Remove(gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnemyStart();
        }
    }


    private void OnEnable()
    {
        UIController.OnPaused += EnemyStop;
        UIController.OnPlay += EnemyStart;
    }

    private void OnDisable()
    {
        UIController.OnPaused -= EnemyStop;
        UIController.OnPlay -= EnemyStart;
    }

    private void EnemyStop()
    {
        speed = 0;
    }

    private void EnemyStart()
    {
        speed = maxSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= minDistance)
        {
            if (speed != 0)
            {
                enemyAnim.Play("Zombee Walk");
            }
            enemyPos = transform.position;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
            if(enemyPos.x - transform.position.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (enemyPos.x - transform.position.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            enemyAnim.Play("Zombee");
        }
    }
}
