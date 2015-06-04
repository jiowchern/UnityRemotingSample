using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Custom
{

    /*
     * 自定義要給客戶端使用的實體
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

        // 傳送經過秒數給客戶端
        float ISample.ElapsedSecond
        {
            get { return _TimeCounter.Second; }
        }


        // 計算num1跟num2的和
        Regulus.Remoting.Value<int> ISample.Add(int num1, int num2)
        {
            return num1 + num2;
        }

        //傳回物件
        Regulus.Remoting.Value<ISubtractor> ISample.GetSubtractor()
        {
            return _Subtractor;
        }
    }
}
