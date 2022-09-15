using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueQueue
{
    private Phrase[] _phrases;
    private int front;
    private int rear;
    private int capacity;

    public DialogueQueue(DialogueSequence sequence)
    {
        _phrases = new Phrase[sequence.Phrases.Length];
        front = -1;
        rear = -1;
        capacity = sequence.Phrases.Length;
    }
    public bool IsFull()
    {
        return front == 0 && rear == capacity - 1;
    }
    public bool IsEmpty()
    {
        return front == -1;
    }
    public void Enqueue(Phrase phrase)
    {
        if (IsFull())
        {
            Debug.Log("Queue is full.");
        }
        else
        {
            if (front == -1)
            {
                front = 0;
            }
            _phrases[++rear] = phrase;
        }
    }
    public Phrase Dequeue()
    {
        if (IsEmpty())
        {
            Debug.Log("Queue is empty.");
            return null;
        }
        var phrase = _phrases[front]; //Return current
        if (front >= rear) //If it's the last element, reset queue.
        {
            front = -1;
            rear = -1;
        }
        else
        {
            front++;
        }

        return phrase;
    }
    public void ShowQueue()
    {
        if (IsEmpty())
        {
            Debug.Log("Queue is empty.");
        }
        else
        {
            Debug.Log($"Front: {front}");
            Debug.Log($"Items:");
            for (int i = front; i < rear+1; i++)
            {
                Debug.Log(_phrases[i].Sentence);
            }

            Debug.Log($"Tail: {rear}");
        }
    }
}
