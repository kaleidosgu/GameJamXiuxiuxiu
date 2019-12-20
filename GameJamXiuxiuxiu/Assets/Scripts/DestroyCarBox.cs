using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCarBox : MonoBehaviour
{
	public GlobalDefine.CarMovementDir BelongDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TagCar"))
		{
			CarMovement _carMove = other.gameObject.GetComponent<CarMovement>();
			if( _carMove.m_dir == BelongDir )
			{
				Destroy(other.gameObject);
			}
		}
    }
}
