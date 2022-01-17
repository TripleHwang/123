using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_bar : MonoBehaviour
{
    public GameObject followingObject;
    // Start is called before the first frame update
    void Start()
    {  
        
    }

    // Update is called once per frame
    void Update()
    {
        //간단히 HP바가 몬스터를 따라다니게 함
        transform.position = followingObject.transform.position + new Vector3(0f, 0.2f, 0f);
    }
}
