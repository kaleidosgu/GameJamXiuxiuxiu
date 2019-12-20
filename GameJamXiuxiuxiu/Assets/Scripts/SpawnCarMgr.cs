using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCarMgr : MonoBehaviour
{
    public GameObject PrefabCar;

    public SpawnPosition[] ArraySpawnPos;
	
	public SpawnPosition NorthPos;
	public SpawnPosition SouthPos;
	public SpawnPosition EastPos;
	public SpawnPosition WestPos;

    public float SpawnCarTime;

    public ChangeSpeedMgr chgSpeedMgr;

    public bool DebugTest;
	public GlobalDefine.CarMovementDir MoveDir;
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
            //_randomSpawn();
			
			GenerateByDir(MoveDir,PrefabCar);
        }
    }
	public void GenerateByDir(GlobalDefine.CarMovementDir _dir,GameObject _obj)
	{
		if( _dir == GlobalDefine.CarMovementDir.CarMovementDir_North )
		{
			_generate(NorthPos,PrefabCar);
		}
		else if( _dir == GlobalDefine.CarMovementDir.CarMovementDir_South )
		{
			_generate(SouthPos,PrefabCar);
		}
		else if( _dir == GlobalDefine.CarMovementDir.CarMovementDir_East )
		{
			_generate(EastPos,PrefabCar);
		}
		else if( _dir == GlobalDefine.CarMovementDir.CarMovementDir_West )
		{
			_generate(WestPos,PrefabCar);
		}
	}
		
	private void _randomSpawn()
	{
        int nRandom = Random.Range(0, m_nCntLst);
        SpawnPosition _pos = ArraySpawnPos[nRandom];
		_generate(_pos,PrefabCar);
	}
    private void _generate(SpawnPosition _pos,GameObject _objPrefab)
    {
        GameObject _objCar = Instantiate(_objPrefab, Vector3.zero, Quaternion.identity);
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
            _randomSpawn();
        }
    }
}
