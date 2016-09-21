using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using GoldenPalm.DAL;


namespace GoldenPalm.BL
{
    public interface IRepository<TEntity>
    {

        List<TEntity> GetAll();
        TEntity GetById(object Id);
        ReturnType Save(TEntity tEntity);

        List<TEntity> getEntityListByID(object Id);

        List<ListColumn> getListByID(object Id);
        
        List<ListColumn> getSelectList(DataView dv, string value1, string text1);
        List<ListColumn> getDropDownList(string ProcName, string value, string text);

    }

    


}
