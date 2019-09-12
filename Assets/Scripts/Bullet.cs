using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float Lifetime = 10f;

    [SerializeField]
    private float Speed = 0.1f;

    [SerializeField]
    private GameObject ExplosionPrefab;

    private Transform aim;
    private Vector3 pos;
    private float startingDistance;

    public void Fly(Vector3 dir, Transform aim)
    {
        this.aim = aim;
        //pos = aim.position;
        GetComponent<Rigidbody>().AddForce(dir, ForceMode.Force);
        startingDistance = Vector3.Distance(aim.transform.position, transform.position);
        Destroy(gameObject, Lifetime);
    }


    public void Fly(Vector3 dir, Vector3 aim)
    {
        this.pos = aim;
        //pos = aim.position;
        GetComponent<Rigidbody>().AddForce(dir, ForceMode.Force);
        startingDistance = Vector3.Distance(pos, transform.position);
        Destroy(gameObject, Lifetime);
    }

    private void Update()
    {
        GetComponent<Rigidbody>().AddForce((pos - transform.position).normalized * Speed * Mathf.Pow((startingDistance/Vector3.Distance(pos, transform.position)), 4), ForceMode.Force);
       // GetComponent<Transform>().Translate((pos - transform.position).normalized*Speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(ExplosionPrefab, collision.GetContact(0).point, Quaternion.identity);
        Destroy(gameObject);
    }
}