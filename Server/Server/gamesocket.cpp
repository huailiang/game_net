#include "gamesocket.h"


gamesocket::gamesocket()
{
	port = 6000;
	this->init();
}


gamesocket::~gamesocket()
{
}


bool gamesocket::init()
{
	//初始化WSA  
	sockVersion = MAKEWORD(2, 2);
	if (WSAStartup(sockVersion, &wsaData) != 0)
	{
		return false;
	}

	//创建套接字  
	slisten = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (slisten == INVALID_SOCKET)
	{
		printf("socket error !");
		return false;
	}
	return true;
}

void gamesocket::DO()
{
	//绑定IP和端口  
	sockaddr_in sin;
	sin.sin_family = AF_INET;
	sin.sin_port = htons(port);
	sin.sin_addr.S_un.S_addr = INADDR_ANY;
	if (bind(slisten, (LPSOCKADDR)&sin, sizeof(sin)) == SOCKET_ERROR)
	{
		printf("bind error !");
	}

	//开始监听  
	if (listen(slisten, 5) == SOCKET_ERROR)
	{
		printf("listen error !");
		return;
	}

	//循环接收数据  
	SOCKET sClient;
	sockaddr_in remoteAddr;
	int nAddrlen = sizeof(remoteAddr);
	char revData[max_buff];
	while (true)
	{
		printf("\n等待连接...\n");
		sClient = accept(slisten, (SOCKADDR *)&remoteAddr, &nAddrlen);
		if (sClient == INVALID_SOCKET)
		{
			printf("accept error !");
			continue;
		}
		printf("接受到一个连接：%s \r\n", inet_ntoa(remoteAddr.sin_addr));

		//接收数据  
		int ret = recv(sClient, revData, max_buff, 0);
		if (ret > 0)
		{
			revData[ret] = 0x00;
			XNet::People person;
			char* pb = ((char*)revData + len_head);

			char head[len_head];
			for (int i = 0; i < len_head; i++)
			{
				head[i] = revData[i];
			}
			short shead = (short)(*head);
			cout << "head is:"<< shead << endl;
			if (!person.ParseFromArray(pb, ret - len_head))
			{
				printf("parse person error");
			}
			else
			{
				cout << "id: " << person.id() << endl;
				cout << "name: " << person.name() << endl;
				cout << "email:" << person.email() << endl;
			}
		}

		//发送数据  
		const char * sendData = "hello,TCP client. The message is from server!";
		send(sClient, sendData, strlen(sendData), 0);
		closesocket(sClient);
	}

	closesocket(slisten);
	WSACleanup();
}