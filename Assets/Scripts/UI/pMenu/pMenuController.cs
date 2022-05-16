using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class pMenuController : MonoBehaviour
{
    private int _numBoutons;
    private pMenuBouton[] _boutons;

    [SerializeField] private string _upButton;
    [SerializeField] private string _downButton;
    [SerializeField] private string _selectButton;

    private int _indexSelection;

    private enum Direction
    {
        up,
        down
    };

    void Start()
    {
        _numBoutons = gameObject.transform.childCount;
        _boutons = new pMenuBouton[_numBoutons];

        for (int i = 0; i < (_numBoutons); i++)
        {
            _boutons[i] = transform.GetChild(i).gameObject.GetComponent<pMenuBouton>();
        }
        
        _boutons[0].ChangeStatut(true);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(_upButton))
        {
            IndexSelection(Direction.up);
        }
        else if (Input.GetKeyDown(_downButton))
        {
            IndexSelection(Direction.down);
        }
        else if (Input.GetKeyDown(_selectButton))
        {
            _boutons[_indexSelection].TriggerButton();
        }
    }
    private void IndexSelection(Direction direction)
    {
        _boutons[_indexSelection].ChangeStatut(false);
        if (direction == Direction.down)
        {
            if (_indexSelection < (_boutons.Length - 1))
            {
                _indexSelection++;
            }
            else if (_indexSelection == (_boutons.Length - 1))
            {
                _indexSelection = 0;
            }
        }
        else if (direction == Direction.up)
        {
            if (_indexSelection == 0)
            {
                _indexSelection = _boutons.Length - 1;
            }
            else if (_indexSelection > 0)
            {
                _indexSelection--;
            }
        }
        _boutons[_indexSelection].ChangeStatut(true);
    }
}
