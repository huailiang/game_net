#ifndef __sington__
#define __sington__

template <typename T>
class sington
{
protected:
	sington() {}
	virtual ~sington() {}

public:
	static T* Instance()
	{
		if (m_instance == 0)
		{
			m_instance = new T();
		}
		return m_instance;
	}

	static void DestroyInstance()
	{
		if (m_instance)
		{
			delete m_instance;
			m_instance = 0;
		}
	}

protected:
	static T* m_instance;
};

template <typename T> T* sington<T>::m_instance = 0;

#endif