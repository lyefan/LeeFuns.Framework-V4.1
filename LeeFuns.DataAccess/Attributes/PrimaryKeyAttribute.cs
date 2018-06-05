namespace LeeFuns.DataAccess.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class PrimaryKeyAttribute : Attribute
    {
        private string _name;

        public PrimaryKeyAttribute()
        {
        }

        public PrimaryKeyAttribute(string name)
        {
            this._name = name;
        }

        public virtual string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
    }
}

