using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ApiWrapper
{
    public partial class Vector3
    {

        [StructLayout(LayoutKind.Sequential)]
        internal struct BackingVector
        {
            [MarshalAs(UnmanagedType.R4)]
            public float _x;
            [MarshalAs(UnmanagedType.R4)]
            public float _y;
            [MarshalAs(UnmanagedType.R4)]
            public float _z;
        }

        private IntPtr _handler;
        private BackingVector _vector;

        public Vector3()
        {
            _vector = new BackingVector();
            _handler = CreateVector3();
        }

        public Vector3(float x, float y, float z)
        {
            _vector = new BackingVector();
            _handler = CreateVector3Args(x, y, z);
        }

        internal Vector3(BackingVector vec)
        {
            _vector = vec;
            _handler = CreateVector3Args(vec._x, vec._y, vec._z);
        }

        public Vector3(IntPtr other)
        {
            _vector = new BackingVector();
            _handler = other;
        }

        ~Vector3()
        {
            DeleteVector3(_handler);
        }

        internal BackingVector GetBackingVector()
        {
            return _vector;
        }


        public float X
        {
            get
            {
                _vector._x = GetX(_handler);
                return _vector._x;
            }
            set
            {
                SetX(_handler, value);
            }
        }
        public float Y
        {
            get
            {
                _vector._y = GetY(_handler);
                return GetY(_handler);
            }
            set
            {
                SetY(_handler, value);
            }
        }
        public float Z
        {
            get
            {
                _vector._z = GetZ(_handler);
                return GetZ(_handler);
            }
            set
            {
                SetZ(_handler, value);
            }
        }

        public Vector3 GetUnitVector()
        {
            return new Vector3(GetUnitVector(_handler));
        }

        public void MakeUnitVector()
        {
            MakeUnitVector(_handler);
        }

        public Vector3 Add(float amount)
        {
            return new Vector3(AddScalar(_handler, amount));
        }

        public Vector3 Add(Vector3 vec)
        {
            return new Vector3(Add(_handler, vec._handler));
        }

        public override string ToString()
        {
            return $"X : {X}, Y : {Y}, Z : {Z}";
        }
    }
}
