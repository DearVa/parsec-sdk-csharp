using System;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable once CheckNamespace
namespace CppSharp.Runtime
{
	internal static class MarshalUtil
	{
		public static unsafe T[] GetArray<T>(void* array, int size) where T : unmanaged
		{
			if ((IntPtr)array == IntPtr.Zero) throw new NullReferenceException();
			var array1 = new T[size];
			fixed (T* destination = array1)
				Buffer.MemoryCopy(
					array,
					destination,
					sizeof(T) * size,
					sizeof(T) * size);
			return array1;
		}

		private static Encoding ParsecEncoding => Encoding.UTF8;

		public static unsafe string GetString(sbyte* array, int size)
		{
			if ((IntPtr)array == IntPtr.Zero) throw new NullReferenceException();
			if (size == 0)
			{
				while (array[size++] != 0) 
				{ }
				size--;
			}
			if (size == 0) return string.Empty;
			
			var bytes = new byte[size];
			fixed (byte* destination = bytes)
				Buffer.MemoryCopy(
					array,
					destination,
					sizeof(byte) * size,
					sizeof(byte) * size);
			return ParsecEncoding.GetString(bytes);
		}

		public static unsafe void SetString(void* array, int size, string str)
		{
			var bytes = ParsecEncoding.GetBytes(str);
			if (bytes.Length > size) throw new ArgumentOutOfRangeException(nameof(size));
			fixed (byte* source = bytes)
				Buffer.MemoryCopy(
					source,
					array,
					sizeof(byte) * size,
					sizeof(byte) * bytes.Length);
		}

		public static unsafe IntPtr StringToHGlobal(string str)
		{
			var bytes = ParsecEncoding.GetBytes(str);
			var ptr = Marshal.AllocHGlobal(bytes.Length + 1);
			fixed (byte* source = bytes)
				Buffer.MemoryCopy(
					source,
					(byte*)ptr,
					sizeof(byte) * bytes.Length,
					sizeof(byte) * bytes.Length);
			return ptr;
		}
	}
}