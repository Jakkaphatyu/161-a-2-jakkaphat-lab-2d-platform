using UnityEngine;

public class Croccodile : Enemy, IShootable
{
    [SerializeField] private float atkRange;
    public Player player;

    [field: SerializeField] public GameObject Bullet {  get; set; }
    [field: SerializeField] public Transform ShootPoint { get; set; }

    public float ReloadTime { get; set; }
    public float WaitTime { get; set; }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Intialize(50);
        DamageHit = 30;

        atkRange = 6.0f;
        player = GameObject.FindFirstObjectByType<Player>();

        WaitTime = 0.0f;
        ReloadTime = 5.0f;
    }

    private void FixedUpdate()
    {
        WaitTime += Time.fixedDeltaTime;
        Behavior();
    }


    public override void Behavior()
    {
        Vector2 distance = transform.position - player.transform.position;
        if (distance.magnitude <= atkRange)
        {
            Debug.Log($"{player.name} is in the {this.name}'s atk range!");
            Shoot();
        }
    }
    
    public void Shoot() 
    {
        if (Bullet != null)
        {
            if (WaitTime >= ReloadTime)
            {
                anim.SetTrigger("Shoot");
                var bullet = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
                Rock rock = bullet.GetComponent<Rock>();
                if (rock != null)
                    rock.InitWeapon(30, this);
                WaitTime = 0.0f;
            }
            
        }
        
    }
}
