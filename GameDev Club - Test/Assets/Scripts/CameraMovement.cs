using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float dumping = 1.5f;
    public Vector2 offset = new Vector2(2f, 1f);
    public bool isLeft;
    [SerializeField] private Transform player;
    Animator playerAnim; 
    private int lastX;

    int currentX;
    Vector3 target;
    Vector3 currentPosition;

    public float leftLimit;
    public float rightLimit;
    public float upperLimit;
    public float lowerLimit;

    public static CameraMovement instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        playerAnim = player.gameObject.GetComponent<Animator>(); 
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer(isLeft); 
    }

    public void FindPlayer(bool playerIsLeft)
    {
        lastX = Mathf.RoundToInt(player.position.x);
        if (playerIsLeft)
        {
            player.rotation = Quaternion.Euler(0, 180, 0);
            transform.position = new Vector3(player.position.x - offset.x, player.position.y - offset.y, transform.position.z);
        }
        else
        {
            player.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z); 
        }
    }

    private void Update()
    {
        if (player)
        {
            currentX = Mathf.RoundToInt(player.position.x); 
            if(currentX > lastX)
            {
                isLeft = false;
            }
            else
            {
                if(currentX < lastX)
                {
                    isLeft = true;
                }
            }
            lastX = Mathf.RoundToInt(player.position.x);

            //if ((player.transform.position.x > -1f) && (player.transform.position.x < 8f) && (player.transform.position.y < 4.5f) && (player.transform.position.y > -13.5f))
            //{
                if (isLeft)
                {
                    target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
                }
                else
                {
                    target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
                }
            //}
            

            currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;

        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit), Mathf.Clamp(transform.position.y, lowerLimit, upperLimit), transform.position.z);
    }
}
