
using System;


namespace CodeJam.Collections
{
    using System.Diagnostics.Contracts;

    public static class ArrayExtensions
	{
		#region byte
		/// <summary>
		/// Returns true, if length and content of <paramref name="a"/> equals <paramref name="b"/>.
		/// </summary>
		/// <param name="a">The first array to compare.</param>
		/// <param name="b">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="a"/> equals <paramref name="b"/>.</returns>
		[Pure]
		public static unsafe bool EqualsTo(this byte[] a, byte[] b)
		{
			if (a == b)
				return true;

			if (a == null || b == null)
				return false;

			if (a.Length != b.Length)
				return false;

			if (a.Length < 5)
			{
				for (var i = 0; i < a.Length; i++)
					if (a[i] != b[i])
						return false;

				return true;
			}

			fixed (void* pa = a, pb = b)
				return Memory.Compare((byte*)pa, (byte*)pb, a.Length * sizeof(byte));
		}

		/// <summary>
		/// Compares length and content of <paramref name="a"/> and <paramref name="b"/>.
		/// </summary>
		/// <param name="a">The first array to compare.</param>
		/// <param name="b">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="a"/> equals <paramref name="b"/>.</returns>
		[Pure]
		public static bool EqualsTo(this byte?[] a, byte?[] b)
		{
			if (a == b)
				return true;

			if (a == null || b == null)
				return false;

			if (a.Length != b.Length)
				return false;

			for (var i = 0; i < a.Length; i++)
			{
				var lhs = a[i];
				var rhs = b[i];
				if (lhs.GetValueOrDefault() != rhs.GetValueOrDefault() || lhs.HasValue != rhs.HasValue)
					return false;
			}

			return true;
		}
		#endregion


		#region decimal
		/// <summary>
		/// Compares length and content of <paramref name="a"/> and <paramref name="b"/>.
		/// </summary>
		/// <param name="a">The first array to compare.</param>
		/// <param name="b">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="a"/> equals <paramref name="b"/>.</returns>
		[Pure]
		public static bool EqualsTo(this decimal[] a, decimal[] b)
		{
			if (a == b)
				return true;

			if (a == null || b == null)
				return false;

			if (a.Length != b.Length)
				return false;

			for (var i = 0; i < a.Length; i++)
				if (a[i] != b[i])
					return false;

			return true;
		}

		/// <summary>
		/// Compares length and content of <paramref name="a"/> and <paramref name="b"/>.
		/// </summary>
		/// <param name="a">The first array to compare.</param>
		/// <param name="b">The second array to compare.</param>
		/// <returns>True, if length and content of <paramref name="a"/> equals <paramref name="b"/>.</returns>
		[Pure]
		public static bool EqualsTo(this decimal?[] a, decimal?[] b)
		{
			if (a == b)
				return true;

			if (a == null || b == null)
				return false;

			if (a.Length != b.Length)
				return false;

			for (var i = 0; i < a.Length; i++)
			{
				var lhs = a[i];
				var rhs = b[i];
				if (lhs.GetValueOrDefault() != rhs.GetValueOrDefault() || lhs.HasValue != rhs.HasValue)
					return false;
			}

			return true;
		}
		#endregion

	}
}
