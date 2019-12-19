using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float NormalSpeed;

    private Vector3 m_vecDir;
    private GlobalDefine.CarMovementDir m_dir;
    // Start is called before the first frame update
    void Start()
    {
        //m_vecDir = Vector3.left;
    }
    public void SetStartData(Vector3 vecPos,GlobalDefine.CarMovementDir _dir)
    {
        m_dir = _dir;
        m_vecDir = vecPos;

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

    private void FixedUpdate()
    {
        if(m_dir == GlobalDefine.CarMovementDir.CarMovementDir_West)
        {
            transform.Translate(transform.right * Time.fixedDeltaTime * NormalSpeed);
        }
        else if (m_dir == GlobalDefine.CarMovementDir.CarMovementDir_East)
        {
            transform.Translate(-transform.right * Time.fixedDeltaTime * NormalSpeed);
        }
        else if (m_dir == GlobalDefine.CarMovementDir.CarMovementDir_North)
        {
            transform.Translate(transform.forward * Time.fixedDeltaTime * NormalSpeed);
        }
        else if (m_dir == GlobalDefine.CarMovementDir.CarMovementDir_South)
        {
            transform.Translate(-transform.forward * Time.fixedDeltaTime * NormalSpeed);
        }
    }
}
