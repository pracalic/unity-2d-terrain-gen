using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMove : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float camerMoveSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(null);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), Time.deltaTime*camerMoveSpeed) ;
    }
}
