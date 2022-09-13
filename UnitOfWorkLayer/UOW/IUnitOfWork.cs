using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessAccessLayer.Repo;

namespace UnitOfWorkLayer.UOW
{
    public interface IUnitOfWork
    {
        IBusinessLayer BusinessLayer { get; }
        void Commit();
        string RollBack();
    }
}
