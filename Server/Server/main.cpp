#include <stdio.h>  
#include "testpb.h"
#include "gamesocket.h"

int main(int argc, char* argv[])
{
	testpb test;
	test.serial();
	
	gamesocket sock;
	sock.DO();

	system("pause");
	return 0;
}