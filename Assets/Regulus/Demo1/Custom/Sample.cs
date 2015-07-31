using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Custom
{
    /// <summary>
    /// 自定義實體
    /// Custom entity
    /// 目前版本提供的所有支援類型
    /// The current version provides all the support types
    /// </summary>
    public interface ISample
    {


        /// <summary>
        /// 可以公開getter屬性伺服器端每段時間掃描更新。
        /// You can open the getter property to the server side for each scan for updates.        
        /// </summary>
        float ElapsedSecond { get; }
        /// <summary>
        /// 方法展示
        /// Methods
        /// 
        /// 1.參數可帶入任何Protobuf支援的類型
        ///   The parameters can be brought to support any type of Protobuf
        /// 2.傳回只支援Regulus.Remoting.Value，做異步資料傳送。
        ///   Returns the only support Regulus.Remoting.Value, do asynchronous data transfer.
        /// 
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns>        
        /// 將會傳回num1與num2的總和
        /// Return the sum of num1 and num2
        /// </returns>
        Regulus.Remoting.Value<int> Add(int num1,int num2);

        /// <summary>
        /// 可以傳回介面實體。
        /// You can return the interface entity.
        /// </summary>
        /// <returns>Return ISubtractor</returns>
        Regulus.Remoting.Value<ISubtractor> GetSubtractor();
    }
}
