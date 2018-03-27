#include "testpb.h"



testpb::testpb()
{
}


testpb::~testpb()
{
}



void testpb::serial()
{
	XNet::People person;

	person.set_email("penghuailiang@126.com");
	person.set_id(123);
	person.set_name("huailiang");
	for (size_t i = 0; i < 3; i++)
	{
		person.add_snip(i + 2);
	}

	fstream output("data.bytes", ios::out | ios::trunc | ios::binary);
	if (!person.SerializePartialToOstream(&output))
	{
		cerr << "serializer error" << endl;
	}
	output.close();
	/*cout << person.name() << endl;
	cout << person.email() << endl;*/
	deserial();
}


void testpb::deserial()
{
	XNet::People person;
	fstream in("data.bytes", ios::in | ios::binary);
	if (!person.ParseFromIstream(&in))
	{
		cerr << "failed to parse data.bytes";
		system("pause");
		exit(1);
	}

	cout << "id:" << person.id() << endl;
	cout << "name:" << person.name() << endl;
	cout << "email:" << person.email() << endl;
	cout << "snip count:" << person.snip_size() << endl;
	for (int i = 0; i < person.snip_size(); i++)
	{
		cout << person.snip(i) << endl;
	}
	in.close();
}