using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100.0f;
    private Vector3 _path;
    private bool _hasTarget = false;
    GameObject target = null;
    // Start is called before the first frame update
    private void Awake()
    {
    }
    private void OnDestroy()
    {
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            _path = target.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, _path, speed * Time.deltaTime);
        }
        else
            TargedIsDead();
        
    }

    public void TargedIsDead()
    {
        Destroy(gameObject);
    }

    public void SetTarget(GameObject value) {
        target = value;
    }

 }   

