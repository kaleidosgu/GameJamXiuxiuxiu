using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float NormalSpeed;

    private Vector3 m_vecDir;
    // Start is called before the first frame update
    void Start()
    {
        //m_vecDir = Vector3.left;
    }
    public void SetStartData(Vector3 vecPos)
    {
        m_vecDir = vecPos;
    }

    private void FixedUpdate()
    {
        transform.Translate(m_vecDir * Time.fixedDeltaTime * NormalSpeed);
    }
}
