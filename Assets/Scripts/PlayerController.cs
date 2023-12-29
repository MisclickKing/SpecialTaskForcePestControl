using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    // Variables
    [SerializeField] private float playerSpeed = 4.1f;
    [SerializeField] private float playerSpeedBoost = 2.6f;
    [SerializeField] private float playerTopSpeed = 6.7f;
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravityValue = -28f;
    [SerializeField] private float xSensitivity = 10.0f;
    [SerializeField] private float ySensitivity = 10.0f;
    [SerializeField] private Camera cam;
    [SerializeField] private Light flashlight;
    [SerializeField] private GameObject barrel;
    [SerializeField] private GameObject normalBullet;
    [SerializeField] private GameObject flameThrower;
    [SerializeField] private Light gunLight;
    [SerializeField] private Light gunLaser;
    [SerializeField] private Image redicle;

    private float xRotation = 0f;
    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Vector2 turn;
    private Vector3 destination;
    private int ammoType = 1;
    private float timeToFire;
    private float fireRate;
  
    // Action inputs
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction lightOnOff;
    private InputAction ammo1;
    private InputAction ammo2;
    // private InputAction ammo3;
    // private InputAction ammo4;
    private InputAction shoot;


    // Load on start
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        lookAction = playerInput.actions["Look"];
        sprintAction = playerInput.actions["Sprint"];
        lightOnOff = playerInput.actions["Flashlight"];
        ammo1 = playerInput.actions["Ammo1"];
        ammo2 = playerInput.actions["Ammo2"];
        // ammo3 = playerInput.actions["Ammo3"];
        // ammo4 = playerInput.actions["Ammo4"];
        shoot = playerInput.actions["Shoot"];
        
        Cursor.lockState = CursorLockMode.Locked;

        gunLight.color = Color.yellow;
        gunLight.intensity = 50f;
        redicle.enabled = false;
    }


    // Player is on floor
    private void isOnFloor()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
    }


    // Player movement
    private void move()
    {
        // Walk
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 move = Vector3.zero;
        move.x = moveInput.x;
        move.z = moveInput.y;
        controller.Move(transform.TransformDirection(move) * playerSpeed * Time.deltaTime);

        // Sprint
        if(sprintAction.inProgress)
        {
            if(playerSpeed != playerTopSpeed)
            {
                playerSpeed = playerTopSpeed;
            }
        }
        else if(playerSpeed == playerTopSpeed)
        {
            playerSpeed -= playerSpeedBoost;
        }
    }

    // Player jump
    private void jump()
    {
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }


    // Player look
    private void look()
    {
        // look calculations
        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        turn.x = (lookInput.x * xSensitivity);
        turn.y = (lookInput.y * ySensitivity);

        xRotation -= (turn.y * Time.deltaTime);
        xRotation = Mathf.Clamp(xRotation, -75f, 40f);

        // Look up/and logic 
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * turn.x * Time.deltaTime);
    }

    // Flashlight
    private void helmetLight()
    {
        if (lightOnOff.triggered)
        {
            flashlight.enabled = !flashlight.enabled;
        }
    }

    // Ammo Switching
    private void ammoSwitching()
    {
        if (ammo1.triggered) 
        {
            ammoType = 1;
            redicle.enabled = false;
            gunLaser.enabled = true;
            gunLight.color = Color.yellow;
            gunLight.intensity = 50f;
            
        }

        if (ammo2.triggered) 
        {
            ammoType = 2;
            redicle.enabled = true;
            gunLaser.enabled = false;
            gunLight.color = Color.red;
            gunLight.intensity = 36f;
        }

        // if (ammo3.triggered) 
        // {
        //     // ammoType = 3;
        //     gunLaser.enabled = false;
        //     gunLight.color = Color.green;
        //     gunLight.intensity = 50f;
        // }

        // if (ammo4.triggered) 
        // {
        //     // ammoType = 4;
        //     gunLaser.enabled = false;
        //     gunLight.color = Color.magenta;
        //     gunLight.intensity = 300f;
        // }
    }

    // Normal bullet instantiation
    private void createNormalBullet()
    {
        float normalBulletSpeed = 30f;
        var projectileObj = Instantiate(normalBullet, barrel.transform.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - barrel.transform.position).normalized * normalBulletSpeed;
    }

    // Flame Thrower instantiation
    private void createFire()
    {
        float flameThrowerSpeed = 25f;
        var projectileObj = Instantiate(flameThrower, barrel.transform.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - barrel.transform.position).normalized * flameThrowerSpeed;
    }

    // Projectile math
    private void projectileTrajectory()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        } else {
            destination = ray.GetPoint(1000f);
        }
    }

    // ammo logic
    private void shootProjectile()
    {
        // Normal Ammo
        if(ammoType == 1 && Time.time >= timeToFire){
            fireRate = 2;
            projectileTrajectory();
            timeToFire = Time.time + 1/fireRate;
            createNormalBullet();
        }
        // Flamethrower
        if(ammoType == 2 && Time.time >= timeToFire){
            fireRate = 15;
            projectileTrajectory();
            timeToFire = Time.time + 1/fireRate;
            createFire();
        }

    }

    // Shoot function
    private void fireGun()
    {
        if(shoot.inProgress)
        {
            shootProjectile();
        }
    }

    // Update every frame
    void Update()
    {
        isOnFloor();
        move();
        jump();
        look();
        helmetLight();
        ammoSwitching();
        fireGun();
    }
}