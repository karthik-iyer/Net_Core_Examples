using System;

namespace MVCCoreTraining.Services
{
    public abstract class DbLayer
    {
        private string _value;

        protected DbLayer()
        {
            _value = Guid.NewGuid().ToString();
        }

        public abstract void Add();
    }
}