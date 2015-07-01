using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Custom
{

    /*
     * 自定義要給客戶端使用的實體
     * Custom entities to be used by the client
     */
    class SampleClass : Custom.ISample
    {
        Regulus.Utility.TimeCounter _TimeCounter;
        Subtractor _Subtractor;
        public SampleClass()
        {
            _TimeCounter = new Regulus.Utility.TimeCounter();
            _Subtractor = new Subtractor();
        }
        
        float ISample.ElapsedSecond
        {
            get { return _TimeCounter.Second; }
        }


        Regulus.Remoting.Value<int> ISample.Add(int num1, int num2)
        {
            return num1 + num2;
        }

        Regulus.Remoting.Value<ISubtractor> ISample.GetSubtractor()
        {
            return _Subtractor;
        }
    }
}
