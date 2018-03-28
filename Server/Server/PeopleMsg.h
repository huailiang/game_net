#ifndef  __peoplemsg__
#define  __peoplemsg__

#include "protomsg.h"
#include "gamesocket.h"

class PeopleMsg :public protomsg
{
public:
	void OnProcess(char* pb, int length);
	ushort getuid();
	char* getbuff();
	PeopleMsg();
	~PeopleMsg();
	void set(XNet::People* p);
private:
	XNet::People* ptr;

};



#endif // ! _peoplemsg__


