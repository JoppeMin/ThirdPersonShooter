using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class mat : MonoBehaviour
{
    [Range(0,1)]
    public float transamount = 0;
    [SerializeField]
    private Renderer thisMat;
    // Start is called before the first frame update
    private void Start()
    {
        thisMat = this.gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        thisMat.material.SetFloat("Transitionamount", transamount);
    }
}
