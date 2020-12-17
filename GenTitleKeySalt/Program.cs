using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GenTitleKeySalt
{
	class Program
	{
		private static readonly byte[] SecretBytes = { 0xFD, 0x04, 0x01, 0x05, 0x06, 0x0B, 0x11, 0x1C, 0x2D, 0x49 };

		static void Main(string[] args)
		{
			string titleId = args[0];
			byte[] titleIdBytes = HexStringToBytes(titleId);

			Console.WriteLine(ToHexString(new MD5CryptoServiceProvider().ComputeHash(SecretBytes.Concat(LStrip(titleIdBytes)).ToArray())));
		}

		[Pure]
		private static T[] LStrip<T>(IReadOnlyList<T> arr, T value = default)
		{
			int index = 0;
			for (int i = 0; i < arr.Count; i++)
			{
				if (Equals(arr[i], value))
					index = i + 1;
				else
					break;
			}
			T[] ret = new T[arr.Count - index];
			for (int i = 0; i < ret.Length; i++)
				ret[i] = arr[index + i];
			return ret;
		}

		[Pure]
		private static byte[] HexStringToBytes(string hexString)
		{
			byte[] ret = new byte[hexString.Length / 2];
			for (int i = 0; i < hexString.Length / 2; i++)
				ret[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
			return ret;
		}

		[Pure]
		private static string ToHexString(byte[] bytes)
		{
			StringBuilder hex = new StringBuilder(bytes.Length * 2);
			foreach (byte b in bytes)
				hex.AppendFormat("{0:x2}", b);
			return hex.ToString();
		}
	}
}
