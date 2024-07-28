namespace Ignite {
    public static class Binary {
        public static byte[] Bytes (this byte value)
            => [value];
        public static byte[] Bytes (this ushort value)
            => System.BitConverter.GetBytes (value);
        public static byte[] Bytes (this uint value)
            => System.BitConverter.GetBytes (value);
        public static byte[] Bytes (this ulong value)
            => System.BitConverter.GetBytes (value);

        public static byte[] Bytes (this sbyte value)
            => [unchecked((byte)value)];
        public static byte[] Bytes (this short value)
            => System.BitConverter.GetBytes (value);
        public static byte[] Bytes (this int value)
            => System.BitConverter.GetBytes (value);
        public static byte[] Bytes (this long value)
            => System.BitConverter.GetBytes (value);

        public static byte[] Bytes (this nint value)
            => System.BitConverter.GetBytes (value);
        public static byte[] Bytes (this nuint value)
            => System.BitConverter.GetBytes (value);

        public static byte[] Bytes (this float value)
            => System.BitConverter.GetBytes (value);
        public static byte[] Bytes (this double value)
            => System.BitConverter.GetBytes (value);
        public static byte[] Bytes (this decimal value) {
            var bits = decimal.GetBits (value);
            var bytes = new byte[bits.Length << 2];
            for (var i = 0; i < bits.Length; i++)
                System.Buffer.BlockCopy (System.BitConverter.GetBytes (bits[i]), 0, bytes, i << 2, 4);
            return bytes;
        }

        public static byte[] Bytes (this bool value)
            => System.BitConverter.GetBytes (value);
        public static byte[] Bytes (this char value)
            => System.BitConverter.GetBytes (value);
        public static byte[] Bytes (this string value)
            => System.Text.Encoding.UTF8.GetBytes (value);

        public static T Convert<T> (this byte[] data) {
            var type = System.Nullable.GetUnderlyingType (typeof (T)) ?? typeof (T);

            switch (type.Name) {
                case "IntPtr" when nint.Size == 4:
                    return (T)(object)new nint (System.BitConverter.ToInt32 (data, 0));
                case "IntPtr" when nint.Size == 8:
                    return (T)(object)new nint (System.BitConverter.ToInt64 (data, 0));
                case "UIntPtr" when nuint.Size == 4:
                    return (T)(object)new nuint (System.BitConverter.ToUInt32 (data, 0));
                case "UIntPtr" when nuint.Size == 8:
                    return (T)(object)new nuint (System.BitConverter.ToUInt64 (data, 0));
            }

            switch (System.Type.GetTypeCode (type)) {
                case System.TypeCode.Byte when data.Length == 1:
                    return (T)(object)data[0];
                case System.TypeCode.UInt16 when data.Length == 2:
                    return (T)(object)System.BitConverter.ToUInt16 (data, 0);
                case System.TypeCode.UInt32 when data.Length == 4:
                    return (T)(object)System.BitConverter.ToUInt32 (data, 0);
                case System.TypeCode.UInt64 when data.Length == 8:
                    return (T)(object)System.BitConverter.ToUInt64 (data, 0);

                case System.TypeCode.SByte when data.Length == 1:
                    return (T)(object)(sbyte)data[0];
                case System.TypeCode.Int16 when data.Length == 2:
                    return (T)(object)System.BitConverter.ToInt16 (data, 0);
                case System.TypeCode.Int32 when data.Length == 4:
                    return (T)(object)System.BitConverter.ToInt32 (data, 0);
                case System.TypeCode.Int64 when data.Length == 8:
                    return (T)(object)System.BitConverter.ToInt64 (data, 0);

                case System.TypeCode.Single when data.Length == 4:
                    return (T)(object)System.BitConverter.ToSingle (data, 0);
                case System.TypeCode.Double when data.Length == 8:
                    return (T)(object)System.BitConverter.ToDouble (data, 0);
                case System.TypeCode.Decimal when data.Length == 16:
                    var bits = new int[4];

                    for (var i = 0; i < 4; i++)
                        bits[i] = System.BitConverter.ToInt32 (data, i << 2);

                    return (T)(object)new decimal (bits);
                case System.TypeCode.Boolean when data.Length == 1:
                    return (T)(object)System.BitConverter.ToBoolean (data, 0);
                case System.TypeCode.Char when data.Length == 2:
                    return (T)(object)System.BitConverter.ToChar (data, 0);
                case System.TypeCode.String:
                    return (T)(object)System.Text.Encoding.UTF8.GetString (data);
            }

            throw new System.InvalidOperationException ($"The type '{typeof (T).Name}' is not supported or the length of the byte array does not match the size of the type.");
        }
    }
}