namespace Leayal.Closers.CMF
{
    internal static class CmfHelper
    {
        internal static void Decode(ref byte[] data)
        {
            int tmp;
            unsafe
            {
                fixed (byte* src = data)
                {
                    for (int i = 0; i < data.Length / 4; i++)
                    {
                        tmp = i * 4;
                        int num = 0;

                        switch (i % 3)
                        {
                            case 0:
                                {
                                    num = ((0 | src[tmp] << 24 | src[tmp + 1] << 8 | src[tmp + 2] << 16 | src[tmp + 3]) ^ CmfFormat.EntryKey1);
                                    break;
                                }
                            case 1:
                                {
                                    num = ((0 | src[tmp] << 24 | src[tmp + 1] << 8 | src[tmp + 2] << 16 | src[tmp + 3]) ^ CmfFormat.EntryKey2);
                                    break;
                                }
                            case 2:
                                {
                                    num = ((0 | src[tmp] << 24 | src[tmp + 1] << 8 | src[tmp + 2] << 16 | src[tmp + 3]) ^ CmfFormat.EntryKey3);
                                    break;
                                }
                        }
                        byte* byteasd = (byte*)&num;
                        src[tmp] = byteasd[0];
                        src[tmp + 1] = byteasd[1];
                        src[tmp + 2] = byteasd[2];
                        src[tmp + 3] = byteasd[3];
                    }
                }
            }
        }

        internal static int Decode(int data, int key)
        {
            int result = 0;
            unsafe
            {
                byte* pi = (byte*)&data;
                result = (0 | pi[0] << 24 | pi[1] << 8 | pi[2] << 16 | pi[3]) ^ key;
            }
            return result;
        }

        internal static int Decode(uint data, int key)
        {
            int result = 0;
            unsafe
            {
                byte* pi = (byte*)&data;
                result = (0 | pi[0] << 24 | pi[1] << 8 | pi[2] << 16 | pi[3]) ^ key;
            }
            return result;
        }
    }
}
