using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : MonoBehaviour {
    public float screenTime = 1.5f;
    private float timer;
    public int damage = 7;
    public float velocity;
    public GameObject player;
    public int direction;
	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //BAD DESIGNING, NÃO FAÇAM ISSO EM PROJETOS PROFISSIONAIS DE VOCÊS
        transform.position += new Vector3(Time.deltaTime * velocity * direction, 0, 0);

        timer += Time.deltaTime;
        if (timer > screenTime)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetInstanceID() != player.GetInstanceID())
        {
            collision.GetComponent<Player>().TakeDamage(damage, -1);
        }
    }
}
