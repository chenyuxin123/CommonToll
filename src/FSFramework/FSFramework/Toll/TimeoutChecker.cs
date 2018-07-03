using System;

namespace FSFramework.Toll
{
    /// <summary>
    /// 业务代码执行超时检查类
    /// </summary>
    public class TimeoutChecker
    {
        long _timeout;                        //超时时间
        Action<Delegate> _proc;               //会超时的代码
        Action<Delegate> _procHandle;         //处理超时
        Action<Delegate> _timeoutHandle;      //超时后处理事件
        System.Threading.ManualResetEvent _event = new System.Threading.ManualResetEvent(false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proc">会超时的代码</param>
        /// <param name="timeoutHandle">超时后处理事件</param>
        public TimeoutChecker(Action<Delegate> proc, Action<Delegate> timeoutHandle)
        {
            _proc = proc;
            _timeoutHandle = timeoutHandle;
            _procHandle = delegate
            {
                //计算代码执行的时间
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                _proc?.Invoke(null);
                sw.Stop();
                //如果执行时间小于超时时间则通知用户线程
                if (sw.ElapsedMilliseconds < _timeout && _event != null)
                {
                    _event.Set();
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public bool Wait(long timeout)
        {
            _timeout = timeout;
            _procHandle.BeginInvoke(null, null, null);//异步执行
            bool flag = _event.WaitOne((int)timeout, false); //如果在规定时间内没等到通知则为 false
            if (!flag)
            {
                //触发超时事件
                _timeoutHandle?.Invoke(null);
            }
            Dispose();

            return flag;
        }

        private void Dispose()
        {
            if (_event != null)
            {
                _event.Close();
            }
            _event = null;
            _proc = null;
            _procHandle = null;
            _timeoutHandle = null;
        }

        //例子
        //public void Test()
        //{
        //    try
        //    {
        //        TimeoutChecker timeout = new TimeoutChecker(
        //            delegate
        //            {
        //                try
        //                {
        //                    Console.WriteLine("执行会超时的代码10秒");
        //                    System.Threading.Thread.Sleep(10000);
        //                }
        //                catch
        //                {

        //                }
        //            },
        //            delegate
        //            {
        //                //执行超时函数
        //                Console.WriteLine("执行超时");
        //            });

        //        if (timeout.Wait(4000))
        //        {
        //            Console.WriteLine("执行成功");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}
    }
}