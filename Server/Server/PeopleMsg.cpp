#include "PeopleMsg.h"
#include "networkmgr.h"

PeopleMsg::PeopleMsg()
{
}

PeopleMsg::~PeopleMsg()
{
	delete ptr;
}

void PeopleMsg::set(XNet::People* p)
{
	this->ptr = p;
}

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

	// reply to  client
	XNet::People* p = new XNet::People();
	p->set_email("rectvv@gmail.com");
	p->set_id(10086);
	p->set_name("Rect");
	p->add_snip(2);
	p->add_snip(4);
	XNet::People_PhoneNumber* ph = p->add_phones();
	ph->set_number("522-123");
	ph->set_type(XNet::People_PhoneType_HOME);
	set(p);
	networkmgr::Instance()->send(this);
}

ushort PeopleMsg::getuid()
{
	return 1001;
}

char* PeopleMsg::getbuff()
{
	assert(ptr);
	buffsize = ptr->ByteSize();
	void* buff = malloc(buffsize);
	ptr->SerializeToArray(buff, buffsize);
	//for (size_t i = 0; i < buffsize; i++)
	//{
	//	cout << (ushort)*((unsigned char*)buff + (int)i) << " ";
	//}
	//cout << endl;
	return (char*)buff;
}