#ifndef  __common__
#define  __common__

#include <string>
#include <sstream>
#include <fstream>
#include <stdlib.h>
#include <vector>

typedef unsigned short ushort;
typedef unsigned int  uint;
typedef unsigned long long ulong;


template<typename T>
std::string tostring(T val)
{
	std::stringstream ss;
	std::string str;
	ss << val;
	ss >> str;
	return str;
}

#define Random(min,max) (rand() % max + min) //[min,max)

static int id = 0;

void tobytes(std::string str);

uint xhash(const char* ch);

std::vector<std::string> split(const std::string& str, const char sep);

std::vector<std::string> split(const std::string& srcstr, const std::string& delimeter);

std::string trimLeft(const std::string& str);

std::string trimRight(const std::string& str);

std::string trim(const std::string& str);

bool startsWith(const std::string& str, const std::string& substr);

bool endsWith(const std::string& str, const std::string& substr);

int new_id();

bool isNumber(const std::string& value);

int countUTF8Char(const std::string &s);


#endif // ! __common__


