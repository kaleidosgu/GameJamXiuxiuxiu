using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCarMgr : MonoBehaviour
{
    public GameObject PrefabCar;

    public SpawnPosition[] ArraySpawnPos;

    public bool DebugTest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DebugTest == true)
        {
            DebugTest = false;
            int nRandom = Random.Range(0, 4);
            SpawnPosition _pos = ArraySpawnPos[nRandom];
            GameObject _objCar = Instantiate(PrefabCar, _pos.transform.position, Quaternion.identity);
            CarMovement _carMovement = _objCar.GetComponent<CarMovement>();
            _carMovement.SetStartData(_pos.MoveDir);
        }
    }
}
