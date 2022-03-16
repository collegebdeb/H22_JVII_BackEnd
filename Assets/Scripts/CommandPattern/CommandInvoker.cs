using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    private static Queue<ICommand> _commandBuffer;

    private void Awake()
    {
        _commandBuffer = new Queue<ICommand>();
    }

    public static void AddCommand(ICommand command)
    {
        _commandBuffer.Enqueue(command);
    }

    // Update is called once per frame
    void Update()
    {
        if (_commandBuffer.Count > 0)
        {
            //ICommand c = _commandBuffer.Dequeue(); 
            //c.Execute();
            //Take the oldest objet how to the queue and give it to us and execute it
            _commandBuffer.Dequeue().Execute();
        }
    }
}
