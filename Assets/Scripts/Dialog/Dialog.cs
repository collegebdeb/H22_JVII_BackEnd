using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog
{
    private SDialog _content;

    public SDialog Content
    {
        get => _content;
        set => _content = value;
    }

    public Dialog(SDialog content)
    {
        Content = content;
    }
}
