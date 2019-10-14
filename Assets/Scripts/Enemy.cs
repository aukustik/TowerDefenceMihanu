using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 8.0f;
    private bool _alive;
    private int _health = 15;
    public bool isDead = false;
    // Start is called before the first frame update


    void Start()
    {
        _alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    public void TakeDamage(int value) {
        _health -= value;
        if (_health <= 0) {
            isDead = true;
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Bullet>() != null) {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
