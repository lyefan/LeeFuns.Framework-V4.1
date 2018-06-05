using System;

namespace LeeFuns.Utilities
{
    public interface IManageProvider
    {
        void AddCurrent(IManageUser user);
        IManageUser Current();
        void EmptyCurrent();
        bool IsOverdue();
    }
}

