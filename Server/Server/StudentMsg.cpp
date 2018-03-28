#include "StudentMsg.h"

void StudentMsg::OnProcess(char* pb, int length)
{
	XNet::Student student;

	if (!student.ParseFromArray(pb, length))
	{
		printf("parse student error");
	}
	else
	{
		cout << "age: " << student.age() << endl;
		cout << "name: " << student.name() << endl;
		cout << "num:" << student.num() << endl;
	}
}


ushort StudentMsg::getuid()
{
	return 1002;
}
