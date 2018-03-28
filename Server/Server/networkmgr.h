#ifndef __networkmgr__
#define __networkmgr__

#include "common.h"
#include "protomsg.h"
#include "sington.h"

class networkmgr:public sington<networkmgr>
{
public:
	networkmgr();
	~networkmgr();
	 void regist();
	 void process(ushort uid, char* pb, int len);
	 void unload();

private:
	std::unordered_map<ushort,protomsg*> mp;
	void innerregist(protomsg* msg);
};



#endif // !__networkmgr


