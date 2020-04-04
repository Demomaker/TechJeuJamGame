using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatController : MonoBehaviour
{
    private Coroutine messageCoroutine;
    private Queue<Message> messageQueue = new Queue<Message>();
    private Message currentMessage = null;
    [SerializeField] private TextMeshProUGUI personTalkingText;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject canvas;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        canvas.SetActive(false);
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        StopCoroutine(messageCoroutine);
    }
    public void QueueMessage(Message message) 
    {
        messageQueue.Enqueue(message);
        if(messageCoroutine == null) messageCoroutine = StartCoroutine(ShowMessagesCoroutine());
    }

    private IEnumerator ShowMessagesCoroutine() 
    {
        while(messageQueue.Count > 0) 
        {
            if(canvas.activeSelf == false)
            canvas.SetActive(true);
            currentMessage = messageQueue.Dequeue();
            personTalkingText.text = currentMessage.PersonTalkingName;
            text.text = currentMessage.MessageText;
            yield return new WaitForSeconds(currentMessage.MessageLengthInSeconds);
        }

        canvas.SetActive(false);
        messageCoroutine = null;
    }

    public void ClearQueue() 
    {
        if(messageCoroutine != null)
        StopCoroutine(messageCoroutine);
        messageCoroutine = null;
        messageQueue.Clear();
        canvas.SetActive(false);
    }
}