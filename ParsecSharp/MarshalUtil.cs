// ReSharper disable once CheckNamespace
namespace CppSharp.Runtime;

internal static class MarshalUtil
{
    public static unsafe T[] GetArray<T>(void* array, int size) where T : unmanaged
    {
        if ((IntPtr)array == IntPtr.Zero) throw new NullReferenceException();
        var array1 = new T[size];
        fixed (T* destination = array1)
            Buffer.MemoryCopy(
                array, destination, sizeof (T) * size, sizeof (T) * size);
        return array1;
    }
}