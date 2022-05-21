using DummyClient;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class NetworkManager
{
	bool connected = false;

	ServerSession _session = new ServerSession();
	string host;
	IPHostEntry ipHost;
	IPAddress ipAddr;
	IPEndPoint endPoint;

	Connector connector;

	float time = 0;

	public bool Connected
    {
		get { return connected; }
		set { connected = value; }
    }

	public void Send(ArraySegment<byte> sendBuff)
	{
		ConnectToServer();
		//while (connected == false)
  //      {
		//	time += Time.deltaTime;
		//	if (time >= 3.0f)
  //          {
		//		time = 0;
		//		return;
  //          }
		//	// busy wait
  //      }
		_session.Send(sendBuff);
	}

	public void Init()
    {
        // DNS (Domain Name System)

        host = Dns.GetHostName();
        ipHost = Dns.GetHostEntry(host);
        ipAddr = IPAddress.Parse("3.39.181.102"); // AWS EC2 Instance의 IP 주소를 IPAdress 객체로 변환
        endPoint = new IPEndPoint(ipAddr, 7777);

        //string host = Dns.GetHostName();
        //IPHostEntry ipHost = Dns.GetHostEntry(host);
        //IPAddress ipAddr = ipHost.AddressList[0];
        //endPoint = new IPEndPoint(ipAddr, 7777);

		connector = new Connector();

		//connector.Connect(endPoint,
		//	() => { return _session; },
		//	1);	
	}

	public void ConnectToServer()
    {
		connector.Connect(endPoint,
			() => { return _session; },
			1);
	}

    public void Update()
	{
		List<IPacket> list = PacketQueue.Instance.PopAll();
		foreach (IPacket packet in list)
			PacketManager.Instance.HandlePacket(_session, packet);
	}


}
