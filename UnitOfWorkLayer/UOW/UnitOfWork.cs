using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessAccessLayer.Repo;
using DataAccessLayer.Model;

namespace UnitOfWorkLayer.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private BusinessLayer _BusinessLayer;
        private Practice2Entities _Entity = new Practice2Entities();
        
        //BusinessAccessLayer interface
        public IBusinessLayer BusinessLayer
        {
            get
            {
                if(_BusinessLayer == null)
                {
                    _BusinessLayer = new BusinessLayer(_Entity);
                }
                return _BusinessLayer;
            }
        }

       //Add data in DbContex
        public void Commit()
        {
            _Entity.SaveChanges();
        }

        //Return if transection is not complete
        public string RollBack()
        {
            return "Transaction Failed";
        }
    }
}
