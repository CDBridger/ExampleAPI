#pragma once
#include <math.h>

//extern "C" struct Vector3
//{
//public:
//	float x;
//	float y;
//	float z;
//	Vector3() : x{ 0.0 }, y{ 0.0 }, z{ 0.0 } {};
//};

extern "C" class Vector3C
{ 
private:
	float _x;
	float _y;
	float _z;
public:
	Vector3C() : _x{0.0}, _y{ 0.0 }, _z{ 0.0 } {};
	Vector3C(float x, float y, float z);
	//Vector3C(const Vector3C &vec);
	////Vector3C(const Vector3 &vec);
	//~Vector3C();
	float GetX();
	void SetX(float val);
	float GetY();
	void SetY(float val);
	float GetZ();
	void SetZ(float val);
	Vector3C GetUnitVector();
	void MakeUnitVector();
	Vector3C Add(Vector3C vec);
	Vector3C Add(float amount);
	float Magnitude();
	//Vector3 GetBackingVector();
};

extern "C" {
	MYAPI Vector3C * CreateVector3();
	MYAPI Vector3C * CreateVector3Args(float x, float y, float z);
	MYAPI void DeleteVector3(Vector3C * handler);
	MYAPI float GetX(Vector3C * handler);
	MYAPI void SetX(Vector3C * handler, float val);
	MYAPI float GetY(Vector3C * handler);
	MYAPI void SetY(Vector3C * handler, float val);
	MYAPI float GetZ(Vector3C * handler);
	MYAPI void SetZ(Vector3C * handler, float val);
	MYAPI Vector3C * GetUnitVector(Vector3C * handler);
	MYAPI void MakeUnitVector(Vector3C * handler);
	MYAPI Vector3C * Add(Vector3C * handler, Vector3C * vec);
	MYAPI Vector3C * AddScalar(Vector3C * handler, float amount);
	MYAPI float Magnitude(Vector3C * handler);
}