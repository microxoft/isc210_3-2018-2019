using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatPlayerBehaviour : MonoBehaviour
{
    public int HitPoints;
    private Animator animator;
    float _speed = 8f;
    Vector3 _deltaPos;
    const float VERTICALUPPERLIMIT = 4f, VERTICALLOWERLIMIT = -4f;
    const int _startingHitPoints = 3;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        HitPoints = _startingHitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        _deltaPos = new Vector3(0, Input.GetAxis("Vertical") * _speed * Time.deltaTime);

        animator.SetFloat("Orientation", _deltaPos.y);
        Debug.Log(_deltaPos.y);

        gameObject.transform.Translate(_deltaPos);
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            Mathf.Clamp(gameObject.transform.position.y, VERTICALLOWERLIMIT, VERTICALUPPERLIMIT));
    }

    public void OnHitted()
    {
        HitPoints--;
        Destroy(GameObject.Find("HitPoints").transform.GetChild(0).gameObject);

        if(HitPoints == 0)
        {
            // Game over!
            Destroy(gameObject);
        }
    }
}
