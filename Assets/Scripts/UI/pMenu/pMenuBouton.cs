using UnityEngine;
using UnityEngine.Events;

public class pMenuBouton : MonoBehaviour
{
    [SerializeField] private Sprite _hover;

    private Sprite _default;
    private SpriteRenderer _spriteRenderer;

    public UnityEvent onPMenuButton;
    
    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _default = _spriteRenderer.sprite;
        ChangeStatut(false);
    }
    
    public void ChangeStatut(bool isHover)
    {
        if (isHover == true)
        {
            _spriteRenderer.sprite = _hover;
        }
        else
        {
            _spriteRenderer.sprite = _default;
        }
    }

    public void TriggerButton()
    {
        onPMenuButton?.Invoke();
    }
}
