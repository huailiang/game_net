#ifndef __networkmgr__
#define __networkmgr__

#include "common.h"
#include "protomsg.h"
#include "sington.h"
#include "gamesocket.h"

class networkmgr :public sington<networkmgr>
{
public:
	networkmgr();
	~networkmgr();
	void init();
	void regist();
	void process(ushort uid, char* pb, int len);
	void unload();
	void send(protomsg* msg);

private:
	std::unordered_map<ushort, protomsg*> mp;
	void innerregist(protomsg* msg);
	gamesocket* sock;
};


#endif // !__networkmgr


