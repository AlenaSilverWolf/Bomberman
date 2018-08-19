using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public GameObject bomb;
    public GameObject explosion;
    public GameObject expUp1;
    public GameObject expDown1;
    public GameObject expLeft1;
    public GameObject expRight1;
    float speed = 3;
    bool up = false;
    bool down = false;
    bool left = false;
    bool right = false;

    private void FixedUpdate()
    {
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (item.GetComponent<Animator>().GetBool("IsDead") == true)
                StartCoroutine(CheckEmeny((item)));
        }
        if (hero != null && hero.GetComponent<Animator>().GetBool("IsDead") == true)
            StartCoroutine(CheckHero());

        if (hero == null || hero.GetComponent<Rigidbody2D>() == null)
            return;
        if (left)
        {
            hero.GetComponent<Animator>().SetInteger("Direction", 1);
            hero.GetComponent<Rigidbody2D>().MovePosition(new Vector2((hero.transform.position.x - 1 * speed * Time.deltaTime), (hero.transform.position.y)));
        }
        else if (right)
        {
            hero.GetComponent<Animator>().SetInteger("Direction", 2);
            hero.GetComponent<Rigidbody2D>().MovePosition(new Vector2((hero.transform.position.x + 1 * speed * Time.deltaTime), (hero.transform.position.y)));
        }
        else if (up)
        {
            hero.GetComponent<Animator>().SetInteger("Direction", 3);
            hero.GetComponent<Rigidbody2D>().MovePosition(new Vector2((hero.transform.position.x), (hero.transform.position.y + 1 * speed * Time.deltaTime)));
        }
        else if (down)
        {
            hero.GetComponent<Animator>().SetInteger("Direction", 4);
            hero.GetComponent<Rigidbody2D>().MovePosition(new Vector2((hero.transform.position.x), (hero.transform.position.y - 1 * speed * Time.deltaTime)));
        }
        else
            hero.GetComponent<Animator>().SetInteger("Direction", 0);
    }
    public void MoveLeft()
    {
        left = true;
    }
    public void MoveRight()
    {
        right = true;
    }
    public void MoveUp()
    {
        up = true;
    }
    public void MoveDown()
    {
        down = true;
    }
    public void Stay()
    {
        up = false;
        down = false;
        left = false;
        right = false;
    }

    public void CreateBomb()
    {
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        if (hero == null)
            return;
        if (!GameObject.FindWithTag("Bomb"))
        {
            Instantiate(bomb, hero.transform.position, Quaternion.identity, hero.transform.parent);
            StartCoroutine(Explode());
        }
    }
    IEnumerator Explode()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject.FindWithTag("Bomb").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);

        GameObject.FindWithTag("Bomb").GetComponent<Animator>().SetBool("Destroy", false);
        Instantiate(explosion, GameObject.FindGameObjectWithTag("Bomb").transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Bomb").transform.parent);
        Destroy(GameObject.FindWithTag("Bomb"));
        yield return new WaitForSeconds(0.05f);

        Instantiate(expDown1, GameObject.FindGameObjectWithTag("Exp0").transform.position - new Vector3(0, 0.5f), Quaternion.identity, GameObject.FindGameObjectWithTag("Exp0").transform.parent);
        Instantiate(expUp1, GameObject.FindGameObjectWithTag("Exp0").transform.position - new Vector3(0, -0.5f), Quaternion.identity, GameObject.FindGameObjectWithTag("Exp0").transform.parent);
        Instantiate(expLeft1, GameObject.FindGameObjectWithTag("Exp0").transform.position - new Vector3(0.5f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Exp0").transform.parent);
        Instantiate(expRight1, GameObject.FindGameObjectWithTag("Exp0").transform.position - new Vector3(-0.5f, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Exp0").transform.parent);

        yield return new WaitForSeconds(0.5f);

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Exp1"))
        {
            Destroy(item);
        }
        yield return new WaitForSeconds(0.1f);

        Destroy(GameObject.FindGameObjectWithTag("Exp0"));
    }

    IEnumerator CheckHero()
    {
        yield return new WaitForSeconds(2.5f);
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        if (hero != null)
        {
            string txtScene = "";
            if (PlayerPrefs.GetInt("Lives") <= 1)
                txtScene = "GameOver";
            else
            {
                PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives") - 1);
                txtScene = "Game";
            }
            Destroy(hero);
            SceneManager.LoadSceneAsync(txtScene);
        }
    }
    IEnumerator CheckEmeny(GameObject enemy)
    {
        yield return new WaitForSeconds(2.5f);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (item == enemy)
                Destroy(enemy);
        }
        FieldGenerator.ChEnemy("Enemies: " + (GameObject.FindGameObjectsWithTag("Enemy").Length));
    }
}