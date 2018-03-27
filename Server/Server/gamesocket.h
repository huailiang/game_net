#ifndef  __gamesocket__
#define  __gamesocket__


#include <stdio.h>  
#include <winsock2.h>  
#include "testpb.h"

#pragma comment(lib,"ws2_32.lib")  

#define max 1024

class gamesocket
{
public:
	gamesocket();
	~gamesocket();

	bool init();
	void DO();

private:
	WORD sockVersion;
	WSADATA wsaData;
	SOCKET slisten;
	u_short port;
};


#endif // ! __gamesocket__



