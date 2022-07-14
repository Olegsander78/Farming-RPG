using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCotroller : MonoBehaviour
{
    public float MoveSpeed;

    private Vector2 _moveInput;
    private bool _interactInput;

    private Vector2 _facingDir;

    public LayerMask InteractLayerMask;

    public Rigidbody2D Rig;
    public SpriteRenderer Sr;

    private void Update()
    {
        if(_moveInput.magnitude != 0f)
        {
            _facingDir = _moveInput.normalized;
            Sr.flipX = _moveInput.x > 0;
        }

        if (_interactInput)
        {
            TryInteractTile();
            _interactInput = false;
        }
    }

    private void FixedUpdate()
    {
        Rig.velocity = _moveInput.normalized * MoveSpeed;
    }

    private void TryInteractTile()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + _facingDir, Vector2.up, 0.1f, InteractLayerMask);

        if (hit.collider != null)
        {
            FieldTile tile = hit.collider.GetComponent<FieldTile>();
            tile.Interact();
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            _interactInput = true;
        }
    }
}
