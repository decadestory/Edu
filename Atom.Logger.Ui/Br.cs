using System;
using System.Collections.Generic;
using System.Text;

namespace Atom.Logger.Ui
{
    public class Br<T>
    {
        public Br(T data = default(T), int code = 1, string msg = "", object extData = null, string stackTrance = "")
        {
            Code = code;
            Data = data;
            ExtData = extData;
            Msg = msg;
            Message = msg;
            Success = code;
            Status = code;
            StackTrance = stackTrance;
        }

        /// <summary>
        /// 响应码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public int Success { get; set; }
        /// <summary>
        /// http状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 错误堆栈
        /// </summary>
        public string StackTrance { get; set; }
        /// <summary>
        /// 扩展字段
        /// </summary>
        public object ExtData { get; set; }

    }

}
