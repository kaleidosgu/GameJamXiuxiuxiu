using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CarDebugInfo
{
    public Color CrSafeDistance;
    public Color CrSlowDownDistance;
    public Color CrStopDistance;

    public bool DebugInfo;
}
[System.Serializable]
public class CarCheckInfo
{
    public enum CheckResult
    {
        CheckResult_None = 0,
        CheckResult_Safe,
        CheckResult_Slow,
        CheckResult_Stop
    }
    public float MaxCheckDistance;

    public float DistOfSlowDown;
    public float DistOfStop;
	public float DiffHeight;
    public LayerMask LayerMsk;
}
[System.Serializable]
public class CarSpeedInfo
{
    public float NormalSpeed;
    public float SlowSpeed;
	public float StopSpeed;
    public float PowerSpeed;

}
public class CarMovement : MonoBehaviour
{
    public float TimeOfPowerSpeed;
    public float DrawLineDistance;


    [Space]
    [Space]
    [Space]
    public CarSpeedInfo CarSpeedInf;
    public CarDebugInfo CarDebug;

    [Space]
    public CarCheckInfo CheckInfo;

    private float m_fCurTime;
    private bool m_bPower;
    private float m_fCurSpeed;
    public GlobalDefine.CarMovementDir m_dir;
    private Collider m_collider;
    private float m_fColliderHeight;
    private ChangeSpeedMgr m_mgr;
    // Start is called before the first frame update
    void Start()
    {
        m_fCurSpeed = CarSpeedInf.NormalSpeed;
    }
    private void Awake()
    {
        m_collider = GetComponent<Collider>();
        m_fColliderHeight = m_collider.bounds.size.z / 2 - CheckInfo.DiffHeight;
    }
    private void OnEnable()
    {
        m_collider = GetComponent<Collider>();
    }
    public void SetStartData(Vector3 vecPos,GlobalDefine.CarMovementDir _dir)
    {
        m_dir = _dir;

        if( _dir == GlobalDefine.CarMovementDir.CarMovementDir_East )
        {
            transform.Rotate(0, 180, 0);
        }
        else if (_dir == GlobalDefine.CarMovementDir.CarMovementDir_North)
        {
            transform.Rotate(0, 90, 0);
        }
        else if (_dir == GlobalDefine.CarMovementDir.CarMovementDir_South)
        {
            transform.Rotate(0, 270,0);
        }
        else if (_dir == GlobalDefine.CarMovementDir.CarMovementDir_West)
        {
            transform.Rotate(0, 0, 0);
        }
    }
    public void SetPowerSpeed( GlobalDefine.CarMovementDir _carDir)
    {
        bool bProcess = false;
        if(m_dir == _carDir)
        {
            bProcess = true;
        }
        else
        {
            if ((m_dir == GlobalDefine.CarMovementDir.CarMovementDir_South || m_dir == GlobalDefine.CarMovementDir.CarMovementDir_North)
                && (_carDir == GlobalDefine.CarMovementDir.CarMovementDir_South || _carDir == GlobalDefine.CarMovementDir.CarMovementDir_North))
            {
                bProcess = true;
            }
            else if ((m_dir == GlobalDefine.CarMovementDir.CarMovementDir_East || m_dir == GlobalDefine.CarMovementDir.CarMovementDir_West)
                && (_carDir == GlobalDefine.CarMovementDir.CarMovementDir_East || _carDir == GlobalDefine.CarMovementDir.CarMovementDir_West))
            {
                bProcess = true;
            }
        }
        if( bProcess == true )
        {
            m_bPower = true;
            m_fCurSpeed = CarSpeedInf.PowerSpeed;
            m_fCurTime = 0.0f;
        }
    }

    private void _drawLine(bool bLeft)
    {
        float fLineDis = DrawLineDistance;
        CarCheckInfo.CheckResult _res;

        Vector3 vecPosFrom;
        if(bLeft == true)
        {
            vecPosFrom = transform.TransformPoint(Vector3.forward * m_fColliderHeight);
        }
        else
        {
            vecPosFrom = transform.TransformPoint(Vector3.forward * -m_fColliderHeight);
        }

        _res = _checkLineResult(vecPosFrom);
        RaycastHit[] _rayCastLst = Physics.RaycastAll(vecPosFrom, transform.right, CheckInfo.MaxCheckDistance, CheckInfo.LayerMsk);
        if (_rayCastLst.Length > 0)
        {
            fLineDis = _rayCastLst[0].distance;
            if (_res == CarCheckInfo.CheckResult.CheckResult_Safe)
            {
                Gizmos.color = CarDebug.CrSafeDistance;
            }
            else if (_res == CarCheckInfo.CheckResult.CheckResult_Slow)
            {
                Gizmos.color = CarDebug.CrSlowDownDistance;
            }
            else
            {
                Gizmos.color = CarDebug.CrStopDistance;
            }
        }
        else
        {
            Gizmos.color = CarDebug.CrSafeDistance;
        }
        Gizmos.DrawLine(vecPosFrom, vecPosFrom + transform.right * fLineDis);
    }
    private void OnDrawGizmos()
    {
        if( CarDebug.DebugInfo == true )
        {
            _drawLine(true);
            _drawLine(false);
        }
    }

