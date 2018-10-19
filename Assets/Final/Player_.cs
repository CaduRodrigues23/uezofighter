using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ : MonoBehaviour {
    public string name = "";
    public int id;
    public static int avgHp = 20;
    private float hp;
    private int state = 0;
    
    public GameObject special;

    public KeyCode keyUp;
    public KeyCode keyLeft;
    public KeyCode keyRight;
    public KeyCode keyHit;
    public KeyCode keySpecial;
    private int direction = 1; //se ele estiver indo à direita

    public float specialTime = 1;
    private float specialTimer;

    public int getHp()
    {
        return Mathf.CeilToInt(hp);
    }

    public int getState()
    {
        return state;
    }

    public int getDirection()
    {
        return direction;
    }

    //Dica> fazer o método de reconhecimento de comandos como uma pilha

	// Use this for initialization
	void Start () {
        hp = avgHp;
        state = 0;

        specialTimer = 0;

        if (transform.GetComponent<SpriteRenderer>().flipX)
        {
            direction = -1;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        //funcoes de comando


        if (Input.GetKeyDown(keyUp))
        {
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 7f), ForceMode2D.Impulse);
        }
        else if (Input.GetKey(keyLeft))
        {
            transform.GetComponent<SpriteRenderer>().flipX = true;
            direction = -1;
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.2f, 0), ForceMode2D.Impulse);
        }
        else if (Input.GetKey(keyRight))
        {
            transform.GetComponent<SpriteRenderer>().flipX = false;
            direction = 1;
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.2f, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(keyHit) && Judge.HitPermission())
        {
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(4f * direction, 0), ForceMode2D.Impulse);
            state = 1;
            
        } else
        {
            state = 0;
        }

        if (Input.GetKey(keySpecial) && specialTimer < specialTime)
        {
            specialTimer += Time.deltaTime;
            if (specialTimer > specialTime)
            {
                Shoot();
            }
        } else
        {
            if (specialTimer < specialTime)
            {
                specialTimer = 0;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player")) //garantir que o objeto que está colidindo com este tem um script Player
        {
            if (collision.transform.GetComponent<Player>().getState() == 1 && state == 0)
            {
                Player enemy = collision.transform.GetComponent<Player>();
                Debug.Log(name + " being hit by " + enemy.name);
                collision.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(5f * direction, 0), ForceMode2D.Impulse);
                transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(1f * -direction, 0), ForceMode2D.Impulse);
                TakeDamage(1, enemy.getHp());
            }
            
        }
    }

    private void Shoot()
    {
        GameObject obj = (GameObject)Instantiate(special, gameObject.transform);
        obj.transform.localPosition = new Vector3(3.54f * direction, 0, 0);
        if (direction == -1)
        {
            obj.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        }
        obj.GetComponent<Special>().player = gameObject;
        obj.GetComponent<Special>().direction = direction;
    }

    public void TakeDamage(int damage, int enemyHp)
    {
        if (enemyHp == -1)
        {
            hp -= damage;
        }
        else
        {
            hp -= damage + damage * (((enemyHp) / avgHp) * 1.5f);
        }

        if (hp <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Debug.Log(name + " was Killed");
        hp = avgHp;
        state = 0;
        FindObjectOfType<Judge>().UpdateScore(gameObject);
    }

    public float GetHPRelative()
    {
        return hp / avgHp;
    }
}
