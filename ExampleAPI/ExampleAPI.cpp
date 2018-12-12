#include "ExampleAPI.h"
#include <algorithm>

ExampleAPI::ExampleAPI(std::vector<Vector3C> vecs)
{
	_vectors = vecs;
}

ExampleAPI::~ExampleAPI()
{
}

void ExampleAPI::PassInVectors(std::vector<Vector3C> vecs)
{
	_vectors = vecs;
}

std::vector<Vector3C>* ExampleAPI::GetVectors()
{
	return &_vectors;
}

MYAPI ExampleAPI * CreateExample()
{
	std::vector<Vector3C> temp;
	Vector3C a(1, 2, 3);
	Vector3C b(4, 5, 6);
	temp.push_back(a);
	temp.push_back(b);
	auto res = new ExampleAPI(temp);
	return res;
}

MYAPI void DeleteExample(ExampleAPI * handler)
{
	handler->~ExampleAPI();
}

MYAPI void PassInVectors(ExampleAPI * handler, Vector3C * vecs, int len)
{
	std::vector<Vector3C> retrieved(vecs, vecs + len);
	//std::vector<Vector3C> vectors;
	//vectors.resize(retrieved.size());
	//std::transform(retrieved.begin(), retrieved.end(), vectors.begin(), [](Vector3 v) {return Vector3C(v);});
	handler->PassInVectors(retrieved);
}

MYAPI  Vector3C * GetVectors(ExampleAPI * handler, int * len)
{
	//std::vector<Vector3> sending;
	//sending.resize(vectors.size());
	//std::transform(vectors.begin(), vectors.end(), sending.begin(), [](Vector3C v) {return v.GetBackingVector();});
	*len = (*handler->GetVectors()).size();
	return (*handler->GetVectors()).data();
}
