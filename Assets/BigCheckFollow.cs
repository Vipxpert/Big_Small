using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCheckFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform smallCube;
    public float x, y;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(smallCube.position.x+x, smallCube.position.y+y);
    }
}
