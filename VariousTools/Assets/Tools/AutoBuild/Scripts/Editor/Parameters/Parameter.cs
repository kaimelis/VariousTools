#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom.Tool.AutoBuild
{
    public abstract class Parameter
    {
        /// <summary>
        /// Version of the project that represent a tag in source control
        /// </summary>
        [BoxGroup("General", Order = 3), OnValueChanged("OnValueChange")]
        [OdinSerialize]
        public string Version;

        /// <summary>
        /// 
        /// </summary>
        [BoxGroup("General", Order = 3), OnValueChanged("OnValueChange")]
        [OdinSerialize]
        public string CompanyName;

        /// <summary>
        /// 
        /// </summary>
        [BoxGroup("General", Order = 3), OnValueChanged("OnValueChange")]
        [OdinSerialize]
        public string ProductName;

        public virtual void OnEnable()
        {

        }

        public virtual void OnPrepare()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnValueChange()
        {

        }
    }    
}

#endif
