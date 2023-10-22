using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombee : MonoBehaviour
{
    [SerializeField] private GameObject zombeePrefab;
    private int countZombee = 3;
    float leftLimit;
    float rightLimit;
    float upperLimit;
    float lowerLimit;

    // Start is called before the first frame update
    void Awake()
    {
        leftLimit = CameraMovement.instance.leftLimit;
        rightLimit = CameraMovement.instance.rightLimit;
        upperLimit = CameraMovement.instance.upperLimit;
        lowerLimit = CameraMovement.instance.lowerLimit;
        for (int i=0; i < countZombee; i++)
        {
            Instantiate(zombeePrefab, new Vector3(Random.Range(leftLimit, rightLimit), Random.Range(upperLimit, lowerLimit), transform.position.z), Quaternion.identity);
        }
    }
}
