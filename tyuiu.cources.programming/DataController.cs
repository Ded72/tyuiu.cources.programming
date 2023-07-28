using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tyuiu.cources.programming.interfaces;

namespace tyuiu.cources.programming
{
    public class DataController
    {
        private Dictionary<Type, DataInfo>? dataMap;
        public DataController()
        {
            InitData();
        }
        public (object? result, object[]? param) GetData<T>()
        {
            var data = dataMap?[typeof(T)];
            if (data == null) { throw new ArgumentNullException(nameof(data)); }
            return (data.result, data.param);
        }
        private void InitData()
        {
            this.dataMap = new Dictionary<Type, DataInfo>() {
                {
                    typeof(ISprint0Task0V0),
                    new DataInfo {
                        result = "54321",
                        param = new object[] { "12345" }
                    }
                }
            };
        }
    }
}
