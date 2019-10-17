using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject TowerPlace;
    [SerializeField] private GameObject EnemyPlace;
    private float _delay = 1.0f;
    private Quaternion quat = new Quaternion();
    private Vector3 _startPosEnemy = new Vector3(-36.0f, 1.5f, 0);
    private Vector3 tempTowerPosition;
    GameObject tower;
    private List<Vector3> occupiedPositions = new List<Vector3>();

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
        LevelOneCreate();
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
            TowerCreate(tower);
        }
    }
    public void TowerCreate(GameObject tower) {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            
            Vector3 towerPosition = tempTowerPosition;
            if (occupiedPositions.Contains(towerPosition))
            {
                Debug.Log("OCCUPIED");
                return;
            }
            occupiedPositions.Add(towerPosition);
            GameObject towInstance = Instantiate(tower, towerPosition, Quaternion.identity);
            towInstance.GetComponentInChildren<TowerBasic>().SetStatus(true);
        }
    }
    private void FollowCursor(GameObject towerInstance) {
        towerInstance.transform.position = new Vector3(0, -5, 0);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.gameObject.GetComponent<TowerPlace>() != null)
        {
            tempTowerPosition = hit.transform.gameObject.GetComponent<TowerPlace>().GetTowerPosition();
            towerInstance.transform.position = tempTowerPosition;
            
        }
    }
    public void SetTowerInstance(GameObject towerInstance) {
        if (towerInstance == null) {
            Destroy(tower);
        }
        else
            tower = towerInstance;
    }

    private void LevelOneCreate()
    {
        List<Vector3> TowerPlaces = new List<Vector3>();
        List<Vector3> EnemyPlaces = new List<Vector3>();

        for (int i = -10; i <= 10; i++)
        {
            TowerPlaces.Add(new Vector3(i * 2, 0, -6));
            TowerPlaces.Add(new Vector3(i * 2, 0, -4));
            TowerPlaces.Add(new Vector3(i * 2, 0, 4));
            TowerPlaces.Add(new Vector3(i * 2, 0, 6));
        }

        foreach (Vector3 position in TowerPlaces) {
            Instantiate(TowerPlace, position, Quaternion.identity);
        }

        for (int i = -10; i <= 10; i++)
        {
            EnemyPlaces.Add(new Vector3(i * 2, 0, 0));
            EnemyPlaces.Add(new Vector3(i * 2, 0, 2));
            EnemyPlaces.Add(new Vector3(i * 2, 0, -2));
        }

        foreach (Vector3 position in EnemyPlaces)
        {
            Instantiate(EnemyPlace, position, Quaternion.identity);
        }
    }
}
