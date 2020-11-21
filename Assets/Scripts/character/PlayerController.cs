using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLifeChangedEventArgs : EventArgs
{
    public int life { get; set; }
    public int diff { get; set;}
}

public class PlayerController : PhysicsObject {

    [SerializeField]
    public float maxSpeed = 7;
    [SerializeField]
    public float jumpTakeOffSpeed = 7;
    [SerializeField]
    public float jumpPressedRememberTime = 0.2f;
    [SerializeField]
    public float groundedRememberTime = 0.2f;
    [SerializeField]
    [Range(0, 1)]
    public float horizontalDampingBasic = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    public float horizontalDampingWhenStopping = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    public float horizontalDampingWhenTurning = 0.5f;
    [SerializeField]
    [Range(0, 10)]
    public float fallMultiplier = 1f;
    [SerializeField]
    [Range(0, 10)]
    public float lowJumpMultiplier = 1f;
    public float vYmax = 9999f;

    public AudioClip jumpSound;
    public AudioClip hurtSound;

    public ParticleSystem dust;
    public TimeManager timeManager;
    public bool godMode;
    public float unvisibleTimer = 0.5f;
    public float wallSliddingSpeed;
    public Transform wallCheck;
    public float wallForceX;
    public float wallForceY;
    public event EventHandler<OnLifeChangedEventArgs> OnLifeChanged;


    private int life = 4;
    private HealthBar healthBar;
    private SpriteRenderer spriteRenderer;
    // allows to jump few frames before to be grounded
    private float jumpPressedRemember = 0.0f;
    private bool jumpedPressed;
    // allow jump few frame after leaving the floor
    private float groundedRemember = 0.0f;
    //private Animator animator;
    private Inventory inventory;
    private bool shoot;
    private bool unvisible = false;
    private bool wallSlidding;
    private float checkRadius = 1.0f;
    private Vector2 move;


    public int Life {
        get => life;
        set {
            OnLifeChangedEventArgs args = new OnLifeChangedEventArgs();
            args.life = value;
            args.diff = value - life;
            if(OnLifeChanged != null)
            {
                OnLifeChanged(this, args);
            }
            life = value;
        }
    }

    protected new void Start()
    {
        if(GameManager.instance) {
            Life = (int) GameManager.instance.CharacterStats.life.Value;
            jumpTakeOffSpeed += GameManager.instance.CharacterStats.jumpTakeOffSpeed.Value;
            maxSpeed += GameManager.instance.CharacterStats.maxSpeed.Value;
        }
        shoot = false;
        spriteRenderer = GetComponent<SpriteRenderer> ();
        inventory = GetComponent<Inventory>();
        //animator = GetComponent<Animator> ();
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        CheckInput();
        CheckMovementDirection();
        CheckIfIwallSliding();
    }

    private void CheckInput()
    {
        move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Jump")) {
            jumpPressedRemember = jumpPressedRememberTime;
            if(!IsGrounded() && !wallSlidding) {
                shoot = true;
            }
        }

        if(Input.GetButtonUp("Jump"))
        {
            SoundManager.instance.PlaySingle(jumpSound);
            if (velocity.y > 0) {
                velocity.y = velocity.y * ( lowJumpMultiplier - 1.0f );
            }
            // in case of shooting
            shoot = false;
        }


        if (inventory.CanShoot() && shoot)
        {
            velocity.y = inventory.Shoot();
        }

