using System;
using System.Data;

namespace Virtion.IM.View.Virtion.Util.Data.MySql
{
    public class Table
    {
        public String TableName
        {
            get;
            set;
        }

        private MysqlHelper mysqlHelper;
        private String whereCondition;

        public Table(String tableName, MysqlHelper mysqlHelper)
        {
            this.TableName = tableName;
            this.mysqlHelper = mysqlHelper;
        }

        public DataTable Execute(String sql)
        {
           return  this.mysqlHelper.ExecuteDataTable(sql);
        }

        public DataTable Select()
        {
            String sql = "select * from " + this.TableName;
            return this.SelectExcute(sql);
        }

        public DataTable Select(string field, params String[] fields)
        {
            String sql = "select " + field + ",";
            for (int i = 0; i < fields.Length; i++)
            {
                sql += fields[i];
                if (fields.Length - 1 != i)
                {
                    sql += " , ";
                }
            }
            sql += " from " + this.TableName;
            return this.SelectExcute(sql);
        }

        private DataTable SelectExcute(String sql)
        {
            sql += this.whereCondition;
            VDebug.Track(sql);
            return this.mysqlHelper.ExecuteDataTable(sql);
        }

        public Table Where(String conditionString)
        {
            this.whereCondition = " where " + conditionString;
            return this;
        }

        public Table Where(Condition condition)
        {
            if (condition.Keys.Count == 0)
            {
                VDebug.Error("condition is null ,it maybe couse a error!");
            }

            this.whereCondition = " where ";
            int i = 0;
            foreach (var key in condition.Keys)
            {
                this.whereCondition += key + "='" + condition[key] + "'";
                if (condition.Keys.Count - 1 != i)
                {
                    this.whereCondition += " and ";
                }
                i++;
            }
            VDebug.Track(this.whereCondition);
            return this;
        }

        public int Count()
        {
            String sql = "select count(*) from " + this.TableName;
            DataTable dataTable = this.SelectExcute(sql);
            return dataTable.Rows.Count;
        }

        public bool Insert(Condition data)
        {
            String sql = "insert into  " + this.TableName;

            int i = 0;
            String fields = "(";
            String values = "(";
            foreach (var key in data.Keys)
            {
                fields += key;
                values += "'" + data[key] + "'";
                if (data.Keys.Count - 1 != i)
                {
                    fields += " , ";
                    values += " , ";
                }
                i++;
            }
            fields += ")";
            values += ")";

            sql += fields + " value" + values;
            VDebug.Track(sql);
            this.mysqlHelper.ExecuteNonQuery(sql);
            return true;
        }

        public void Delete()
        {
            string sql = "delete from " + this.TableName + this.whereCondition;
            VDebug.Track(sql);
            this.mysqlHelper.ExecuteNonQuery(sql);
        }

        public void Update(Condition data)
        {
            String sql = "update " + this.TableName + " SET ";
            int i = 0;
            foreach (var key in data.Keys)
            {
                sql += key + "='" + data[key] + "'";
                if (data.Keys.Count - 1 != i)
                {
                    sql += " and ";
                }
                i++;
            }

            sql += this.whereCondition;
            VDebug.Track(sql);
            this.mysqlHelper.ExecuteNonQuery(sql);
        }

    }
}
