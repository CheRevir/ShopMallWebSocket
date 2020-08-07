﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//     Website: http://ITdos.com/Dos/ORM/Index.html
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Dos.ORM;

namespace Model
{
    /// <summary>
    /// 实体类Mall_User。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("Mall_User")]
    [Serializable]
    public partial class Mall_User : Entity
    {
        #region Model
        private int _UID;
        private string _User_ID;
        private string _User_Name;
        private string _Password;
        private string _Phone;
        private string _E_Mail;
        private string _Identity_Number;
        private string _ADDR;
        private string _QQ;
        private string _WX;
        private string _IP;
        private DateTime? _Login_Time;
        private DateTime _Registration_Time;
        private int _Grade_Integral;
        private int? _Available_Integral;
        private string _Integral_Password;
        private string _Verification_Code;
        private string _Head;
        private int _State;
        private int? _Lgoin_State;
        private string _Invitation_Code;
        private int? _Superior;
        private string _Superior_Name;
        private decimal _Balance;
        private int? _Admin;
        private string _Admin_Name;
        private string _Remarks;

        /// <summary>
        /// 
        /// </summary>
        [Field("UID")]
        public int UID
        {
            get { return _UID; }
            set
            {
                this.OnPropertyValueChange("UID");
                this._UID = value;
            }
        }
        /// <summary>
        /// 账户ID
        /// </summary>
        [Field("User_ID")]
        public string User_ID
        {
            get { return _User_ID; }
            set
            {
                this.OnPropertyValueChange("User_ID");
                this._User_ID = value;
            }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        [Field("User_Name")]
        public string User_Name
        {
            get { return _User_Name; }
            set
            {
                this.OnPropertyValueChange("User_Name");
                this._User_Name = value;
            }
        }
        /// <summary>
        /// 密码
        /// </summary>
        [Field("Password")]
        public string Password
        {
            get { return _Password; }
            set
            {
                this.OnPropertyValueChange("Password");
                this._Password = value;
            }
        }
        /// <summary>
        /// 手机
        /// </summary>
        [Field("Phone")]
        public string Phone
        {
            get { return _Phone; }
            set
            {
                this.OnPropertyValueChange("Phone");
                this._Phone = value;
            }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Field("E_Mail")]
        public string E_Mail
        {
            get { return _E_Mail; }
            set
            {
                this.OnPropertyValueChange("E_Mail");
                this._E_Mail = value;
            }
        }
        /// <summary>
        /// 身份证号
        /// </summary>
        [Field("Identity_Number")]
        public string Identity_Number
        {
            get { return _Identity_Number; }
            set
            {
                this.OnPropertyValueChange("Identity_Number");
                this._Identity_Number = value;
            }
        }
        /// <summary>
        /// 地址
        /// </summary>
        [Field("ADDR")]
        public string ADDR
        {
            get { return _ADDR; }
            set
            {
                this.OnPropertyValueChange("ADDR");
                this._ADDR = value;
            }
        }
        /// <summary>
        /// QQ
        /// </summary>
        [Field("QQ")]
        public string QQ
        {
            get { return _QQ; }
            set
            {
                this.OnPropertyValueChange("QQ");
                this._QQ = value;
            }
        }
        /// <summary>
        /// 微信
        /// </summary>
        [Field("WX")]
        public string WX
        {
            get { return _WX; }
            set
            {
                this.OnPropertyValueChange("WX");
                this._WX = value;
            }
        }
        /// <summary>
        /// IP
        /// </summary>
        [Field("IP")]
        public string IP
        {
            get { return _IP; }
            set
            {
                this.OnPropertyValueChange("IP");
                this._IP = value;
            }
        }
        /// <summary>
        /// 登录时间
        /// </summary>
        [Field("Login_Time")]
        public DateTime? Login_Time
        {
            get { return _Login_Time; }
            set
            {
                this.OnPropertyValueChange("Login_Time");
                this._Login_Time = value;
            }
        }
        /// <summary>
        /// 注册时间
        /// </summary>
        [Field("Registration_Time")]
        public DateTime Registration_Time
        {
            get { return _Registration_Time; }
            set
            {
                this.OnPropertyValueChange("Registration_Time");
                this._Registration_Time = value;
            }
        }
        /// <summary>
        /// 等级积分
        /// </summary>
        [Field("Grade_Integral")]
        public int Grade_Integral
        {
            get { return _Grade_Integral; }
            set
            {
                this.OnPropertyValueChange("Grade_Integral");
                this._Grade_Integral = value;
            }
        }
        /// <summary>
        /// 可用积分
        /// </summary>
        [Field("Available_Integral")]
        public int? Available_Integral
        {
            get { return _Available_Integral; }
            set
            {
                this.OnPropertyValueChange("Available_Integral");
                this._Available_Integral = value;
            }
        }
        /// <summary>
        /// 积分密码
        /// </summary>
        [Field("Integral_Password")]
        public string Integral_Password
        {
            get { return _Integral_Password; }
            set
            {
                this.OnPropertyValueChange("Integral_Password");
                this._Integral_Password = value;
            }
        }
        /// <summary>
        /// 验证码
        /// </summary>
        [Field("Verification_Code")]
        public string Verification_Code
        {
            get { return _Verification_Code; }
            set
            {
                this.OnPropertyValueChange("Verification_Code");
                this._Verification_Code = value;
            }
        }
        /// <summary>
        /// 头像
        /// </summary>
        [Field("Head")]
        public string Head
        {
            get { return _Head; }
            set
            {
                this.OnPropertyValueChange("Head");
                this._Head = value;
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        [Field("State")]
        public int State
        {
            get { return _State; }
            set
            {
                this.OnPropertyValueChange("State");
                this._State = value;
            }
        }
        /// <summary>
        /// 登录状态
        /// </summary>
        [Field("Lgoin_State")]
        public int? Lgoin_State
        {
            get { return _Lgoin_State; }
            set
            {
                this.OnPropertyValueChange("Lgoin_State");
                this._Lgoin_State = value;
            }
        }
        /// <summary>
        /// 邀请码
        /// </summary>
        [Field("Invitation_Code")]
        public string Invitation_Code
        {
            get { return _Invitation_Code; }
            set
            {
                this.OnPropertyValueChange("Invitation_Code");
                this._Invitation_Code = value;
            }
        }
        /// <summary>
        /// 上级
        /// </summary>
        [Field("Superior")]
        public int? Superior
        {
            get { return _Superior; }
            set
            {
                this.OnPropertyValueChange("Superior");
                this._Superior = value;
            }
        }
        /// <summary>
        /// 上级账户
        /// </summary>
        [Field("Superior_Name")]
        public string Superior_Name
        {
            get { return _Superior_Name; }
            set
            {
                this.OnPropertyValueChange("Superior_Name");
                this._Superior_Name = value;
            }
        }
        /// <summary>
        /// 余额
        /// </summary>
        [Field("Balance")]
        public decimal Balance
        {
            get { return _Balance; }
            set
            {
                this.OnPropertyValueChange("Balance");
                this._Balance = value;
            }
        }
        /// <summary>
        /// 管理员
        /// </summary>
        [Field("Admin")]
        public int? Admin
        {
            get { return _Admin; }
            set
            {
                this.OnPropertyValueChange("Admin");
                this._Admin = value;
            }
        }
        /// <summary>
        /// 管理员账户
        /// </summary>
        [Field("Admin_Name")]
        public string Admin_Name
        {
            get { return _Admin_Name; }
            set
            {
                this.OnPropertyValueChange("Admin_Name");
                this._Admin_Name = value;
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        [Field("Remarks")]
        public string Remarks
        {
            get { return _Remarks; }
            set
            {
                this.OnPropertyValueChange("Remarks");
                this._Remarks = value;
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// 获取实体中的主键列
        /// </summary>
        public override Field[] GetPrimaryKeyFields()
        {
            return new Field[] {
                _.UID,
            };
        }
        /// <summary>
        /// 获取实体中的标识列
        /// </summary>
        public override Field GetIdentityField()
        {
            return _.UID;
        }
        /// <summary>
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
                _.UID,
                _.User_ID,
                _.User_Name,
                _.Password,
                _.Phone,
                _.E_Mail,
                _.Identity_Number,
                _.ADDR,
                _.QQ,
                _.WX,
                _.IP,
                _.Login_Time,
                _.Registration_Time,
                _.Grade_Integral,
                _.Available_Integral,
                _.Integral_Password,
                _.Verification_Code,
                _.Head,
                _.State,
                _.Lgoin_State,
                _.Invitation_Code,
                _.Superior,
                _.Superior_Name,
                _.Balance,
                _.Admin,
                _.Admin_Name,
                _.Remarks,
            };
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
                this._UID,
                this._User_ID,
                this._User_Name,
                this._Password,
                this._Phone,
                this._E_Mail,
                this._Identity_Number,
                this._ADDR,
                this._QQ,
                this._WX,
                this._IP,
                this._Login_Time,
                this._Registration_Time,
                this._Grade_Integral,
                this._Available_Integral,
                this._Integral_Password,
                this._Verification_Code,
                this._Head,
                this._State,
                this._Lgoin_State,
                this._Invitation_Code,
                this._Superior,
                this._Superior_Name,
                this._Balance,
                this._Admin,
                this._Admin_Name,
                this._Remarks,
            };
        }
        /// <summary>
        /// 是否是v1.10.5.6及以上版本实体。
        /// </summary>
        /// <returns></returns>
        public override bool V1_10_5_6_Plus()
        {
            return true;
        }
        #endregion

        #region _Field
        /// <summary>
        /// 字段信息
        /// </summary>
        public class _
        {
            /// <summary>
            /// * 
            /// </summary>
            public readonly static Field All = new Field("*", "Mall_User");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field UID = new Field("UID", "Mall_User", "");
            /// <summary>
			/// 账户ID
			/// </summary>
			public readonly static Field User_ID = new Field("User_ID", "Mall_User", "账户ID");
            /// <summary>
			/// 姓名
			/// </summary>
			public readonly static Field User_Name = new Field("User_Name", "Mall_User", "姓名");
            /// <summary>
			/// 密码
			/// </summary>
			public readonly static Field Password = new Field("Password", "Mall_User", "密码");
            /// <summary>
			/// 手机
			/// </summary>
			public readonly static Field Phone = new Field("Phone", "Mall_User", "手机");
            /// <summary>
			/// 邮箱
			/// </summary>
			public readonly static Field E_Mail = new Field("E_Mail", "Mall_User", "邮箱");
            /// <summary>
			/// 身份证号
			/// </summary>
			public readonly static Field Identity_Number = new Field("Identity_Number", "Mall_User", "身份证号");
            /// <summary>
			/// 地址
			/// </summary>
			public readonly static Field ADDR = new Field("ADDR", "Mall_User", "地址");
            /// <summary>
			/// QQ
			/// </summary>
			public readonly static Field QQ = new Field("QQ", "Mall_User", "QQ");
            /// <summary>
			/// 微信
			/// </summary>
			public readonly static Field WX = new Field("WX", "Mall_User", "微信");
            /// <summary>
			/// IP
			/// </summary>
			public readonly static Field IP = new Field("IP", "Mall_User", "IP");
            /// <summary>
			/// 登录时间
			/// </summary>
			public readonly static Field Login_Time = new Field("Login_Time", "Mall_User", "登录时间");
            /// <summary>
			/// 注册时间
			/// </summary>
			public readonly static Field Registration_Time = new Field("Registration_Time", "Mall_User", "注册时间");
            /// <summary>
			/// 等级积分
			/// </summary>
			public readonly static Field Grade_Integral = new Field("Grade_Integral", "Mall_User", "等级积分");
            /// <summary>
			/// 可用积分
			/// </summary>
			public readonly static Field Available_Integral = new Field("Available_Integral", "Mall_User", "可用积分");
            /// <summary>
			/// 积分密码
			/// </summary>
			public readonly static Field Integral_Password = new Field("Integral_Password", "Mall_User", "积分密码");
            /// <summary>
			/// 验证码
			/// </summary>
			public readonly static Field Verification_Code = new Field("Verification_Code", "Mall_User", "验证码");
            /// <summary>
			/// 头像
			/// </summary>
			public readonly static Field Head = new Field("Head", "Mall_User", "头像");
            /// <summary>
			/// 状态
			/// </summary>
			public readonly static Field State = new Field("State", "Mall_User", "状态");
            /// <summary>
			/// 登录状态
			/// </summary>
			public readonly static Field Lgoin_State = new Field("Lgoin_State", "Mall_User", "登录状态");
            /// <summary>
			/// 邀请码
			/// </summary>
			public readonly static Field Invitation_Code = new Field("Invitation_Code", "Mall_User", "邀请码");
            /// <summary>
			/// 上级
			/// </summary>
			public readonly static Field Superior = new Field("Superior", "Mall_User", "上级");
            /// <summary>
			/// 上级账户
			/// </summary>
			public readonly static Field Superior_Name = new Field("Superior_Name", "Mall_User", "上级账户");
            /// <summary>
			/// 余额
			/// </summary>
			public readonly static Field Balance = new Field("Balance", "Mall_User", "余额");
            /// <summary>
			/// 管理员
			/// </summary>
			public readonly static Field Admin = new Field("Admin", "Mall_User", "管理员");
            /// <summary>
			/// 管理员账户
			/// </summary>
			public readonly static Field Admin_Name = new Field("Admin_Name", "Mall_User", "管理员账户");
            /// <summary>
			/// 备注
			/// </summary>
			public readonly static Field Remarks = new Field("Remarks", "Mall_User", "备注");
        }
        #endregion
    }
}