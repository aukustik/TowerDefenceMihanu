using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBasic : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private float _nextActionTime = 0.0f;
    private GameObject _target = null;
    private Bullet _bulletController;
    private bool active = false;
    public float period = 0.1f;


    private void Awake()
    {
    }
    private void OnDestroy()
    {
    }
    void Start()
    {
        _bulletController = bullet.GetComponent<Bullet>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _nextActionTime && active)
        {
            _nextActionTime += period;
            if (_target != null)
            {
                GameObject kek = Instantiate(bullet, transform.position, transform.rotation);
                kek.GetComponent<Bullet>().SetTarget(_target);
            }
            else {

                Collider[] collisions = Physics.OverlapSphere(transform.position, 20.0f);

                foreach (Collider i in collisions)
                {
                    if (i.GetComponent<Enemy>() != null)
                    {
                        _target = i.gameObject;
                    }
                }
            }
        }

    }
    public void SetStatus(bool status) {
        active = status;
    }
    
    private void ChangeTarget(GameObject trgt) {

        if (_target == trgt)
        {

            Collider[] collisions = Physics.OverlapSphere(transform.position, 20.0f);

            foreach (Collider i in collisions)
            {
                if (i.GetComponent<Enemy>() != null)
                {
                    _target = i.gameObject;
                }
            }
        }
    }



}
