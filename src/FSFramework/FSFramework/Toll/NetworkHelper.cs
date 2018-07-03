using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace FSFramework.Toll
{
    /// <summary>
    /// 网络帮助类
    /// </summary>
    public static class NetworkHelper
    {
        /// <summary>
        /// 获取本机IPv4地址
        /// </summary>
        /// <returns></returns>
        public static string[] GetLocalIPv4()
        {
            IPHostEntry hostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] address = hostInfo.AddressList;
            return address.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(x => x.ToString()).ToArray();
        }

        /// <summary>
        /// ping IP地址 timeout 局域网用200,广域网用2000
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="timeout">超时 毫秒</param>
        /// <returns></returns>
        public static bool Ping(string ip, int timeout)
        {
            IPAddress ipadd;
            if (!IPAddress.TryParse(ip, out ipadd))
            {
                return false;
            }
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send(ip, timeout, new Byte[] { Convert.ToByte(1) });
            if (reply.Status == IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}