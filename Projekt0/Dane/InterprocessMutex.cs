using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Dane
{
    internal class InterprocessMutex
    {
        private Mutex _mutex;
        private bool _locked;

        public InterprocessMutex()
        {
            _mutex = new Mutex();
            _locked = false;
        }

        public bool IsLocked()
        {
            return _locked;
        }

        public void WaitOne()
        {
            _mutex.WaitOne();
            _locked = true;
        }

        public void Close()
        {
            _mutex.Close();
            _locked = false;
        }
    }
}
