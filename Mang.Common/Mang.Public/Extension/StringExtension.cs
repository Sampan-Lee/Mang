using System;
using System.Runtime.InteropServices;
using Mang.Public.Util;

namespace Mang.Public.Extension
{
    public static class StringExtension
    {
        private static bool _windows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static string ReplacePath(this string path)
        {
            if (string.IsNullOrEmpty(path))
                return "";
            if (_windows)
                return path.Replace("/", "\\");
            return path.Replace("\\", "/");
        }

        #region string

        /// <summary>
        /// 首字母大写/适用于前端小驼峰方式传参和C#大驼峰属性名对比
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToInitialUpper(this string str)
        {
            return str?.Substring(0, 1).ToUpper() + str?.Substring(1);
        }

        #endregion

        /// <summary>
        /// 汉子转拼音 首字母全称
        /// </summary>
        /// <param name="chinese"></param>
        /// <returns></returns>
        public static ChineseModel ToChinese(this string chinese)
        {
            return PingYinHelper.GetFirstSpell(chinese);
        }
    }
}