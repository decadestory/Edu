﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Atom.Lib.Security
{
    public class CryptographyUtils
    {
        public static string MD5(string text, string encoding = "utf-8")
        {
            byte[] bytes = Encoding.GetEncoding(encoding).GetBytes(text);

            using (MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                md5.ComputeHash(bytes);
                byte[] hashBytes = md5.Hash;
                md5.Clear();
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public static string SHA1(string text, string encoding = "utf-8")
        {
            byte[] bytes = Encoding.GetEncoding(encoding).GetBytes(text);

            using (SHA1Managed sha1 = new SHA1Managed())
            {
                sha1.ComputeHash(bytes);
                byte[] hashBytes = sha1.Hash;
                sha1.Clear();
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public static string SHA256(string text, string encoding = "utf-8")
        {
            byte[] bytes = Encoding.GetEncoding(encoding).GetBytes(text);

            using (SHA256Managed sha256 = new SHA256Managed())
            {
                sha256.ComputeHash(bytes);
                byte[] hashBytes = sha256.Hash;
                sha256.Clear();
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public static string SHA384(string text, string encoding = "utf-8")
        {
            byte[] bytes = Encoding.GetEncoding(encoding).GetBytes(text);

            using (SHA384Managed sha384 = new SHA384Managed())
            {
                sha384.ComputeHash(bytes);
                byte[] hashBytes = sha384.Hash;
                sha384.Clear();
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public static string SHA512(string text, string encoding = "utf-8")
        {
            byte[] bytes = Encoding.GetEncoding(encoding).GetBytes(text);

            using (SHA512Managed sha512 = new SHA512Managed())
            {
                sha512.ComputeHash(bytes);
                byte[] hashBytes = sha512.Hash;
                sha512.Clear();
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public static Tuple<string, string> Pwd(string pwd)
        {
            var salt = Guid.NewGuid().ToString("N");
            var enPwd = SHA1(pwd+salt);
            return new Tuple<string, string>(enPwd,salt);
        }

        public static Tuple<string, string> Pwd(string pwd,string salt)
        {
            var enPwd = SHA1(pwd + salt);
            return new Tuple<string, string>(enPwd, salt);
        }

    }
}
