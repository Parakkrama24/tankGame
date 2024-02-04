using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PloayerController : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls; 
    
    [Header("Map Name refarence ")]
    [SerializeField] private string ActionMapName="Player";

    [Header("Action map refernce")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string sprint = "Sprint";

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction sprintAction;
   
    public Vector2 moveInput {  get; private set; }
    public Vector2 lookInput{ get;  private set; }
    public float sprintInput {  get; private set; }

    public static PloayerController Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        moveAction =playerControls.FindActionMap(ActionMapName).FindAction(move);
        lookAction =playerControls.FindActionMap(ActionMapName).FindAction(look);
        sprintAction =playerControls.FindActionMap(ActionMapName).FindAction(sprint);
        RegisterInputAction();

    }

    void RegisterInputAction()
    {
        moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveInput = Vector2.zero;
        lookAction.performed += context => lookInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => lookInput = Vector2.zero;

        sprintAction.performed += context => sprintInput = context.ReadValue<float>();
        sprintAction.canceled += context => sprintInput = 0f;
    }
    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        sprintAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable(); 
        sprintAction.Disable();
    }
}
