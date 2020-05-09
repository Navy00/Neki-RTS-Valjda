using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject selectedObject;//objekat u igri
    private ObjectInfo selectedInfo;

    public float panSpeed;
    public float rotateSpeed;
    public float rotateAmount;

    private Quaternion rotation;//za rotaciju, mnogo komplikovano samo se koriste neke metode

    private float panDetect = 40;
    private float minHeight = 10;
    private float maxHeight = 200;
    // Start is called before the first frame update
    void Start()
    {
        rotation = Camera.main.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        RotateCamera();

        if(Input.GetMouseButtonDown(0))
        {
            LeftClick();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.transform.rotation = rotation;
        }
    }

    public void LeftClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//laseri
        RaycastHit hit;//laseri koji udare

        if (Physics.Raycast(ray, out hit, 1000))//100-koliko daleko da proverava da li je udario
        {
            if(hit.collider.tag == "Ground")//hit tj collider ima info objekta koji je udario
            {
                selectedObject = null;
                selectedInfo.isSelected = false;
                Debug.Log("Deselected");//mnogo pomaze
            }
            else if(hit.collider.tag == "Selectable")
            {
                selectedObject = hit.collider.gameObject;
                selectedInfo = selectedObject.GetComponent<ObjectInfo>(); //typeof(ObjectInfo)

                selectedInfo.isSelected = true; //nisam mogao da nadjem ObjectInfo klasu
                Debug.Log("Selected" + selectedInfo.objectName);
            }
        }
    }

    private void RotateCamera()
    {
        Vector3 origin = Camera.main.transform.eulerAngles; //pocetno
        Vector3 destination = origin;                       //krajnje

        if(Input.GetMouseButton(2))
        {
            destination.x -= Input.GetAxis("Mouse Y") * rotateAmount;
            destination.y += Input.GetAxis("Mouse X") * rotateAmount;
        }
        if(destination != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * rotateSpeed);
        }
    }

    private void MoveCamera()
    {
        float moveX = Camera.main.transform.position.x;
        float moveY = Camera.main.transform.position.y;
        float moveZ = Camera.main.transform.position.z;

        
        

        float xPosition = Input.mousePosition.x;
        float yPosition = Input.mousePosition.y;

        if(Input.GetKey(KeyCode.A) || xPosition > 0 && xPosition < panDetect && Camera.main.transform.position.x > -77)
        {
            moveX -= panSpeed;
        }
        else if (Input.GetKey(KeyCode.D) || xPosition < Screen.width && xPosition > Screen.width - panDetect && Camera.main.transform.position.x  < 70)
        {
            moveX += panSpeed;
        }
        if (Input.GetKey(KeyCode.W) || yPosition > Screen.height && yPosition > Screen.height-panDetect && Camera.main.transform.position.z < 6.1)
        {
            moveZ += panSpeed;
        }
        else if (Input.GetKey(KeyCode.S) || yPosition > 0 && yPosition < panDetect && Camera.main.transform.position.z > -161)
        {
            moveZ -= panSpeed;
        }

        moveY -= Input.GetAxis("Mouse ScrollWheel") * (panSpeed * 20);//* 20 da bude bolje

        moveY = Mathf.Clamp(moveY, minHeight, maxHeight);
        Vector3 newPosition = new Vector3(moveX,moveY,moveZ);

        Camera.main.transform.position = newPosition;

    }
}
