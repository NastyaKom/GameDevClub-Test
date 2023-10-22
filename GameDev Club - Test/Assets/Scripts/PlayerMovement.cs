using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerMovement : MonoBehaviour
{
    public JoystickMovement movementJoystick;
    public float playerSpeed;
    private Rigidbody2D rb;
    private Animator playerAnim;

    [SerializeField] private Image hpBar;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombee"))
        {
            hpBar.fillAmount -= 1f / 3f;
            if(hpBar.fillAmount == 0)
            {
                UIController.instance.diedAudio.Play();
                UIController.instance.isFade = true;
                // нопка повторить
            }
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullets"))
        {
            Shooting.instance.bulletAmount = Shooting.instance.maxBulletAmount;
            Shooting.instance.ShowAmountText();
            Destroy(collision.gameObject);
        }
        else
        {
            List<Item> itemList = InventoryManager.instance.items; 
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].itemName == collision.gameObject.tag && collision.gameObject.layer == 3)
                {
                    InventoryManager.instance.Create(itemList[i]);
                    Destroy(collision.gameObject);
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movementJoystick.joystickVector.y != 0)
        {
            playerAnim.Play("Player Walk");
            rb.velocity = new Vector2(movementJoystick.joystickVector.x * playerSpeed, movementJoystick.joystickVector.y * playerSpeed);
            if(movementJoystick.joystickVector.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            playerAnim.Play("Player Idle");
            rb.velocity = Vector2.zero;
        }
    }
}
