using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manifesto
{
    // https://msdn.microsoft.com/en-us/library/bb756929.aspx
    class ManifestInfo
    {
        private string m_level;
        private string m_uiAccess;
        private string m_dpiAware;
        private string m_autoElevate;

        public string Level
        {
            get
            {
                return m_level;
            }
            set
            {
                m_level = value;
            }
        }

        public string uiAccess
        {
            get
            {
                return m_uiAccess;
            }
            set
            {
                m_uiAccess = value;
            }
        }

        public string dpiAware
        {
            get
            {
                return m_dpiAware;
            }
            set
            {
                m_dpiAware = value;
            }
        }

        public string autoElevate
        {
            get
            {
                return m_autoElevate;
            }
            set
            {
                m_autoElevate = value;
            }
        }
    }
}
