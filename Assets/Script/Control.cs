using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    [SerializeField] float axisSpeed = 5.0f; // 카메라 x축과 y축의 회전 속도 
    [SerializeField] GameObject eye;
    [SerializeField] Image bloodScreen;

    public int health = 100;
    private float eulerAngleX;
    private float eulerAngleY;

    

    private CharacterController characterControl;
    private Vector3 moveForce;
    [SerializeField] float distance = 100.0f;
    [SerializeField] float speed;
    [SerializeField] float gravity = 20.0f;
    [SerializeField] ParticleSystem effect;
    [SerializeField] LayerMask layer;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        characterControl = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (health <= 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            GameManager.instance.resultScreen.SetActive(true);
            Time.timeScale = 0;
        }
        UpdateRotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if(Input.GetButtonDown("Fire1"))
        {
            SoundSystem.instance.Sound(0);
            effect.Play();
            
            TwpStepRay();
            
        }

        Jump();
        MoveTo(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

        // 바닥과 충돌하지 않았다면
        if(characterControl.isGrounded == false)
        {
            moveForce.y -= gravity * Time.deltaTime;
        }
        

        characterControl.Move(moveForce * Time.deltaTime);

    }

    public void MoveTo(Vector3 direction)
    {
        // 카메라 회전으로 전방 방향이 변하기 때문에 회전 값을 곱해서 연산합니다.
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        // 위/ 아래를 바라보고 이동할 때 캐릭터 오브젝트가 공중으로 이동하거나 아래로 가라앉기 때문에
        // direction을 그대로 사용하지 않고, moveForce 변수에 x와 z값만 넣어서 사용합니다.
        moveForce = new Vector3(direction.x * speed, moveForce.y, direction.z * speed);
    }

    public void UpdateRotate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * axisSpeed; // 마우스 좌/우 이동으로 카메라 y축 회전

        // 마우스 아래로 내리면 -로 음수인데 오브젝트의 x축이 + 방향으로 회전해야 아래를 보기 때문입니다.
        eulerAngleX -= mouseY * axisSpeed; // 마우스 위/아래 이동으로 카메라 x축 회전

        // 카메라 x축 회전의 경우 회전 범위를 설정합니다.
        eulerAngleX = ClampAngle(eulerAngleX, -80, 50);

        transform.rotation = Quaternion.Euler(transform.rotation.x, eulerAngleY, 0);

        eye.transform.rotation = Quaternion.Euler(eulerAngleX, transform.eulerAngles.y, 0);
    }

    public float ClampAngle(float angle, float min, float max)
    {
        return Mathf.Clamp(angle, min, max);
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (characterControl.isGrounded)
            {
                moveForce.y = 10f;
                
            }
        }
    }

    public void TwpStepRay()
    {
        Ray ray;
        RaycastHit hit;
        Vector3 target = Vector3.zero;

        ray = Camera.main.ViewportPointToRay(Vector2.one * 0.5f);

        if(Physics.Raycast(ray,out hit, distance))
        {
            target = hit.point;
            Instantiate(effect, hit.point, hit.transform.rotation);
        }
        else
        {
            target = ray.origin + ray.direction * distance;
        }

        Vector3 direction = (target - effect.transform.position);

        if (Physics.Raycast(effect.transform.position, direction,out hit,distance, layer))
        {
            hit.collider.GetComponentInParent<Zombie>().health -= 20;
            
        }
        
    }

    public void ScreenCall()
    {
        StartCoroutine(nameof(Damage));
    }

    public IEnumerator Damage()
    {
        bloodScreen.color = new Color(1, 0, 0);
        yield return new WaitForSeconds(0.1f);
        bloodScreen.color = Color.clear;
    }
}
