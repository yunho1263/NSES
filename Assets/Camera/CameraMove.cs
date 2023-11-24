using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform mapSizeRU;
    public Transform mapSizeLD;

    public Camera mainCamera;

    void Start()
    {
        
    }

    void Update()
    {
        // wasd로 카메라 이동
        if (Input.GetKey(KeyCode.W))
        {
            mainCamera.transform.Translate(Vector3.up * Time.deltaTime * 10);
        }
        if (Input.GetKey(KeyCode.A))
        {
            mainCamera.transform.Translate(Vector3.left * Time.deltaTime * 10);
        }
        if (Input.GetKey(KeyCode.S))
        {
            mainCamera.transform.Translate(Vector3.down * Time.deltaTime * 10);
        }
        if (Input.GetKey(KeyCode.D))
        {
            mainCamera.transform.Translate(Vector3.right * Time.deltaTime * 10);
        }

        // 마우스 휠로 카메라 줌
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            mainCamera.orthographicSize -= 1;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            mainCamera.orthographicSize += 1;
        }

        // 카메라가 맵 밖으로 나가지 않도록
        if (mainCamera.orthographicSize > 20)
        {
            mainCamera.orthographicSize = 20;
        }
        if (mainCamera.orthographicSize < 5)
        {
            mainCamera.orthographicSize = 5;
        }

        //카메라의 오른쪽 위 꼭지점이 맵의 오른쪽 위 꼭지점보다 크면
        if (mainCamera.transform.position.x + mainCamera.orthographicSize > mapSizeRU.position.x)
        {
            mainCamera.transform.position = new Vector3(mapSizeRU.position.x - mainCamera.orthographicSize, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
        if (mainCamera.transform.position.y + mainCamera.orthographicSize > mapSizeRU.position.y)
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mapSizeRU.position.y - mainCamera.orthographicSize, mainCamera.transform.position.z);
        }

        //카메라의 왼쪽 아래 꼭지점이 맵의 왼쪽 아래 꼭지점보다 작으면
        if (mainCamera.transform.position.x - mainCamera.orthographicSize < mapSizeLD.position.x)
        {
            mainCamera.transform.position = new Vector3(mapSizeLD.position.x + mainCamera.orthographicSize, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
        if (mainCamera.transform.position.y - mainCamera.orthographicSize < mapSizeLD.position.y)
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mapSizeLD.position.y + mainCamera.orthographicSize, mainCamera.transform.position.z);
        }
    }
}
