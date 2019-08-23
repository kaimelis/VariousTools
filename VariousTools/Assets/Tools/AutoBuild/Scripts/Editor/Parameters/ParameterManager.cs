#if UNITY_EDITOR
using System.Collections.Generic;
namespace Custom.Tool.AutoBuild
{
    public class ParameterManager
    {
        private static ParameterManager _instance;
        private List<Parameter> _allParameters = new List<Parameter>();

        public static ParameterManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ParameterManager();
                return _instance;
            }
            private set
            {
                _instance = new ParameterManager();
            }
        }
        

        public void RegisterParameter(Parameter param)
        {
            if (param != null)
                _allParameters.Add(param);
        }


        public void OnUpgradeAllParameters()
        {
            if (_allParameters.Count == 0)
                return;
            foreach (var item in _allParameters)
            {
                item?.OnPrepare();
            }
        }
    }
}
#endif
