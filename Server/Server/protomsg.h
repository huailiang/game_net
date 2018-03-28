#ifndef  __protomsg__
#define __protomsg__


#include "common.h"


class protomsg
{
public:
	virtual void OnProcess(char* pb, int length) = 0;
	virtual ushort getuid() = 0;
};


#endif //  __protomsg__



