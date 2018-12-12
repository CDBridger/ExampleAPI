#pragma once
#include <vector>
#include "Vector3.h"
extern "C" class ExampleAPI
{
private:
	std::vector<Vector3C> _vectors;
public:
	ExampleAPI() : _vectors{ Vector3C(1,1,1), Vector3C(2,2,2), Vector3C(3,3,3) } {};
	ExampleAPI(std::vector<Vector3C> vecs);
	~ExampleAPI();
	void PassInVectors(std::vector<Vector3C> vecs);
	std::vector<Vector3C>* GetVectors();
};

extern "C" {
	MYAPI ExampleAPI * CreateExample();
	MYAPI void DeleteExample(ExampleAPI * handler);
	MYAPI void PassInVectors(ExampleAPI * handler, Vector3C * vecs, int len);
	MYAPI Vector3C * GetVectors(ExampleAPI * handler, int* len);
}