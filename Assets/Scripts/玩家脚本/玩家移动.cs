using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class 玩家移动 : MonoBehaviour
{
    public int Frame;
    int FrameCounter;
    public Vector2 velocity=new Vector2();
    public float Speed = 15;
    public LayerMask Ground;
    private int direction = 1;
    private Rigidbody2D PhysicalController;
    private Animator AnimationController;
    private bool Jumping=false;
    private int JumpingCd = 0;
    private bool Dashing = false;
    private int DashCd = 0;

    public GameObject Dashshadow;

    private List<SpriteRenderer> shadows = new List<SpriteRenderer>();
    public int shadowAmount=10;
    private SpriteRenderer _sp;
    // Start is called before the first frame update
    void Start()
    {
        PhysicalController= GetComponent<Rigidbody2D>();
        AnimationController= GetComponent<Animator>();
        _sp = GetComponent<SpriteRenderer>();
        for (int i = 0; i < shadowAmount; i++)
        {
            SpriteRenderer shadow = new GameObject("shadow" + i).AddComponent<SpriteRenderer>();
            shadow.sortingOrder = _sp.sortingOrder;
            shadow.sortingLayerName = _sp.sortingLayerName;
            shadow.sprite = _sp.sprite;
            shadow.gameObject.SetActive(false);
            shadows.Add(shadow);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        //ControlMoveMent();
    }
    private void FixedUpdate()
    {
        PhysicalMovement();
    }
    void ControlMoveMent()
    {
        /*if (velocity.magnitude < 5)
        {
            velocity.x+= Input.GetAxisRaw("Horizontal")*0.3f;
        }
        if (Input.GetAxisRaw("Horizontal") == 0) velocity *= 0.9f;
        //velocity.x = Input.GetAxisRaw("Horizontal");
        //velocity.y = Input.GetAxisRaw("Vertical");
        //velocity.Normalize();
        //velocity *= 15f;
        transform.Translate(velocity * Time.deltaTime);
        //transform.position += new Vector3(velocity.x, velocity.y)*Time.deltaTime;*/
    }
    void PhysicalMovement()
    {
        if (!Dashing)//不在冲刺状态时，方向键控制左右移动
        {
            velocity.x = Input.GetAxisRaw("Horizontal");
            //velocity.y = Input.GetAxisRaw("Vertical");
            velocity *= Speed;
            PhysicalController.AddForce(velocity);
        }

        if (velocity.x > 0|| velocity.x<0)
        {
            AnimationController.SetBool("IsWalking", true);
        }
        else
        {
            AnimationController.SetBool("IsWalking", false);
        }
        
        if (PhysicalController.velocity.x > 0.1f)
        {
            direction = 1;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (PhysicalController.velocity.x < -0.1f)
        {
            direction=-1;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        Jump();
        Dash();
        //Debug.Log(" "+ PhysicalController.IsTouchingLayers());
        //Debug.Log(Input.GetAxisRaw("Jump"));
        //if(velocity.x<0)
    }
    void Dash()
    {
        if (DashCd <= 0)
        {
            Dashing = false;
            if (Input.GetAxisRaw("Dash") == 1)
            {
                StartCoroutine(ShowShadows());

                Dashing = true;
                DashCd = 15;
                PhysicalController.AddForce(new Vector2(direction*1000, 0));
            }
        }
        else DashCd--;
        if (Dashing)
        {   

            //StartCoroutine(ShowShadows());

            //ShowShadows();

        }
        if (shadows[9].color.a > 0)
        {
            foreach (var shadow in shadows)
            {
                if (shadow.color.a > 0)
                {
                    shadow.color *= 0.85f;
                }
            }
        }

    }
    IEnumerator ShowShadows()
    {
        for(int i = 0; i < shadowAmount; i++)
        {
            shadows[i].color = _sp.color;
            shadows[i].sprite = _sp.sprite;
            shadows[i].transform.position = transform.position;
            shadows[i].transform.rotation = transform.rotation;
            //shadows[i].transform.
            shadows[i].transform.localScale = _sp.transform.localScale;
            shadows[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.05f);
        }
    }
    void Jump()
    {
        Vector2 boxcenter = (Vector2)transform.position + new Vector2(direction * 0.023f, -0.28f);
        Vector2 boxsize = new Vector2(0.10f, 0.06f);
        if (Physics2D.OverlapBox(boxcenter, boxsize, 0, Ground) != null)
        {
            if (Input.GetAxisRaw("Jump") == 1 && !Jumping)
            {
                Jumping = true;
                JumpingCd = 3;
                PhysicalController.AddForce(new Vector2(0, 400));
            }
            if (JumpingCd <= 0)
            {
                AnimationController.SetBool("IsJumping", false);
                Jumping = false;
            }
            else JumpingCd--;

        }
        else
        {
            PhysicalController.AddForce(new Vector2(0, -10));
            AnimationController.SetBool("IsJumping", true);
        }
    }
    private void OnDrawGizmos()
    {
        Vector2 boxcenter =(Vector2)transform.position+ new Vector2(direction * 0.023f,-0.28f);
        Vector2 boxsize = new Vector2(0.10f, 0.06f);
        if (Physics2D.OverlapBox(boxcenter,boxsize,0, Ground))Gizmos.color = Color.green;
        else Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxcenter, boxsize);
    }

}
