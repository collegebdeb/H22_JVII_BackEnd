using UnityEngine;

public class AudioLinesControl : MonoBehaviour
{
    public float _startScale, _scaleMultiplier, _maxScale;
    
    private int _numLines;

    [SerializeField] private GameObject[] _lines;
    [SerializeField] private int[] _band;

    void Start()
    {
        _numLines = gameObject.transform.childCount;
        
        _lines = new GameObject[_numLines];
        _band = new int[_numLines];
        
        for (int i = 0; i < (_numLines); i++)
        {
            _lines[i] = transform.GetChild(i).gameObject;
            _band[i] = (i * 1);
        }
    }
    
    void Update()
    {
        for (int i = 0; i < (_numLines); i++)
        {
            float scale = (AudioPeer._freqBand[_band[i]] * _scaleMultiplier) + _startScale;
            scale = Mathf.Clamp(scale, _startScale, _maxScale);
            _lines[i].transform.localScale = new Vector3(transform.localScale.x,scale, transform.localScale.z);
        }
    }
}
