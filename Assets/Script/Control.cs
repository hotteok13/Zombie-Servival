using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    [SerializeField] float axisSpeed = 5.0f; // ī�޶� x��� y���� ȸ�� �ӵ� 
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

        // �ٴڰ� �浹���� �ʾҴٸ�
        if(characterControl.isGrounded == false)
        {
            moveForce.y -= gravity * Time.deltaTime;
        }
        

        characterControl.Move(moveForce * Time.deltaTime);

    }

    public void MoveTo(Vector3 direction)
    {
        // ī�޶� ȸ������ ���� ������ ���ϱ� ������ ȸ�� ���� ���ؼ� �����մϴ�.
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        // ��/ �Ʒ��� �ٶ󺸰� �̵��� �� ĳ���� ������Ʈ�� �������� �̵��ϰų� �Ʒ��� ����ɱ� ������
        // direction�� �״�� ������� �ʰ�, moveForce ������ x�� z���� �־ ����մϴ�.
        moveForce = new Vector3(direction.x * speed, moveForce.y, direction.z * speed);
    }

    public void UpdateRotate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * axisSpeed; // ���콺 ��/�� �̵����� ī�޶� y�� ȸ��

        // ���콺 �Ʒ��� ������ -�� �����ε� ������Ʈ�� x���� + �������� ȸ���ؾ� �Ʒ��� ���� �����Դϴ�.
        eulerAngleX -= mouseY * axisSpeed; // ���콺 ��/�Ʒ� �̵����� ī�޶� x�� ȸ��

        // ī�޶� x�� ȸ���� ��� ȸ�� ������ �����մϴ�.
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
