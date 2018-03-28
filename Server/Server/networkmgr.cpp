#include "networkmgr.h"
#include "PeopleMsg.h"
#include "StudentMsg.h"


networkmgr::networkmgr()
{
	regist();
}

networkmgr::~networkmgr()
{
}

void networkmgr::unload()
{
	std::unordered_map<ushort, protomsg*>::iterator iter = mp.begin();
	for (; iter != mp.end(); iter++)
	{
		delete iter->second;
	}
}


void networkmgr::regist()
{
	innerregist(new PeopleMsg());
	innerregist(new StudentMsg());
}


void networkmgr::innerregist(protomsg* msg)
{
	mp.insert(std::make_pair(msg->getuid(), msg));
}


void networkmgr::process(ushort uid, char* pb, int len)
{
	if (mp.find(uid) != mp.end())
	{
		mp[uid]->OnProcess(pb, len);
	}
}