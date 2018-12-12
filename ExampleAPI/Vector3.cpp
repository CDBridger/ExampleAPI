#include "Vector3.h"

Vector3C::Vector3C(float x, float y, float z)
{
	_x = x;
	_y = y;
	_z = z;
}

//Vector3C::Vector3C(const Vector3C & vec)
//{
//	_x = vec._x;
//	_y = vec._y;
//	_z = vec._z;
//}

//Vector3C::Vector3C(const Vector3 & vec)
//{
//	_vec = vec;
//}

//Vector3C::~Vector3C()
//{
//}

float Vector3C::GetX()
{
	return _x;
}

void Vector3C::SetX(float val)
{
	_x = val;
}

float Vector3C::GetY()
{
	return _y;
}

void Vector3C::SetY(float val)
{
	_y = val;
}

float Vector3C::GetZ()
{
	return _z;
}

void Vector3C::SetZ(float val)
{
	_z = val;
}

Vector3C Vector3C::GetUnitVector()
{
	float mag = Magnitude();
	return Vector3C(_x / mag, _y / mag, _z / mag);
}

void Vector3C::MakeUnitVector()
{
	float mag = Magnitude();
	_x = _x / mag;
	_y = _y / mag;
	_z = _z / mag;
}

Vector3C Vector3C::Add(Vector3C vec)
{
	return Vector3C(_x + vec._x, _y + vec._y, _z + vec._z);
}

Vector3C Vector3C::Add(float amount)
{
	return Vector3C(_x + amount, _y + amount, _z + amount);
}

float Vector3C::Magnitude()
{
	return sqrtf(_x*_x + _y * _y + _z * _z);
}

//Vector3 Vector3C::GetBackingVector()
//{
//	return _vec;
//}

MYAPI Vector3C * CreateVector3()
{
	return new Vector3C();
}

MYAPI Vector3C * CreateVector3Args(float x, float y, float z)
{
	return new Vector3C(x, y, z);
}

MYAPI void DeleteVector3(Vector3C * handler)
{
	handler->~Vector3C();
}

MYAPI float GetX(Vector3C * handler)
{
	return handler->GetX();
}

MYAPI void SetX(Vector3C * handler, float val)
{
	handler->SetX(val);
}

MYAPI float GetY(Vector3C * handler)
{
	return handler->GetY();
}

MYAPI void SetY(Vector3C * handler, float val)
{
	handler->SetY(val);
}

MYAPI float GetZ(Vector3C * handler)
{
	return handler->GetZ();
}

MYAPI void SetZ(Vector3C * handler, float val)
{
	handler->SetZ(val);
}

MYAPI Vector3C * GetUnitVector(Vector3C * handler)
{
	return new Vector3C(handler->GetUnitVector());
}

MYAPI void MakeUnitVector(Vector3C * handler)
{
	handler->MakeUnitVector();
}

MYAPI Vector3C * Add(Vector3C * handler, Vector3C * vec)
{
	return new Vector3C(handler->Add(*vec));
}

MYAPI Vector3C * AddScalar(Vector3C * handler, float amount)
{
	return new Vector3C(handler->Add(amount));
}

MYAPI float Magnitude(Vector3C * handler)
{
	return handler->Magnitude();
}
