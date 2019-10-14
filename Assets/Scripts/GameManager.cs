using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject BasicTower;
    private float _delay = 1.0f;
    private Quaternion quat = new Quaternion();
    private Vector3 _startPosEnemy = new Vector3(-36.0f, 1.5f, 0);
    GameObject tower;

    public float nextActionTime = 0.0f;

    // Start is called before the first frame update
    private void Awake()
    {
        Messenger<GameObject>.AddListener(GameEvent.TOWER_IN_HANDS, SetTowerInstance);
    }
    private void OnDestroy()
    {
        Messenger<GameObject>.RemoveListener(GameEvent.TOWER_IN_HANDS, SetTowerInstance);
    }
    void Start()
    {
        tower = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += _delay;
            Instantiate(Enemy, _startPosEnemy, quat);
        }
        if (tower != null)
        {
            FollowCursor(tower);
        }
        if (Input.GetMouseButtonDown(0) && tower != null) {
            TowerCreate(BasicTower);
        }
    }
    public void TowerCreate(GameObject tower) {
        Vector3 mousePos = Input.mousePosition;
        Debug.Log(mousePos);
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 towerPosition = new Vector3(hit.point.x, 0.5f, hit.point.z);
            GameObject towInstance = Instantiate(tower, towerPosition, Quaternion.identity);
            towInstance.GetComponentInChildren<TowerBasic>().SetStatus(true);
        }
    }
    private void FollowCursor(GameObject towerInstance) {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.gameObject.GetComponent<Ground>() != null)
        {
            Vector3 towerPosition = new Vector3(hit.point.x, 0.5f, hit.point.z);
            towerInstance.transform.position = towerPosition;
            
        }
    }
    public void SetTowerInstance(GameObject towerInstance) {
        if (towerInstance == null) {
            Destroy(tower);
        }
        else
            tower = towerInstance;
    }
}
