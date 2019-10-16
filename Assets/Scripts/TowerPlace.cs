using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlace : MonoBehaviour
{
    private Vector3 TowerPositon;
    // Start is called before the first frame update
    void Start()
    {
        TowerPositon = transform.position;
        TowerPositon.y = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetTowerPosition()
    {
        return TowerPositon;
    }
}
