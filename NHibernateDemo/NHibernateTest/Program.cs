using NHibernate;
using NHibernate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHibernateTest
{
    class Program
    {
        private static NhibernateHelper _nhelper = new NhibernateHelper();
        static void Main(string[] args)
        {
            ISession session = _nhelper.GetSession();
            //查询
            var temp = session.QueryOver<TblEmployee>().List();
            //插入
            TblEmployee cModel = new TblEmployee();
            cModel.FirstName = "静";
            cModel.LastName = "王";
            cModel.Salary = 9500;
            session.Save(cModel);
            session.Flush();
            //更新
            temp[0].FirstName = "修改";
            session.Update(temp[0]);
            session.Flush();
            //删除
            session.Delete(temp[1]);
            session.Flush();
        }
    }
}
