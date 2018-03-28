#include "StudentMsg.h"
#include "networkmgr.h"

StudentMsg::StudentMsg()
{
}

StudentMsg::~StudentMsg()
{
	delete ptr;
}

void StudentMsg::set(XNet::Student* p)
{
	this->ptr = p;
}

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

	//reply to  client
	set(&student);
	networkmgr::Instance()->send(this);
}


ushort StudentMsg::getuid()
{
	return 1002;
}


char* StudentMsg::getbuff()
{
	assert(ptr);
	buffsize = ptr->ByteSize();
	void* buff = malloc(buffsize);
	ptr->SerializeToArray(buff, buffsize);
	return (char*)buff;
}

