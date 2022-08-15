using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class WebsocketConnector : MonoBehaviour
{
    ClientWebSocket cws;
    // Start is called before the first frame update
    void Start()
    {
        var canToken = new System.Threading.CancellationToken();
        
        cws = new ClientWebSocket();
        var task = cws.ConnectAsync(new System.Uri("ws://127.0.0.1:1337"), canToken);
        StartCoroutine(WaitforTask(task));
        
    }

    async void Run()
    {
        Debug.Log("___ Started the task Run");
        while (cws.State == WebSocketState.Open)
        {
            var byteData = new byte[1024];
            var received = await cws.ReceiveAsync(new ArraySegment<byte>(byteData),CancellationToken.None);
            var t = received;
        }
        Debug.Log("___ Completed the task Run");
    }

    private IEnumerator WaitforTask(Task a_task)
    {
        while(!a_task.IsCompleted)
        {
            yield return null;
        }
        Debug.Log("Coonection completed: "+ a_task.Status + ", cws state: " + cws.State);
        if(cws.State == WebSocketState.Open)
        {
            //var runTask = Task.Factory.StartNew(Run);
        }
    }

    private void OnApplicationQuit()
    {
        if (cws.State == WebSocketState.Open)
        {
            cws.CloseAsync(WebSocketCloseStatus.NormalClosure,string.Empty,System.Threading.CancellationToken.None);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField]
    private TMP_InputField m_input = null;

    public void Send()
    {
        var strData = m_input.text;
        var bytes = Encoding.UTF8.GetBytes(strData);

        var segment = new ArraySegment<byte>(bytes);
        var task = cws.SendAsync(segment, WebSocketMessageType.Text, false, new System.Threading.CancellationToken());
        //StartCoroutine(WaitforTask(task));
    }
}