    public CarCheckInfo.CheckResult _checkLineResult(Vector3 vecPosFrom)
    {
        CarCheckInfo.CheckResult _res = CarCheckInfo.CheckResult.CheckResult_None;
        float fLineDis = 0.0f;
        RaycastHit[] _rayCastLst;
        _rayCastLst = Physics.RaycastAll(vecPosFrom, transform.right, CheckInfo.MaxCheckDistance, CheckInfo.LayerMsk);
        if (_rayCastLst.Length >  0)
        {
            fLineDis = _rayCastLst[0].distance;
            if (fLineDis > CheckInfo.DistOfSlowDown)
            {
                _res = CarCheckInfo.CheckResult.CheckResult_Safe;
            }
            else if (fLineDis > CheckInfo.DistOfStop)
            {
                _res = CarCheckInfo.CheckResult.CheckResult_Slow;
            }
            else
            {
                _res = CarCheckInfo.CheckResult.CheckResult_Stop;
            }
        }
        else
        {
            _res = CarCheckInfo.CheckResult.CheckResult_Safe;
        }
        if( _res == CarCheckInfo.CheckResult.CheckResult_None)
        {
            Debug.Assert(false);
        }
        return _res;
    }
    private void FixedUpdate()
    {
        if(m_bPower == true)
        {
            m_fCurTime += Time.fixedDeltaTime;
            if( m_fCurTime >= TimeOfPowerSpeed )
            {
                m_bPower = false;
                m_fCurSpeed = CarSpeedInf.NormalSpeed;
            }
        }
        else
        {
            Vector3 vecPosLeft = transform.TransformPoint(Vector3.forward * m_fColliderHeight);
            Vector3 vecPosRight = transform.TransformPoint(Vector3.forward * -m_fColliderHeight);

            CarCheckInfo.CheckResult _resLeft = _checkLineResult(vecPosLeft);
            CarCheckInfo.CheckResult _resRight = _checkLineResult(vecPosRight);

            CarCheckInfo.CheckResult _res = _resLeft > _resRight ? _resLeft : _resRight;
            if (_res == CarCheckInfo.CheckResult.CheckResult_Safe)
            {
                m_fCurSpeed = CarSpeedInf.NormalSpeed;
            }
            else if (_res == CarCheckInfo.CheckResult.CheckResult_Slow)
            {
                m_fCurSpeed = CarSpeedInf.SlowSpeed;
            }
            else if (_res == CarCheckInfo.CheckResult.CheckResult_Stop)
            {
                m_fCurSpeed = CarSpeedInf.StopSpeed;
            }
        }
        if(m_dir == GlobalDefine.CarMovementDir.CarMovementDir_West)
        {
            transform.Translate(transform.right * Time.fixedDeltaTime * m_fCurSpeed);
        }
        else if (m_dir == GlobalDefine.CarMovementDir.CarMovementDir_East)
        {
            transform.Translate(-transform.right * Time.fixedDeltaTime * m_fCurSpeed);
        }
        else if (m_dir == GlobalDefine.CarMovementDir.CarMovementDir_North)
        {
            transform.Translate(transform.forward * Time.fixedDeltaTime * m_fCurSpeed);
        }
        else if (m_dir == GlobalDefine.CarMovementDir.CarMovementDir_South)
        {
            transform.Translate(-transform.forward * Time.fixedDeltaTime * m_fCurSpeed);
        }
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("TagCar"))
		{
			Debug.Log("Crash");
            CrashEvent _evt = FindObjectOfType<CrashEvent>();
            if(_evt != null)
            {
                _evt.CrashHappened();
            }
		}
	}

    public void Registe(ChangeSpeedMgr _mgr)
    {
        _mgr.RegisteCar(SetPowerSpeed);
        m_mgr = _mgr;
    }

    private void OnDestroy()
    {
        if(m_mgr != null)
        {
            m_mgr.UnregisteCar(SetPowerSpeed);
        }
    }
}
