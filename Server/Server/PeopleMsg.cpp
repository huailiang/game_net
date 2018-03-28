#include "PeopleMsg.h"

void PeopleMsg::OnProcess(char* pb, int length)
{

	XNet::People person;

	if (!person.ParseFromArray(pb, length))
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


ushort PeopleMsg::getuid()
{
	return 1001;
}