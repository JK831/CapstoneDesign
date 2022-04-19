using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject root = GameObject.Find("@PacketEvent");
        if (root == null)
        {
            root = new GameObject() { name = "@PacketEvent" };
            root.AddComponent<PacketEvent>();

        }
        
        DontDestroyOnLoad(root);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendRequestChallengeTop30()
    {
        C_RequestChallengeTop30 packet = new C_RequestChallengeTop30();

        packet.UId = Managers.User.UID;

        Managers.Network.Send(packet.Write());
    }

    public void SendRequestMyChallengeRanking()
    {
        C_RequestMyChallengeRanking packet = new C_RequestMyChallengeRanking();

        packet.UId = Managers.User.UID;

        Managers.Network.Send(packet.Write());
    }
}
