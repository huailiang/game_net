#ifndef  __gamesocket__
#define  __gamesocket__


#include <stdio.h>  
#include <winsock2.h>  
#include "testpb.h"

#pragma comment(lib,"ws2_32.lib")  

#define max_buff 1024
#define len_head 2

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



