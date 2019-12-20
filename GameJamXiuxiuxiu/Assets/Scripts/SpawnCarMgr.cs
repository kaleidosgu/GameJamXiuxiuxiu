using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCarMgr : MonoBehaviour
{
    public GameObject PrefabCar;

    public SpawnPosition[] ArraySpawnPos;

    public float SpawnCarTime;

    public ChangeSpeedMgr chgSpeedMgr;

    public bool DebugTest;
    private float m_fCurSpawnCarTime;
    private int m_nCntLst;
    // Start is called before the first frame update
    void Start()
    {
        m_nCntLst = ArraySpawnPos.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (DebugTest == true)
        {
            DebugTest = false;
            _generate();
        }
    }

    private void _generate()
    {
        int nRandom = Random.Range(0, m_nCntLst);
        SpawnPosition _pos = ArraySpawnPos[nRandom];
        GameObject _objCar = Instantiate(PrefabCar, Vector3.zero, Quaternion.identity);
        _objCar.transform.SetParent(_pos.transform);
        _objCar.transform.localPosition = Vector3.zero;
        CarMovement _carMovement = _objCar.GetComponent<CarMovement>();
        chgSpeedMgr.RegisteCar(_carMovement.SetPowerSpeed);
        _carMovement.SetStartData(_pos.MoveDir, _pos.Dir);
    }

    private void FixedUpdate()
    {
        m_fCurSpawnCarTime += Time.fixedDeltaTime;
        if(m_fCurSpawnCarTime >= SpawnCarTime)
        {
            m_fCurSpawnCarTime -= SpawnCarTime;
            _generate();
        }
    }
}
