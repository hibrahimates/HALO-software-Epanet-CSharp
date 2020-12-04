// 
// Epanet -- Epanet2 Toolkit hydraulics library C# Interface
//                                                                    
// EpanetException.cs -- Epanet 2 Toolkit error code to C# exception mapping.
// 
// CREATED:    02/13/2014                                                                    
// VERSION:    2.00                                               
// DATE:         02/13/2014
//             
// AUTHOR:     slava           
// 
// This is free and unencumbered software released into the public domain.
// 
// Anyone is free to copy, modify, publish, use, compile, sell, or
// distribute this software, either in source code form or as a compiled
// binary, for any purpose, commercial or non-commercial, and by any
// means.
// 
// In jurisdictions that recognize copyright laws, the author or authors
// of this software dedicate any and all copyright interest in the
// software to the public domain. We make this dedication for the benefit
// of the public at large and to the detriment of our heirs and
// successors. We intend this dedication to be an overt act of
// relinquishment in perpetuity of all present and future rights to this
// software under copyright law.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org/>

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Epanet
{
    /// <summary>Epanet warning exception</summary>
    public class Warning : EpanetException
    {
        public Warning(ErrorCode error) : base(error) { }
    }

    /// <summary>Epanet system error exception</summary>
    public class SystemError : EpanetException
    {
        public SystemError(ErrorCode error) : base(error) { }
    }

    /// <summary>Epanet user input exception</summary>
    public class InputError : EpanetException
    {
        public InputError(ErrorCode error) : base(error) { }
    }

    /// <summary>Epanet I/O exception</summary>
    public class FileError : EpanetException
    {
        public FileError(ErrorCode error) : base(error) { }
    }

    /// <summary>Epanet library error code to <see cref="System.Exception" /> wrapper.</summary>
    [Serializable]
    public class EpanetException : Exception
    {
        private static List<ErrorCode> _warnings;
        private readonly ErrorCode _errorCode;

        /// <summary>Constructor.</summary>
        /// <param name="error">Epanet dll error code.</param>
        public EpanetException(ErrorCode error)
        {
            this._errorCode = error;
        }

        #region ISerializable implementation

        protected EpanetException(SerializationInfo info, StreamingContext context)
        {
            this._errorCode = (ErrorCode)info.GetValue("_errorCode", typeof(ErrorCode));
        }

        // protected EpanetException() { _errorCode = (ErrorCode)-1; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("_errorCode", this._errorCode);
        }

        #endregion

        public ErrorCode ErrorCode { get { return this._errorCode; } }

        /// <summary>Check, whether this Exception is epanet error.</summary>
        public bool IsError
        {
            get { return this._errorCode >= ErrorCode.Err101 && this._errorCode <= ErrorCode.Err309; }
        }

        /// <summary>Check, whether this Exception is epanet warning.</summary>
        public bool IsWarning
        {
            get { return this._errorCode >= ErrorCode.Warn1 && this._errorCode <= ErrorCode.Warn6; }
        }

        /// <summary>
        ///     Check, whether this Exception is epanet system error.
        ///     (Error codes from <see cref="Epanet.ErrorCode.Err101" />
        ///     to <see cref="Epanet.ErrorCode.Err120" />
        /// </summary>
        public bool IsSystemError
        {
            get { return this._errorCode >= ErrorCode.Err101 && this._errorCode <= ErrorCode.Err120; }
        }

        /// <summary>
        ///     Check, whether this Exception is epanet invalid input error.
        ///     (Error codes from <see cref="Epanet.ErrorCode.Err200" /> to <see cref="Epanet.ErrorCode.Err251" />
        /// </summary>
        public bool IsInputError
        {
            get { return this._errorCode >= ErrorCode.Err200 && this._errorCode <= ErrorCode.Err251; }
        }

        /// <summary>
        ///     Check, whether this Exception is epanet filesystem error.
        ///     (Error codes from <see cref="Epanet.ErrorCode.Err301" /> to <see cref="Epanet.ErrorCode.Err309" />
        /// </summary>
        public bool IsIOError
        {
            get { return this._errorCode >= ErrorCode.Err301 && this._errorCode <= ErrorCode.Err309; }
        }

        /// <summary>
        ///     Retrieves error message string from epanet dll,
        ///     associated with epanet error code this exception was created.
        /// </summary>
        public override string Message
        {
            get
            {
                StringBuilder errmsg = new StringBuilder(UnsafeNativeMethods.MAXMSG);
                ErrorCode err = UnsafeNativeMethods.ENgeterror(this._errorCode, errmsg, errmsg.Capacity + 1);
                return err == 0 ? errmsg.ToString() : string.Format("Unknown EPANET error #{0}", (int)this._errorCode);
            }
        }

        /// <summary>Keeps list of warning error codes, thrown by epanet dll.</summary>
        public static List<ErrorCode> Warnings { get { return _warnings ?? (_warnings = new List<ErrorCode>()); } }

        /// <summary>
        ///     Check error codes returned by epanet methods for errors/warnings.
        ///     <para>If error code is error, throws corresponding <see cref="EpanetException" /></para>
        ///     <para>If error code is warning, adds error code to list of warnings <see cref="Warnings" />.</para>
        ///     <para>If error code == 0 does nothing.</para>
        ///     <para>Use tis method to check errors of epanet unmanaged methods:</para>
        ///     <code>
        /// EpanetException.Check(ENUnmanagedMethod(param1, param2, param3)); 
        /// </code>
        /// </summary>
        /// <param name="err">
        ///     <see cref="Epanet.ErrorCode" />
        /// </param>
        public static void Check(ErrorCode err)
        {
            switch (err)
            {
                case ErrorCode.Ok:
                    return;

                case ErrorCode.Warn1:
                case ErrorCode.Warn2:
                case ErrorCode.Warn3:
                case ErrorCode.Warn4:
                case ErrorCode.Warn5:
                case ErrorCode.Warn6:
                    Warnings.Add(err);
                    return;

                case ErrorCode.Err101:
                case ErrorCode.Err102:
                case ErrorCode.Err103:
                case ErrorCode.Err104:
                case ErrorCode.Err105:
                case ErrorCode.Err106:
                case ErrorCode.Err107:
                case ErrorCode.Err108:
                case ErrorCode.Err109:
                case ErrorCode.Err110:
                case ErrorCode.Err120:
                    throw new SystemError(err);

                case ErrorCode.Err200:
                case ErrorCode.Err201:
                case ErrorCode.Err202:
                case ErrorCode.Err203:
                case ErrorCode.Err204:
                case ErrorCode.Err205:
                case ErrorCode.Err206:
                case ErrorCode.Err207:
                case ErrorCode.Err208:
                case ErrorCode.Err209:
                case ErrorCode.Err210:
                case ErrorCode.Err211:
                case ErrorCode.Err212:
                case ErrorCode.Err213:
                case ErrorCode.Err214:
                case ErrorCode.Err215:
                case ErrorCode.Err216:
                case ErrorCode.Err217:
                case ErrorCode.Err219:
                case ErrorCode.Err220:
                case ErrorCode.Err222:
                case ErrorCode.Err223:
                case ErrorCode.Err224:
                case ErrorCode.Err225:
                case ErrorCode.Err226:
                case ErrorCode.Err227:
                case ErrorCode.Err230:
                case ErrorCode.Err233:
                case ErrorCode.Err240:
                case ErrorCode.Err241:
                case ErrorCode.Err250:
                case ErrorCode.Err251:
                    throw new InputError(err);

                case ErrorCode.Err301:
                case ErrorCode.Err302:
                case ErrorCode.Err303:
                case ErrorCode.Err304:
                case ErrorCode.Err305:
                case ErrorCode.Err306:
                case ErrorCode.Err307:
                case ErrorCode.Err308:
                case ErrorCode.Err309:
                    throw new FileError(err);

                default:
                    throw new EpanetException(err);
            }
        }

        /// <summary>Throws exception, if error code != 0. <seealso cref="Check" /></summary>
        /// <param name="err">
        ///     <see cref="ErrorCode" />
        /// </param>
        public static void CheckWarning(ErrorCode err)
        {
            if (err != 0) throw new EpanetException(err);
        }
    }

}
