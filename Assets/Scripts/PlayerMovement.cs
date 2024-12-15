using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private StatsSO stats;
    [SerializeField] private BoneSO bones;
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;
    private float hitRange = 0.3f;
    public LayerMask mapLayer; 
    private bool moveFlag = true;
    private bool isMoving = true;
    private bool isShooting = false;
    private Animator animator;

    void Start()
    {
        mapLayer = UnityEngine.LayerMask.GetMask("Map");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        isMoving = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;

        UpdateMovementAnimation();

        if (Input.GetButtonDown("Fire1")) {
            isShooting = true;
            animator.SetBool("isShooting", true);
            Shoot();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isShooting = false;
            animator.SetBool("isShooting", false);
        }

        CheckCollision();
        CheckMapCollision();
    }

    void FixedUpdate()
    {
        if (!isShooting && moveFlag)
        {
            Vector3 m_Input = new Vector3(horizontal, vertical, 0);
            rb.MovePosition(transform.position + m_Input * stats.speed * Time.fixedDeltaTime);
        }
    }
    
    private void CheckCollision()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, hitRange);
        foreach (var hit in hits)
        {
            if (hit.gameObject.tag == "Guard")
                moveFlag = CanMoveOutOfCollision(hits);
        }
    }

    private void CheckMapCollision()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, hitRange, mapLayer);
        if (hits.Length > 0)
        {
            moveFlag = CanMoveOutOfCollision(hits);
        }

        
    }
    
    private bool CanMoveOutOfCollision(Collider2D[] hits)
    {
        foreach (var hit in hits)
        {
            Vector2 directionToExit = (transform.position - hit.transform.position).normalized;

            if (Input.GetAxisRaw("Horizontal") * directionToExit.x > 0 ||
                Input.GetAxisRaw("Vertical") * directionToExit.y > 0)
            {
                return true;
            }
        }
        return false;
    }

    // animation functions
    private void UpdateMovementAnimation()
    {
        animator.SetBool("isRunning", isMoving);
    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bone")
        {
            bones.boneCurrent++;
            Destroy(other.gameObject);
            Debug.Log(bones.boneCurrent);
        }
        else if (other.gameObject.tag == "GuardTrigger")
        {
            CanvasGroup guardCanvas = other.gameObject.GetComponentInChildren<CanvasGroup>();
            
            if (bones.boneCurrent == bones.boneMax)
            {
                GameObject panel = guardCanvas.gameObject.GetComponentInChildren<Image>().gameObject;
                TextMeshProUGUI text = panel.GetComponentInChildren<TextMeshProUGUI>();
                text.text = "You have enough bones to pass!";
            }
            StartCoroutine(FadeCanvasOpacity(1f, 0.6f, guardCanvas));
        }
    }
    public IEnumerator FadeCanvasOpacity(float targetOpacity, float duration, CanvasGroup canvasGroup)
    {
        float startOpacity = canvasGroup.alpha;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startOpacity, targetOpacity, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final opacity is set to target
        canvasGroup.alpha = targetOpacity;
        if (targetOpacity == 1.0f)
        {
            yield return new WaitForSeconds(4f);
            StartCoroutine(FadeCanvasOpacity(0f, 1.0f, canvasGroup));
            
        }
    }

}