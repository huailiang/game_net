#ifndef  __protomsg__
#define __protomsg__


#include "common.h"


class protomsg
{
public:
	virtual void OnProcess(char* pb, int length) = 0;
	virtual ushort getuid() = 0;
	virtual char* getbuff() = 0;
	int getbuffSize();

protected:
	int buffsize;
};


#endif //  __protomsg__



