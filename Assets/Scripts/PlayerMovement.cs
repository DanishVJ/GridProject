using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] private float moveSpeed = 5f;
  
  private PlayerControls _controls;
  private InputAction _moveAction;
  private PlayerFacing _playerFacing;
  
  private float _cellSize = 1f;
  private Vector3 _targetPosition;
  
  void Awake()
  {
    _controls = new PlayerControls();
    _moveAction = _controls.Player.Move;
    _playerFacing = GetComponent<PlayerFacing>();
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
   _targetPosition =  transform.position; 
  }

  void Update()
  {
    transform.position = Vector3.MoveTowards(transform.position, _targetPosition, moveSpeed * Time.deltaTime);
  }

  private void OnMove(InputAction.CallbackContext context)
  {
    Vector2 inputVector = context.ReadValue<Vector2>();
    Vector3 moveDirection = Vector3.zero;

    if (Mathf.Abs(inputVector.x) > Mathf.Abs(inputVector.y))
    {
      moveDirection = new Vector3(Mathf.Sign(inputVector.x) * _cellSize, 0, 0);
    }
    
    else if (Mathf.Abs(inputVector.x) < Mathf.Abs(inputVector.y))
      {
      moveDirection = new Vector3(0, Mathf.Sign(inputVector.y) * _cellSize, 0);
      }
    // 1. Calculate where the player WANTS to go next
    Vector3 potentialTarget = _targetPosition + moveDirection;

    // 2. Ask the GridCreator to find the cell at that world position
    Cell nextCell = GridCreator.Instance.GetCellFromWorldPosition(potentialTarget);

    // 3. Check if the cell exists and if it is walkable
    if (nextCell != null && nextCell.IsWalkable())
    {
      // Path is clear! Go ahead and update the target position
      _targetPosition = potentialTarget;
      _playerFacing.SetFacing(moveDirection);
    }
    else
    {
      Debug.Log("Movement blocked! The cell is either off the grid, a wall, or an enemy.");
    }
  }
  
}
