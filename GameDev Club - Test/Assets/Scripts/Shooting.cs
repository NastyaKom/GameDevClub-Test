using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    private float bulletPower = 1000f;

    [SerializeField] TextMeshProUGUI textAmount;
    [HideInInspector] public int bulletAmount;
    [HideInInspector] public int maxBulletAmount = 15;

    public List<GameObject> zombees;
    float distanceZombee;
    float maxDistanceZombee = 4f;
    public float minDistanceZombee=100;
    [SerializeField] int minDistanceID;

    public static Shooting instance;

    private void Awake()
    {
        if(instance == null)
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
        bulletAmount = maxBulletAmount;
        GameObject[] zombeesArray = GameObject.FindGameObjectsWithTag("Zombee");
        for(int i =0; i < zombeesArray.Length; i++)
        {
            zombees.Add(zombeesArray[i]);
        }

    }

    public void FindMinDistanceZombee()
    {
        for (int i = 0; i <= zombees.Count-1; i++)
        {
            distanceZombee = Vector2.Distance(transform.position, zombees[i].transform.position);
            if (distanceZombee < minDistanceZombee)
            {
                minDistanceZombee = distanceZombee;
                minDistanceID = i;
            }
        }
    }

    public void ShowAmountText()
    {
        textAmount.text = bulletAmount + "/15";
    }

    public void Shoot()
    {
        FindMinDistanceZombee();
        if (bulletAmount >0 )
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

            if ((minDistanceZombee <= maxDistanceZombee) && (transform.rotation.y != zombees[minDistanceID].transform.rotation.y))
            {
                float angle = (zombees[minDistanceID].transform.position - transform.position).y / Mathf.Sqrt(Mathf.Pow((zombees[minDistanceID].transform.position - transform.position).x, 2) + Mathf.Pow((zombees[minDistanceID].transform.position - transform.position).y, 2));
                bullet.transform.Rotate(0, 0, angle * Mathf.Rad2Deg);
                rbBullet.AddForce((zombees[minDistanceID].transform.position - transform.position) * bulletPower);
            }
            else
            {
                rbBullet.AddForce(transform.right * bulletPower);
            }
            Destroy(bullet, 3f);
        }

        if (bulletAmount < 1) bulletAmount = 0; else bulletAmount--;
        ShowAmountText();
    }
}
