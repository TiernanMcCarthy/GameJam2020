
using UnityEngine;

[System.Serializable]
/// Vector3 is not usually serialisable, this allows for easy transmission
public struct SerialVector3 
{

    public float x { get; }

    public float y { get; }

    public float z { get; }


    public SerialVector3(float nx,float ny,float nz)
    {
        x = nx;
        y = ny;
        z = nz;
    }

    public static implicit operator Vector3(SerialVector3 NValue)
    {
        return new Vector3(NValue.x, NValue.y, NValue.z);
    }

    public static implicit operator SerialVector3(Vector3 NValue)
    {
        return new SerialVector3(NValue.x, NValue.y, NValue.z);
    }


    public static Vector3  operator *(SerialVector3 f, Vector3 v)
    {
        return new Vector3(f.x * v.x, f.y * v.y, f.z * v.z);
    }
    public static Vector3 operator +(SerialVector3 f, Vector3 v)
    {
        return new Vector3(f.x + v.x, f.y + v.y, f.z + v.z);
    }
    public static Vector3 operator -(SerialVector3 f, Vector3 v)
    {
        return new Vector3(f.x - v.x, f.y - v.y, f.z - v.z);
    }
    public static Vector3 operator /(SerialVector3 f, Vector3 v)
    {
        return new Vector3(f.x / v.x, f.y / v.y, f.z / v.z);
    }
    public static bool operator ==(SerialVector3 f, SerialVector3 v)
    {
        
        if(f.x==v.x && f.y==v.y && f.z==v.z)
        {
            return true;
        }
        return false;
    }
    public static bool operator !=(SerialVector3 f, SerialVector3 v)
    {

        if (f.x != v.x && f.y != v.y && f.z != v.z)
        {
            return true;
        }
        return false;
    }
    public override bool Equals(System.Object obj)
    {
        if (obj == null || !(obj is SerialVector3))
            return false;
        else
            return x == ((SerialVector3)obj).x;
    }

    public override int GetHashCode()
    {
        return (int)x;
    }


}
