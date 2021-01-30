using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraHelper : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> allCameras;
    [SerializeField] private CinemachineVirtualCamera centerCamera;
    [SerializeField] private CinemachineVirtualCamera leftCamera;
    [SerializeField] private CinemachineVirtualCamera rightCamera;
    [SerializeField][Range(0,1)] private float camSwitchTreshold = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xDir = Input.GetAxis("Horizontal");
        if(Mathf.Abs(xDir)> camSwitchTreshold)
        {
            if (xDir > 0)
            {

                SetActiveCamera(leftCamera);
            } 
            else
            {
                SetActiveCamera(rightCamera);
            }
        }
        else
        {
            SetActiveCamera(centerCamera);
        }
    }  
    private void SetActiveCamera(CinemachineVirtualCamera activeCam)
    {
        foreach(CinemachineVirtualCamera camera in allCameras)
        {
            camera.Priority = 10;
        }
        activeCam.Priority = 100;
    }
}
