using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    public List<GameObject> Essences;
    public GameObject Spike;
    float _spikeRatio = 0.2f;
    const int _essenceQuantity = 6;
    float _nextTime;
    const float _LOWERTIME = 0.016f, _UPPERTIME = 2f;
    const float VERTICALUPPERLIMIT = 4f, VERTICALLOWERLIMIT = -4f;
    GameObject _newObject;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstantiatorCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator InstantiatorCoroutine()
    {
        _nextTime = Random.Range(_LOWERTIME, _UPPERTIME);
        
        while(true)
        {
            yield return new WaitForSeconds(_nextTime);

            if (Random.Range(0f, 1f) <= _spikeRatio % 3)
                _newObject = Spike;
            else
                _newObject = Essences[Random.Range(0, 6)];

            Instantiate(_newObject, new Vector3(11f, Random.Range(VERTICALLOWERLIMIT, VERTICALUPPERLIMIT)), Quaternion.identity);

            _spikeRatio *= 1.1f;
        }
    }
}
