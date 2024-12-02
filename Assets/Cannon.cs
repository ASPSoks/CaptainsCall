using UnityEngine;

public class Cannon : MonoBehaviour
{
    private Rigidbody2D _rb;

    public GameObject cannonballPrefab;
    public float _cannonballSpeed = 7f;
    public float _fireCooldown = 1.5f;
    private float _lastFireTime = 0f;

    public GameObject tripulant;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    public void MoveCannon(Vector2 velocity)
    {
        _rb.velocity = velocity;
    }
    public GameObject FireCannon(Vector2 direction)
    {
        Debug.Log("atirou");
        // Verifica se o cooldown terminou (se passou 1.5 segundos desde o último disparo)
        if (!IsAvailableToShoot())
        {
            return null;
        }


        // Instancia a bola de canhão na posição do player
        GameObject cannonball = Instantiate(cannonballPrefab, transform.position, Quaternion.identity);

        // Atualiza o tempo do último disparo
        _lastFireTime = Time.time;

        // Define a velocidade da bola de canhão na direção do cursor
        cannonball.GetComponent<Rigidbody2D>().velocity = direction * _cannonballSpeed;

        return cannonball;

    }

    public bool IsAvailableToShoot()
    {
        return Time.time >= _lastFireTime + _fireCooldown;
    }

}
