using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Wire : MonoBehaviour
{
    [SerializeField]
    private Transform pointOne;
    [SerializeField]
    private Transform pointTwo;

    public float ropeLength;
    public AnimationCurve curve;
    public float timeElapsed;
    
    private Vector3 distance;
    private Vector3 horizontalDistance;

    private LineRenderer _lineRenderer;
    
    
    void Start()
    {
        _lineRenderer = this.gameObject.GetComponent<LineRenderer>();

        UpdateLine();
    }

    // Update is called once per frame
    void Update()
    {

        
        UpdateLine();



    }


    private void UpdateLine()
    {
        _lineRenderer.SetPosition(0, pointOne.position);
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, pointTwo.position);
        
        distance = pointOne.position - pointTwo.position;
        horizontalDistance = new Vector3(distance.x, 0, distance.z);
        _lineRenderer.positionCount =  ((int)distance.magnitude + 1) * 30;

        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            _lineRenderer.SetPosition(i, new Vector3((pointOne.position.x + (-distance.x / _lineRenderer.positionCount) * i), 
                                                (pointOne.position.y +(-distance.y / _lineRenderer.positionCount) * i - ((curve.Evaluate(Time.time) * ropeLength) * Mathf.Sin(horizontalDistance.magnitude * ((Mathf.PI / horizontalDistance.magnitude) / _lineRenderer.positionCount) * i))) , 
                                                    (pointOne.position.z + (-distance.z / _lineRenderer.positionCount) * i)));
        }
    }
}
