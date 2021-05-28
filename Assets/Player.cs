using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Queue<Vector2> _movementTargets = new Queue<Vector2>();
    private Vector2 lastMovementTarget;
    private Vector2 currentTarget;
    private float _speed = 5f;
    private Camera _camera;
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse = Input.mousePosition;
            Vector2 target = _camera.ScreenToWorldPoint(mouse);

            _movementTargets.Enqueue(target);
        }


        if (Vector2.Distance(currentTarget, transform.position) < 0.1)
        {
            if (_movementTargets.Count > 0)
            {
                currentTarget = _movementTargets.Dequeue();
            }

            {
                animator.SetBool(IsMoving, false);
            }
        }
        else
        {
            animator.SetBool(IsMoving, true);
            transform.position = Vector2.MoveTowards(transform.position, currentTarget, (_speed * Time.deltaTime));
        }
    }
}