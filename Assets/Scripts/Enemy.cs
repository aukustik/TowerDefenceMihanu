using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 8.0f;
    private bool _alive;
    private int _health = 15;
    public bool isDead = false;
    private float slowTime = 0f;
    private float _slowFactor = 0f; // in percents
    public float SlowFactor { get { return 1 - _slowFactor / 100; } private set { _slowFactor = value; } }
    // Start is called before the first frame update


    void Start()
    {
        _alive = true;
    }

    public void ApplySlow(float time, float factor)
    {
        slowTime = time;
        SlowFactor = factor;
    }


    // Update is called once per frame
    void Update()
    {
        var calculatedSpeed = speed * SlowFactor;
        transform.Translate(calculatedSpeed * Time.deltaTime, 0, 0);
        slowTime -= Time.deltaTime;
        if (slowTime <= 0 )
        {
            SlowFactor = 0;
        }
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
        if (other.gameObject.GetComponent<Slow>() is Slow slow)
        {
            ApplySlow(slow.time, slow.factor);
        }
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
