using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 5f;
  
  private PlayerControls _controls;
  private InputAction _moveAction;
  
  private float cellSize = 1f;
  private Vector3 targetPosition;
  
  void Awake()
  {
    _controls = new PlayerControls();
    
    _moveAction = _controls.Player.Move;
  }

  void OnEnable()
  {
    _controls.Enable();
    _moveAction.performed += OnMove;
  }

  void OnDisable()
  {
    _controls.Disable();
    _moveAction.performed -= OnMove;
  }

  void Start()
  {
   targetPosition =  transform.position; 
  }

  void Update()
  {
    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
  }

  private void OnMove(InputAction.CallbackContext context)
  {
    Vector2 inputVector = context.ReadValue<Vector2>();
    Vector3 moveDirection = Vector3.zero;

    if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
    {
      moveDirection = new Vector3(Mathf.Sign(inputVector.x) * cellSize, 0, 0);
    }
    
    else if (Mathf.Abs(inputVector.x) < Mathf.Abs(inputVector.y))
      {
      moveDirection = new Vector3(0, Mathf.Sign(inputVector.y) * cellSize, 0);
      }
    
    targetPosition += moveDirection;
  }
  
}
