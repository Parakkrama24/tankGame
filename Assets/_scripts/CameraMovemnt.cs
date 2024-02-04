using UnityEngine;
using UnityEngine.UI;

public class CameraMovemnt : MonoBehaviour
{
    private float speed = 0.0f;
    private float zoomSpeed = 10.0f;
    private float rotateSpeed = 0.1f;

    private float  maxHeight= 40f;
    private float minHeight = 4f;

    Vector2 p1;
    Vector2 p2;

    [SerializeField] private Slider camreSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = camreSpeed.value*0.06f;
            zoomSpeed =camreSpeed.value* 20.0f;
        }
        else
        {
            speed =camreSpeed.value* 0.03f;
            zoomSpeed = camreSpeed.value * 10f;
        }

        float hsp =transform.position.y* speed * Input.GetAxis("Horizontal");
        float Vsp =transform.position.y* speed * Input.GetAxis("Vertical");
        float scrolSpeed =Mathf.Log( transform.position.y)*-zoomSpeed * Input.GetAxis("Mouse ScrollWheel");


        if((transform.position.y >= maxHeight) && (scrolSpeed>0))
        {
            scrolSpeed = 0;
        }
        else if((transform.position.y < minHeight) && (scrolSpeed>0))
        {
            scrolSpeed = 0;
        }

        if(transform.position.y +scrolSpeed> maxHeight)
        {
            scrolSpeed= maxHeight-transform.position.y;
        }
        else if(transform.position.y+scrolSpeed < minHeight)
        {
            scrolSpeed= minHeight-transform.position.y;
        }

        Vector3 verticalMouve = new Vector3(0, scrolSpeed, 0);
        Vector3 lateralMove = hsp * transform.right;
        Vector3 forwrdMove = transform.forward;
        forwrdMove.y = 0;
        forwrdMove.Normalize();
        forwrdMove *=Vsp;

        Vector3 move = verticalMouve + lateralMove + forwrdMove;
        transform.position += move;


        CameraRotation();
    }

    void CameraRotation()
    {
        if (Input.GetMouseButtonDown(2))
        {
            p1 = Input.mousePosition;
        }

        if(Input.GetMouseButton(2))//hold middle mouse button
        {
            p2 = Input.mousePosition;

            float dx=(p2 - p1).x-rotateSpeed;
            float dy=(p2 - p1).y-rotateSpeed;

            transform.rotation*= Quaternion.Euler(new Vector3(0, dy, 0));//y rotation
            transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy, 0, 0));
            p1 = p2;
        }
    }
}
