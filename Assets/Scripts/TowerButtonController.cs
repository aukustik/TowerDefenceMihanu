using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonController : MonoBehaviour
{
    [SerializeField] private GameObject BasicTower;
    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && active)
        {
            active = false;
            Messenger<GameObject>.Broadcast(GameEvent.TOWER_IN_HANDS, null);
        }
    }
    public void onClick() {
        if (!active)
        {
            active = true;
            Messenger<GameObject>.Broadcast(GameEvent.TOWER_IN_HANDS, Instantiate(BasicTower));

        }
    }
}
