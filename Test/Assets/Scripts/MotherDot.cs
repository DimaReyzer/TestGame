using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherDot : MonoBehaviour
{
    [SerializeField]
    private Dot dot;
    public enum teams // К какой команде принадлежит MotherDot
    {
        Team1,
        Team2
    }
    public teams team;
    public List<Dot> childDots;
    public int HP;

    protected Renderer renderer;
    private float timer;
    protected void OnEnable()
    {
        HP = 20;
        renderer = GetComponent<Renderer>();
        if(team == teams.Team1)
        {
            renderer.material.color = Color.blue;
        }else{
            renderer.material.color = Color.red;
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (HP <= 0) // Если ХП заканчиваются, то дот уничтожается
        {
            Destroy(this.gameObject);
        }
        if (timer >= 1.0f) // Если прошло больше N секунд, то появляется дочерний класс Dot
        {
            Dot localDot = Instantiate<Dot>(dot);
            localDot.transform.position = transform.position;
            localDot.transform.Translate(Vector3.left * Random.Range(1.0f, 5.0f));
            localDot.transform.RotateAround(transform.position, -Vector3.forward, Random.Range(0.0f, 360.0f));
            localDot.team = this.team;
            localDot.motherDot = this;
            childDots.Add(localDot);
            timer = 0.0f;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Dot>() != null && collision.gameObject.GetComponent<Dot>().team != team)
        {
            HP--;
            Destroy(collision.gameObject);
        }
    }
    public void Attack(MotherDot motherDot)
    {
        for(int i = 0; i < childDots.Count; i++)
        {
            childDots[i].targetDot = motherDot;
        }
    }
}
