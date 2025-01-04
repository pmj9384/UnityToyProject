using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Transform gunPivot;

    public Gun gun;
    [SerializeField]
    private PlayerInput input;

    private void Awake()
        {
            input = GetComponent<PlayerInput>();
        }

    private void Update()
    {   
        if (input.Fire)    
        {
             gun.Fire();
        }

    }
}
