using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float NormalSpeed;

    public float PowerSpeed;

    public float TimeOfPowerSpeed;

    private float m_fCurTime;

    private bool m_bPower;

    private float m_fCurSpeed;
    private GlobalDefine.CarMovementDir m_dir;
    // Start is called before the first frame update
    void Start()
    {
        m_fCurSpeed = NormalSpeed;
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
            m_fCurSpeed = PowerSpeed;
            m_fCurTime = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        if(m_bPower == true)
        {
            m_fCurTime += Time.fixedDeltaTime;
            if( m_fCurTime >= TimeOfPowerSpeed )
            {
                m_bPower = false;
                m_fCurSpeed = NormalSpeed;
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
