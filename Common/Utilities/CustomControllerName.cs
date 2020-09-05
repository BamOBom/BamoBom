using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utilities
{
    [AttributeUsage(validOn: AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class CustomControllerNameAttribute : System.Attribute
    {
        private string _displayNmae;
        public CustomControllerNameAttribute(string displayNmae)
        {
            _displayNmae = displayNmae;
        }

        public string Name
        {
            get => _displayNmae;
            private set => _displayNmae = value;
        }
    }

    [AttributeUsage(validOn: AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class CustomActionNameAttribute : System.Attribute
    {
        private string _displayNmae;
        public CustomActionNameAttribute(string displayNmae)
        {
            _displayNmae = displayNmae;
        }

        public string Name
        {
            get => _displayNmae;
            private set => _displayNmae = value;
        }
    }
}
