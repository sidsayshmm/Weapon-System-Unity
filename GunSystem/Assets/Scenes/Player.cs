using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float movementSpeed;
    Rigidbody2D rb;
    Vector2 movement;
    Vector2 mousePos;

    [Header("AimDistance")]
    LineRenderer lr;
    Vector3 endPosition;
    [SerializeField] float maxDistance;
    [SerializeField] float currentDistance;
    [SerializeField] AnimationCurve ac;

    [Header("Shooting")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float fireRate;
    [SerializeField] float bulletForce;
    bool canFire;
    bool isAiming;

    [Header("SandbagTest")]
    [SerializeField] Button sandBagButton;
    [SerializeField] GameObject sandBagPrefab;
    GameObject currentPlaceableObject;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        AimDistance();
        if (currentPlaceableObject != null)
        {
            OnObjectRelease();

        }

    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        movement.y = Input.GetAxisRaw("Vertical");
        movement.x = Input.GetAxisRaw("Horizontal");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
        Vector2 lookDirection = rb.position - mousePos;
        Debug.DrawLine(rb.position, mousePos, Color.red);
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

        if (movement.y != 0 || movement.x != 0)
        {
            rb.SetRotation(angle);
        }
    }

    private void AimDistance()
    {
        if (Input.GetMouseButtonDown(1))
        {

            lr.enabled = true;
            lr.positionCount = 2;
            lr.useWorldSpace = true;
        }

        if (Input.GetMouseButton(1))
        {
            isAiming = true;
            endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lr.SetPosition(0, firePoint.position);
            
            Vector3 dir = firePoint.up - endPosition;
            RaycastHit2D ray = Physics2D.Raycast(firePoint.up, dir.normalized, Vector2.Distance(firePoint.position, endPosition));
            Debug.DrawRay(firePoint.position, dir, Color.blue);
            if (ray.collider != null)
            {
                Debug.Log(ray.collider);
                lr.SetPosition(1, ray.point);
            }
            else
            {
                lr.SetPosition(1, endPosition);

            }
            lr.widthCurve = ac;
            lr.numCapVertices = 50;
            lr.numCornerVertices = 20;

            currentDistance = Vector3.Distance(firePoint.position, endPosition);

            if (currentDistance >= maxDistance)
            {
                lr.startColor = Color.yellow;
                lr.endColor = Color.red;
            }
            else
            {
                lr.startColor = Color.blue;
                lr.endColor = Color.green;
                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine("Shoot");
                }
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
            lr.enabled = false;
        }
    }

    public IEnumerator Shoot()
    {
        if (isAiming && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attempting to shoot");
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            canFire = false;
            yield return new WaitForSeconds(fireRate);
            canFire = true;
        }

    }

    public void SpawnSandBag()
    {
        if (currentPlaceableObject != null)
        {
            Destroy(currentPlaceableObject);
        }
        else
        {
            currentPlaceableObject = Instantiate(sandBagPrefab);
        }
    }

    public void OnObjectRelease()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPlaceableObject = null;
        }

    }
}