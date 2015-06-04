using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Custom
{
    /// <summary>
    /// 展示用遠端物件
    /// 目前版本提供的所有支援類型
    /// </summary>
    public interface ISample
    {


        /// <summary>
        /// 可以公開getter屬性伺服器端每段時間掃描更新。
        /// 完全比較需求效能較高也因此建議少用(未來不排除移除該項功能，或其他實做方法解決)
        /// </summary>
        float ElapsedSecond { get; }
        /// <summary>
        /// 方法展示
        /// 
        /// 1.目前支援的型別為任何Protobuf可續列化的物件。
        /// 2.傳回只支援Regulus.Remoting.Value，做異步資料傳送或接收。
        /// 
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns>        
        /// 將會傳回num1與num2的總和
        /// </returns>
        Regulus.Remoting.Value<int> Add(int num1,int num2);

        /// <summary>
        /// 傳回值可以帶入介面物件，參數目前不支援。
        /// </summary>
        /// <returns>傳回ISubtractor</returns>
        Regulus.Remoting.Value<ISubtractor> GetSubtractor();
    }
}
