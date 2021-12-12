using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryBeginners.Models
{
    public class SortModel
    {

        private string UpIcon = "fa fa-arrow-up";
        private string DownIcon = "fa fa-arrow-down";
        public string SortedProperty { get; set; }

        public SortOrder SortedOrder { get; set; }
        private List<SortableColumn> sortableColumns = new List<SortableColumn>();
       
        
        public void AddColumn(string colname, bool IsDefaultColumn = false)
        {
            SortableColumn temp = this.sortableColumns.Where(c => c.ColumnName.ToLower() == colname.ToLower()).SingleOrDefault();
            if (temp == null)
                sortableColumns.Add(new SortableColumn() { ColumnName = colname });
            

            if(IsDefaultColumn==true || sortableColumns.Count==1)
            {
                SortedProperty = colname;
                SortedOrder = SortOrder.Ascending;
            }

        }


        public SortableColumn GetColumn(string colname)
        {
            SortableColumn temp = this.sortableColumns.Where(c => c.ColumnName.ToLower() == colname.ToLower()).SingleOrDefault();
            if (temp == null)
                sortableColumns.Add(new SortableColumn() { ColumnName = colname });
            return temp;
        }

        public void ApplySort(string sortExpression)
        {

            //this.GetColumn("name").SortIcon = "";
            //this.GetColumn("name").SortExpression = "name";

            //this.GetColumn("description").SortIcon = "";
            //this.GetColumn("description").SortExpression = "description";

            if (sortExpression=="")
                sortExpression = this.SortedProperty;

            sortExpression = sortExpression.ToLower();


            foreach (SortableColumn sortableColumn in this.sortableColumns)
            {
                sortableColumn.SortIcon = "";
                sortableColumn.SortExpression = sortableColumn.ColumnName;

                if (sortExpression == sortableColumn.ColumnName.ToLower())
                {
                    this.SortedOrder = SortOrder.Ascending;
                    this.SortedProperty = sortableColumn.ColumnName;
                    sortableColumn.SortIcon = DownIcon;
                    sortableColumn.SortExpression = sortableColumn.ColumnName + "_desc";
                }

                if (sortExpression == sortableColumn.ColumnName.ToLower() + "_desc")
                {
                    this.SortedOrder = SortOrder.Descending;
                    this.SortedProperty = sortableColumn.ColumnName;
                    sortableColumn.SortIcon = UpIcon;
                    sortableColumn.SortExpression = sortableColumn.ColumnName;
                }
            }

        }
    }
    public class SortableColumn
    {
        public string ColumnName { get; set; }

        public string SortExpression { get; set; }
        public string SortIcon { get; set; }

    }
}
