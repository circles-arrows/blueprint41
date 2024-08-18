using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blueprint41.Sync
{
    public static class Hashing
    {
        #region Identifier

        public static string EncodeIdentifier(long value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("The identifier is invalid");

            // scramble ID space
            uint output = 0;
            uint input = (uint)(value & 0xFFFFFFFF);
            for (int index = 7; index >= 0; index--)
            {
                int pos = index * 4;
                output = output << 4;
                uint mapindex = (input >> pos) & 0xf;
                output |= map[mapindex];
            }
            output ^= 0x3364abf7;
            for (int index = 6; index > 1; index--)
            {
                int pos = index * 4;
                output ^= (output & 0x7f) << pos;
            }

            ulong output2 = output | ((ulong)value & 0xFFFFFFFF00000000);

            // convert integer to base 36 string
            char[] returnValue = new char[] { '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' };
            for (int index = 0; index < 13; index++)
            {
                returnValue[12 - index] = base36Chars[(int)(output2 % 36)];
                output2 = output2 / (ulong)36;
            }

            int skip = 0;
            while (returnValue[skip] == '0' && skip < 7)
            {
                skip++;
            }

            return new string(returnValue, skip, 13 - skip);
        }
        public static long DecodeIdentifier(string value, bool throws = true)
        {
            if (value is null)
            {
                if (throws)
                    throw new ArgumentNullException("The identifier is invalid");
                else
                    return -1;
            }

            if (value.Length < 6)
            {
                if (throws)
                    throw new ArgumentNullException("The identifier is invalid");
                else
                    return -1;
            }


            if (value.Length > 13)
            {
                if (throws)
                    throw new ArgumentNullException("The identifier is invalid");
                else
                    return -1;
            }

            // convert base 36 string to integer
            ulong input2 = 0;
            string upper = value.ToUpper();
            for (int index = 0; index < value.Length; index++)
            {
                int valueindex = base36Chars.IndexOf(upper[index]);
                if (valueindex == -1)
                {
                    if (throws)
                        throw new ArgumentNullException("The identifier is invalid");
                    else
                        return -1;
                }
                
                int power = value.Length - index - 1;
                double base36PowerOf = Math.Pow(36, value.Length - index - 1);
                input2 += (ulong)(valueindex * base36PowerOf);
                if (input2 < base36PowerOf)
                {
                    if (throws)
                        throw new ArgumentNullException("The identifier is invalid");
                    else
                        return -1;
                }
            }

            uint input = (uint)(input2 & 0xFFFFFFFF);
            // unscramble ID space
            uint output = 0;
            for (int index = 6; index > 1; index--)
            {
                int pos = index * 4;
                input ^= (input & 0x7f) << pos;
            }
            input ^= 0x3364abf7;
            for (int index = 7; index >= 0; index--)
            {
                int pos = index * 4;
                output = output << 4;
                uint mapindex = (input >> pos) & 0xf;
                output |= rmap[mapindex];
            }

            return (long)(output | ((ulong)input2 & 0xFFFFFFFF00000000));
        }

        private static readonly uint[] map = new uint[] { 4, 2, 3, 5, 7, 1, 0, 6, 15, 8, 13, 11, 14, 9, 12, 10 };
        private static readonly uint[] rmap = new uint[] { 6, 5, 1, 2, 0, 3, 7, 4, 9, 13, 15, 11, 14, 10, 12, 8 };
        private static readonly string base36Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        #endregion
    }
}
