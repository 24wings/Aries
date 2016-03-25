using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using CYQ.Data;
using CYQ.Data.Cache;
using System.Collections;
using System.Xml.Linq;
using Web.Utility.ADHelper;
using System.IO;

namespace Web.Utility
{
    public class FileHelper
    {
        private static object _lock = new object();
        #region[���캯��]
        public static FileHelper Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_Instance == null)
                        _Instance = new FileHelper();
                    return _Instance;
                }                
            }
        }
        private static FileHelper _Instance;
		/// <summary>
		/// �ļ�io������
		/// </summary>
		private static FileManager _CurFileManager;
		public static  FileManager CurFileManager
		{
			get
			{
				if (_CurFileManager == null)
					_CurFileManager = FileIdentityFactory.GetCurFileManager();
				return _CurFileManager;
			}
		}

		/// <summary>
		/// Զ�̹����·��
		/// </summary>
		private string _SharePath;
		public string ShareRootPath
		{
			get
			{
				if (_SharePath == null)
				{
					_SharePath = this.GetShareRootPath();
				}
				return _SharePath;
			}
		}
        #endregion

        

        #region �ļ���������
        #region �ϴ���ʽ
        public string GetUploadType()
        {
            return GetConfigByKeyAndName("�ļ���������Ϣ", "�ϴ���ʽ")["ConfigValue"].ToString();
        }
        #endregion

        #region Զ�̹�������
        /// <summary>
        /// ��ȡԶ�̹���Ŀ¼
        /// </summary>
        /// <returns></returns>
        public string GetSharePath()
        {
            return GetConfigByKeyAndName("�ļ���������Ϣ", "����·��")["ConfigValue"].ToString();
        }

        /// <summary>
        /// ��ȡ������û���
        /// </summary>
        /// <returns></returns>
        public string GetShareUserId()
        {
            return GetConfigByKeyAndName("�ļ���������Ϣ", "�����û���")["ConfigValue"].ToString();
        }

        /// <summary>
        /// ��ȡ������û�����
        /// </summary>
        /// <returns></returns>
        //public string GetSharePwd()
        //{
        //    var password = GetConfigByKeyAndName("�ļ���������Ϣ", "��������")["ConfigValue"].ToString();
        //    return EncrpytHelper.Decrypt(password);
        //}
        #endregion

        #region ��ȡ����Ϣ
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <returns></returns>
        public string GetDomainImpersonation()
        {
            //return GetConfigByKeyAndName("�ļ���������Ϣ", "������").ConfigValue;
            return GetConfigByKeyAndName("�ļ���������Ϣ", "������")["ConfigValue"].ToString();
        }

        /// <summary>
        /// ��ȡ���Ŀ¼
        /// </summary>
        /// <returns></returns>
        public string GetDomainRootFolder()
        {
            //return GetConfigByKeyAndName("�ļ���������Ϣ", "��·��").ConfigValue;
            return GetConfigByKeyAndName("�ļ���������Ϣ", "��·��")["ConfigValue"].ToString();
        }

        /// <summary>
        /// ��ȡ���û�
        /// </summary>
        /// <returns></returns>
        public string GetDomainImpersonationUser()
        {
            //return GetConfigByKeyAndName("�ļ���������Ϣ", "���û���").ConfigValue;
            return GetConfigByKeyAndName("�ļ���������Ϣ", "���û���")["ConfigValue"].ToString();
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <returns></returns>
        public string GetDomainImpersonationPass()
        {
            //EncrpytUtility encrpyt = new EncrpytUtility();
            //return encrpyt.Decrypt3DES(GetConfigByKeyAndName("�ļ���������Ϣ", "������").ConfigValue);
            return GetConfigByKeyAndName("�ļ���������Ϣ", "������")["ConfigValue"].ToString();
        }
        #endregion

        #region ����Ŀ¼
        /// <summary>
        /// �����ϴ���Ŀ¼
        /// </summary>
        /// <returns></returns>
        public string GetLocalPath()
        {
            return  HttpContext.Current.Server.MapPath(GetConfigByKeyAndName("�ļ���������Ϣ", "����·��")["ConfigValue"].ToString());
        }
        #endregion

        #region ��վ������Ϣ
        /// <summary>
        /// ������ѹ�����·��
        /// </summary>
        /// <returns></returns>
        public string GetWebTempPath()
        {
            return GetConfigByKeyAndName("��վ��Ϣ", "��ʱĿ¼")["ConfigValue"].ToString();
        }

        /// <summary>
        /// ͼƬѹ����С
        /// </summary>
        /// <returns></returns>
        public double GetCompressZize()
        {
            return Convert.ToInt32(GetConfigByKeyAndName("��վ��Ϣ", "ͼƬѹ����С")["ConfigValue"]);
        }

        /// <summary>
        /// ͼƬѹ������
        /// </summary>
        /// <returns></returns>
        public int GetCompressQuality()
        {
            return Convert.ToInt32(GetConfigByKeyAndName("��վ��Ϣ", "ͼƬѹ������")["ConfigValue"]);
        }

        /// <summary>
        /// ͼƬ��Ҫѹ��������
        /// </summary>
        /// <returns></returns>
        public int GetCompressPicUp()
        {
            return Convert.ToInt32(GetConfigByKeyAndName("��վ��Ϣ", "ͼƬѹ������")["ConfigValue"]);
        }

        //public IList<Config> GetFirstZhuanYe()
        //{
        //    string sql = string.Format("select * from PB_config where ConfigKey='{0}'and ConfigValue={1}", "רҵ���", 1);
        //    IList<Config> model = base.GetList(sql, null, false);
        //    return model;
        //}
        #endregion

		#region ����Ŀ¼
		/// <summary>
		/// ����Ŀ¼·��
		/// </summary>
		/// <returns></returns>
		public string GetVirtualDirePath()
		{
            return  HttpContext.Current.Server.MapPath(GetConfigByKeyAndName("�ļ���������Ϣ", "����Ŀ¼·��")["ConfigValue"].ToString());
		}
		#endregion

        #region ϵͳ��Ϣ
        /// <summary>
        /// ������ѹ�����·��
        /// </summary>
        /// <returns></returns>
        public string GetSystemRarSoftPath()
        {
            return GetConfigByKeyAndName("ϵͳ��Ϣ", "Rar·��")["ConfigValue"].ToString();
        }
        #endregion

        #region ��ȡ����·��
        /// <summary>
        /// ��ȡ����ĸ�Ŀ¼
        /// </summary>
        /// <returns></returns>
        public string GetShareRootPath()
        {
            string _SharePath = null;
            string identityType = GetUploadType();
            //GetConfigByKeyAndName("�ļ���������Ϣ", "�ϴ���ʽ").ConfigValue;
            if (identityType == "1")
            {
                _SharePath = GetSharePath();
            }
            else if (identityType == "2")
                _SharePath = GetDomainRootFolder();
			else if (identityType == "4")
				_SharePath = GetVirtualDirePath();
            else
                _SharePath = GetLocalPath();
            return _SharePath;
        }
        #endregion
        #endregion

        private const string CACH_KEY = "PB_ConfigDALList";

        private DataTable TableSource
        {
            get
            {
                var configSource = CacheManage.Instance.Get(CACH_KEY);
                if (configSource == null)
                {
                    using (MAction action = new MAction("PB_Config"))
                    {
                        return action.Select().ToDataTable();
                    }
                }
                else
                {
                    return configSource as DataTable;
                }
            }
        }

        private DataRow GetConfigByKeyAndName(string configKey,string configName)
        {
            return TableSource.Select(string.Format(" ConfigKey = '{0}' AND ConfigName = '{1}'",configKey,configName))[0];
        }

    }
}

