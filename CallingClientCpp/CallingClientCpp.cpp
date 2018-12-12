#include "pch.h"
#include <iostream>
#include "ExampleAPI.h"

void GetVecs(ExampleAPI * api)
{
	int meh = 0;
	int* size = &meh;
	Vector3C * vecsPtr = GetVectors(api, size);
	std::vector<Vector3C> vecs(vecsPtr, vecsPtr + *size);
	std::cout << "Element 0, X : " << GetX(&vecs[0]) << std::endl;
	std::cout << "Element 0, Y : " << GetY(&vecs[0]) << std::endl;
	std::cout << "Element 0, Z : " << GetZ(&vecs[0]) << std::endl;
}

void SetVecs(ExampleAPI * api)
{
	std::vector<Vector3C> settingVecs;
	Vector3C a = *CreateVector3Args(6, 6, 6);
	Vector3C b = *CreateVector3Args(7, 7, 7);
	Vector3C c = *CreateVector3Args(8, 8, 8);
	settingVecs.push_back(a);
	settingVecs.push_back(b);
	settingVecs.push_back(b);
	PassInVectors(api, settingVecs.data(), settingVecs.size());
}

int main()
{
	auto api = CreateExample();
	GetVecs(api);
	SetVecs(api);
	GetVecs(api);
	DeleteExample(api);
}
