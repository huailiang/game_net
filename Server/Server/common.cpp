#include "common.h"


void tobytes(std::string str)
{
	size_t len = str.length();
	for (size_t i = 0; i<len; i++)
	{
		printf("0x%x,", (unsigned char)str[i]);
	}
	printf("\n");
}


uint xhash(const char* pChar)
{
	if (pChar == NULL) return 0;
	uint hash = 0;
	size_t len = strlen(pChar);
	for (size_t i = 0; i < len; ++i)
	{
		hash = (hash << 5) + hash + pChar[i];
	}
	return hash;
}

std::string trimLeft(const std::string& str) {
	std::string t = str;
	t.erase(0, t.find_first_not_of(" /t/n/r"));
	return t;
}

std::string trimRight(const std::string& str) {
	std::string t = str;
	t.erase(t.find_last_not_of(" /t/n/r") + 1);
	return t;
}

std::string trim(const std::string& str) {
	std::string t = str;
	t.erase(0, t.find_first_not_of(" /t/n/r"));
	t.erase(t.find_last_not_of(" /t/n/r") + 1);
	return t;
}

std::vector<std::string> split(const std::string& str, const char sep)
{
	return split(str, tostring(sep));
}

std::vector<std::string> split(const std::string& srcstr, const std::string& delimeter)
{
	std::vector<std::string> ret(0);//use ret save the spilted reault
	if (srcstr.empty())    //judge the arguments
	{
		return ret;
	}
	std::string::size_type pos_begin = srcstr.find_first_not_of(delimeter);//find first element of srcstr

	std::string::size_type dlm_pos;//the delimeter postion
	std::string temp;              //use third-party temp to save splited element
	while (pos_begin != std::string::npos)//if not a next of end, continue spliting
	{
		dlm_pos = srcstr.find(delimeter, pos_begin);//find the delimeter symbol
		if (dlm_pos != std::string::npos)
		{
			temp = srcstr.substr(pos_begin, dlm_pos - pos_begin);
			pos_begin = dlm_pos + delimeter.length();
		}
		else
		{
			temp = srcstr.substr(pos_begin);
			pos_begin = dlm_pos;
		}
		if (!temp.empty())
			ret.push_back(temp);
	}
	return ret;
}


bool startsWith(const std::string& str, const std::string& substr) {
	return str.find(substr) == 0;
}

bool endsWith(const std::string& str, const std::string& substr) {
	return str.rfind(substr) == (str.length() - substr.length());
}


int new_id()
{
	return id++;
}


bool isNumber(const std::string& value)
{
	const char* str = value.c_str();
	while ((*str++ != 0) && (*str >= '0') && (*str <= '9'));
	return *str == 0;
}


int countUTF8Char(const std::string &s)
{
	const int mask = 0;
	int Count = 0;
	for (size_t i = 0; i < s.size(); ++i)
	{
		unsigned char c = (unsigned char)s[i];
		if (c <= 0x7f || c >= 0xc0)
		{
			++Count;
		}
	}
	return Count;
}