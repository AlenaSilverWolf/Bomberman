using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Explosion : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            if (PlayerPrefs.GetInt("Lives") <= 1)
                StartCoroutine(KillHero(collision.gameObject, "GameOver"));
            else
            {
                PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives") - 1);
                StartCoroutine(KillHero(collision.gameObject, "Game"));
            }
        }
        if (collision.gameObject.tag == "Enemy")
            StartCoroutine(KillEnemy(collision.gameObject));
        if (collision.gameObject.tag == "Ice")
        {
            collision.gameObject.GetComponent<Animator>().SetBool("Destroy", true);

            if (collision.gameObject.GetComponent<BoxCollider2D>() != null)
                Destroy(collision.gameObject.GetComponent<BoxCollider2D>());
            if (collision.gameObject.GetComponent<Block>() != null)
                Destroy(collision.gameObject.GetComponent<Block>());

            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Box")
            Destroy(gameObject);
    }

    IEnumerator KillHero(GameObject col, string txtScene)
    {
        col.gameObject.GetComponent<Animator>().SetBool("IsDead", true);
        if (col.GetComponent<Rigidbody2D>() != null)
            Destroy(col.GetComponent<Rigidbody2D>());
        if (col.GetComponent<BoxCollider2D>() != null)
            Destroy(col.GetComponent<BoxCollider2D>());

        yield return new WaitForSeconds(0.05f);
        Destroy(col);
        SceneManager.LoadSceneAsync(txtScene);
    }
    IEnumerator KillEnemy(GameObject col)
    {
        if (col.GetComponent<Rigidbody2D>() != null)
            Destroy(col.GetComponent<Rigidbody2D>());
        if (col.GetComponent<BoxCollider2D>() != null)
            Destroy(col.GetComponent<BoxCollider2D>());

        FieldGenerator.ChEnemy("Enemies: " + (GameObject.FindGameObjectsWithTag("Enemy").Length - 1));
        FieldGenerator.ChScore(100);
        col.gameObject.GetComponent<Animator>().SetBool("IsDead", true);
        yield return new WaitForSeconds(0.1f);
        Destroy(col);
    }
}