#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Custom.Tool.AutoBuild
{
    public abstract class Parameter
    {
        /// <summary>
        /// Version of the project that represent a tag in source control
        /// </summary>
        [BoxGroup("General", Order = 3), OnValueChanged("OnValueChange")]
        public string Version;

        /// <summary>
        /// 
        /// </summary>
        [BoxGroup("General", Order = 3), OnValueChanged("OnValueChange")]
        public string CompanyName;

        /// <summary>
        /// 
        /// </summary>
        [BoxGroup("General", Order = 3), OnValueChanged("OnValueChange")]
        public string ProductName;

        public virtual void OnStart()
        {
            Version = PlayerSettings.bundleVersion;
            CompanyName = PlayerSettings.companyName;
            ProductName = PlayerSettings.productName;
        }

        public virtual void OnPrepare()
        {
            PlayerSettings.bundleVersion = Version;
            PlayerSettings.companyName = CompanyName;
            PlayerSettings.productName = ProductName;
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnValueChange()
        {
            PlayerSettings.bundleVersion = Version;
            PlayerSettings.companyName = CompanyName;
            PlayerSettings.productName = ProductName;
        }
    }    
}

#endif
