using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Custom
{

    /// <summary>
    /// 你的實作類別
    /// 必須繼承的介面Regulus.Remoting.ICore，Regulus.Remoting.Soul.Native.Server依賴於他
    /// </summary>
    public class Appliction :       
        Regulus.Remoting.ICore
    {
        // 範例物件        
        SampleClass _SampleClass;
        Regulus.Utility.Console.IViewer _View;
        
        public Appliction(Regulus.Utility.Console.IViewer view)
        {
        
            _SampleClass = new SampleClass();
            _View = view;            
        }

        //系統初始化會呼叫此方法
        void Regulus.Framework.IBootable.Launch()
        {            
        }

        //系統關閉時會呼叫此方法
        void Regulus.Framework.IBootable.Shutdown()
        {            
        }

        // 如果有使用者連線進來則會呼叫此方法
        void Regulus.Remoting.ICore.AssignBinder(Regulus.Remoting.ISoulBinder binder)
        {

            // 綁定_SampleClass客戶端將會收到Custom.ISample實體            
            binder.Bind<Custom.ISample>(_SampleClass);

            // 如果客戶端斷線則會發生此事件
            binder.BreakEvent += () => { _View.WriteLine("有一位使用者離開"); };

            _View.WriteLine("有一位使用者加入");
        }

        /*
         * 系統每次刷新皆會呼叫此方法
         * 每秒刷新次數不固定。
         */
        bool Regulus.Utility.IUpdatable.Update()
        {
            return true;
        }

    }
}