        if(IsGrounded())
        {
            inventory.Reload();
        }
    }

    private void CheckMovementDirection()
    {
        if ((spriteRenderer.flipX && move.x > 0.1f) || (!spriteRenderer.flipX && move.x < -0.1f))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            CreateDust();

        }
    }

    private void CheckSurroundings()
    {
        groundedRemember = groundedRemember - Time.deltaTime;
        if(grounded && groundedName != "Enemy") // do not taking account grounded when the enemy is the collider
        {
            groundedRemember = groundedRememberTime;
        }

        jumpPressedRemember = jumpPressedRemember - Time.deltaTime;
    }


    protected override void ComputeVelocity()
    {
        // X avis
        if (Mathf.Abs(move.x) < 0.01f)
        {
            move.x *= Mathf.Pow(1f - horizontalDampingWhenStopping, Time.deltaTime * 10f);
        }
        else if (Mathf.Sign(move.x) != Mathf.Sign(move.x))
        {
            move.x *= Mathf.Pow(1f - horizontalDampingWhenTurning, Time.deltaTime * 10f);
        }
        else
        {
            move.x *= Mathf.Pow(1f - horizontalDampingBasic, Time.deltaTime * 10f);
        }

        // y axis
        CheckSurroundings();
        if (CanJump() && IsGrounded())
        {
            CreateDust();
            jumpPressedRemember = 0.0f;
            groundedRemember = 0.0f;
            velocity.y = jumpTakeOffSpeed;
        }

        if (velocity.y < 0)
        {
            velocity.y = velocity.y * ( fallMultiplier - 1.0f);
        }

        // limit the fall velocity
        if(velocity.y < -vYmax)
        {
            velocity.y = -vYmax;
        }

        if(wallSlidding)
        {
            velocity.y = Mathf.Clamp(velocity.y, -wallSliddingSpeed, float.MaxValue);
        }

         if(wallSlidding && CanJump())
         {
            jumpPressedRemember = 0.0f;
            move.x = wallForceX * -move.x;
            velocity.y = wallForceY;
         }
        //animator.SetBool ("grounded", grounded);
        //animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);
        targetVelocity = move * maxSpeed;
    }

    private bool CanJump()
    {
        return jumpPressedRemember > 0.0f;
    }

    private bool IsGrounded()
    {
        return groundedRemember > 0.0f;
    }

    private void CheckIfIwallSliding()
    {
        Vector3 originRay = spriteRenderer.flipX ? -transform.right : transform.right;

        RaycastHit2D isTouchingFront = Physics2D.Raycast(wallCheck.position, originRay, checkRadius, LayerMask.GetMask("bloc"));
        Debug.DrawLine(wallCheck.position, wallCheck.position + (originRay * checkRadius), Color.green, 2);

        wallSlidding = isTouchingFront.collider && !IsGrounded() && velocity.y < 0.0f;
    }

    public void Hurt(EnemyBase enemy)
    {
        if(!unvisible && !godMode) {
            if(life >= 1) {
                // todo add armor
                Life = Math.Max(life - enemy.Damage, 0);
                SoundManager.instance.PlaySingle(hurtSound);
            }

            if(life <= 0)
            {
                LevelManager.instance.GameOver();
                return;
            }
            StartCoroutine(FlashSprite(GetComponent<SpriteRenderer>(), 0.0f, 1.0f, 0.1f, unvisibleTimer));
            StartCoroutine(GetUnvisible(unvisibleTimer, enemy));
            //timeManager.DoSlowMotion();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(unvisible) {
            return;
        }
        EnemyBase enemy = collision.collider.GetComponent<EnemyBase>();
        if(!enemy) {
            //Debug.Log(collision.collider.name);
            // otherwise it must be floor
            if(LevelManager.instance != null)
            {
                foreach(ContactPoint2D point in collision.contacts)
                {
                    if(point.normal.y >= 0.9f)
                    {
                        LevelManager.instance.ResetCombo();
                        return;
                    }
                }
            }
            return;
        }
        bool hasJumpedOnEnemy = false;
        bool hurtEnemyDuringJump = false;

        if(enemy.canBeJumped) // else you can jump on it so player will be hurt
        {
            foreach(ContactPoint2D point in collision.contacts)
            {
                //Debug.DrawLine(point.point, point.point + point.normal, Color.red,100);
                // Debug.Log(point.normal);
                // if fall into enemy
                if( point.normal.y >= 0.9f)
                {
                  hasJumpedOnEnemy = true;
                }

                // contact with an enemy after the ascendant phase
                if(velocity.y > 0 && Math.Abs(point.normal.x) == 1.0f)
                {
                    hurtEnemyDuringJump = true;
                }
            }
        }
        if(hasJumpedOnEnemy)
        {
            velocity.y = jumpTakeOffSpeed * 0.75f;
            enemy.Hurt(inventory.GetDamage());
            if(enemy.Life == 0)
            {
                inventory.Reload();
            }
        }
        else if(hurtEnemyDuringJump)
        {
            enemy.Hurt(inventory.GetDamage());
            inventory.Reload();
        }
        else
        {
            Hurt(enemy);
        }
    }

    IEnumerator FlashSprite(SpriteRenderer renderer, float minAlpha, float maxAlpha, float interval, float duration)
    {
        Color colorNow = renderer.color;
        Color minColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, minAlpha);
        Color maxColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, maxAlpha);

        float currentInterval = 0;
        while(duration > 0)
        {
            float tColor = currentInterval / interval;
            renderer.color = Color.Lerp(minColor, maxColor, tColor);

            currentInterval += Time.deltaTime;
            if(currentInterval >= interval)
            {
                Color temp = minColor;
                minColor = maxColor;
                maxColor = temp;
                currentInterval = currentInterval - interval;
            }
            duration -= Time.deltaTime;
            yield return null;
        }
        renderer.color = maxColor;
    }

    IEnumerator GetUnvisible(float unvisibleTimer, EnemyBase enemy)
    {
        unvisible = true;
        if(enemy)
        {
            enemy.gameObject.layer = LayerMask.NameToLayer("enemy_hurt");
        }
        yield return new WaitForSeconds(unvisibleTimer);
        unvisible = false;
        if(enemy)
        {
            enemy.gameObject.layer = LayerMask.NameToLayer("enemy");
        }
    }

    void CreateDust()
    {
        dust.Play();
    }

    public void SetToZero()
    {
        velocity = new Vector2(0.0f, 0.0f);
    }
}
