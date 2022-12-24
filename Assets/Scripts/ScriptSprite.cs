using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSprite : MonoBehaviour
{

    [SerializeField] Camera maCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        this.transform.LookAt(transform.position + maCamera.transform.forward);
    }
}
