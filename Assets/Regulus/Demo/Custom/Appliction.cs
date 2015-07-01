using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Custom
{

    /// <summary>
    /// Your implementation classes
    /// </summary>
    public class Appliction :       
        Regulus.Remoting.ICore
    {            
        SampleClass _SampleClass;
        Regulus.Utility.Console.IViewer _View;
        
        public Appliction(Regulus.Utility.Console.IViewer view)
        {
        
            _SampleClass = new SampleClass();
            _View = view;            
        }

        //系統初始化會呼叫此方法
        //System initialization call this method
        void Regulus.Framework.IBootable.Launch()
        {            
        }

        //系統關閉時會呼叫此方法
        //Call this method when the system is off
        void Regulus.Framework.IBootable.Shutdown()
        {            
        }

        // 如果有使用者連線進來則會呼叫此方法
        // Call this method if there is a user connection.
        void Regulus.Remoting.ICore.AssignBinder(Regulus.Remoting.ISoulBinder binder)
        {

            // 綁定_SampleClass客戶端將會收到Custom.ISample實體        
            // Binding _SampleClass client will receive Custom.ISample entities
            binder.Bind<Custom.ISample>(_SampleClass);

            // 如果客戶端斷線則會發生此事件
            // This event occurs if the client is disconnected
            binder.BreakEvent += () => { _View.WriteLine("There is a user to leave"); };

            _View.WriteLine("There is a user to join.");
        }

        /*
         * 系統每次刷新皆會呼叫此方法
         * This method is called by the system at each time
         * 每秒刷新次數不固定。
         * Number of calls per second is not fixed.
         */
        bool Regulus.Utility.IUpdatable.Update()
        {
            return true;
        }

    }
}
