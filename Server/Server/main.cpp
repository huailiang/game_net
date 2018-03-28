#include <stdio.h>  
#include "testpb.h"
#include "networkmgr.h"


int main(int argc, char* argv[])
{
	testpb test;
	test.serial();
	
	networkmgr::Instance()->init();

	system("pause");
	return 0;
}