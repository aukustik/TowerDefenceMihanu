using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    private float _delay = 1.0f;
    private Quaternion quat = new Quaternion();
    private Vector3 _startPosEnemy = new Vector3(-36.0f, 1.5f, 0);

    public float nextActionTime = 0.0f;

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime) {
            nextActionTime += _delay;
            Instantiate(Enemy, _startPosEnemy, quat);
        }
    }
}
