using NHibernate;
using NHibernate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
         
            var temp1 = session.QueryOver<TblEmployee>().Where(it => it.FirstName == "静").List();
            //执行sql语句
            List<TblEmployee> result = session.CreateSQLQuery(" update TblEmployee  set UpdateTime='2019/12/28 16:29:00'").AddEntity(typeof(TblEmployee)).List().Cast<TblEmployee>().ToList();
            //分页查询
            int tatal=0;
            DateTime startTime=DateTime.ParseExact("20191228162400", "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
            DateTime endTime=DateTime.ParseExact("20191228162800", "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
            Expression<Func<TblEmployee, bool>> wherelambda = u => u.UpdateTime >= startTime && u.UpdateTime <= endTime;
            Expression<Func<TblEmployee, object>> orderLambda = u => u.EmployeeId;
            var lsResult = GetPageEmployees(2, 2, out tatal, wherelambda, orderLambda);
            ////插入
            TblEmployee cModel = new TblEmployee();
            cModel.FirstName = "王";
            cModel.LastName = "";
            cModel.Salary = 9500;
            session.Save(cModel);
            session.Flush();
            ////更新
            temp[0].FirstName = "修改";
            session.Update(temp[0]);
            session.Flush();
            ////删除
            session.Delete(temp[1]);
            session.Flush();
        }
        public static List<TblEmployee> GetPageEmployees(int pageSize, int pageIndex, out int total, Expression<Func<TblEmployee, bool>> wherelambda,
           Expression< Func<TblEmployee,object>> orderbyLambda)
        {
            ISession session = _nhelper.GetSession();
            total = session.QueryOver<TblEmployee>().Where(wherelambda).List().Count();
            var result = session.QueryOver<TblEmployee>().OrderBy(orderbyLambda).Asc.Where(wherelambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).List().ToList();
            return result;
        }


     
    }
}
