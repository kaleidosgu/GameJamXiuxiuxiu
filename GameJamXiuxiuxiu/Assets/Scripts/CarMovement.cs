using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CarDebugInfo
{
    public Color CrSafeDistance;
    public Color CrSlowDownDistance;
    public Color CrStopDistance;
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

    public LayerMask LayerMsk;
}
[System.Serializable]
public class CarSpeedInfo
{
    public float NormalSpeed;

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
    private GlobalDefine.CarMovementDir m_dir;
    // Start is called before the first frame update
    void Start()
    {
        m_fCurSpeed = CarSpeedInf.NormalSpeed;
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
    private void OnDrawGizmos()
    {
        float fLineDis = DrawLineDistance;
        CarCheckInfo.CheckResult _res = _checkResult();
        RaycastHit[] _rayCastLst = Physics.RaycastAll(transform.position, transform.right, CheckInfo.MaxCheckDistance, CheckInfo.LayerMsk);
        if(_rayCastLst.Length > 0)
        {
            fLineDis = _rayCastLst[0].distance;
            if(_res == CarCheckInfo.CheckResult.CheckResult_Safe)
            {
                Gizmos.color = CarDebug.CrSafeDistance;
            }
            else if(_res == CarCheckInfo.CheckResult.CheckResult_Slow)
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
        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(transform.right) * fLineDis);
    }

    public CarCheckInfo.CheckResult _checkResult()
    {
        CarCheckInfo.CheckResult _res = CarCheckInfo.CheckResult.CheckResult_None;
        float fLineDis = 0.0f;
        RaycastHit[] _rayCastLst = Physics.RaycastAll(transform.position, transform.right, CheckInfo.MaxCheckDistance, CheckInfo.LayerMsk);
        if (_rayCastLst.Length > 0)
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
}
