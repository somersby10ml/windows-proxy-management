using System.Collections;

namespace windows_proxy_management
{
    public partial class Main
    {
        // Functions from MainForm

        /// <summary>
        /// Add proxy list to listview
        /// </summary>
        /// <param name="proxies">Proxy data list</param>
        private void AddListView(List<Proxy> proxies)
        {
            listView1.BeginUpdate();
            foreach (Proxy proxy in proxies)
                AddListView(proxy);
            listView1.EndUpdate();
        }

        void ListViewAutoResizeColumns()
        {
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            int[] minSize = new int[] { 50, 70, 30, 160, 60, 90, 60 };
            for (int i = 0; i < listView1.Columns.Count; i++)
                if (listView1.Columns[i].Width < minSize[i])
                    listView1.Columns[i].Width = minSize[i];
        }

        private void AddListView(Proxy proxy, bool isNew = false)
        {
            listView1.ItemCheck -= listView1_ItemCheck;
            ProxyObject proxyObject;
            if (isNew)
            {
                proxyObject = new(proxy);
            }
            else
                proxyObject = new ProxyObject(proxy);

            ListViewItem item = new(listView1.Items.Count.ToString());
            item.Checked = proxy.active;
            item.Tag = proxyObject;
            item.SubItems.Add($"{proxy.srcAddr}:{proxy.srcPort}");
            item.SubItems.Add("➔");
            item.SubItems.Add($"{proxy.destAddr}:{proxy.destPort}");
            item.SubItems.Add(proxy.type);
            item.SubItems.Add(proxy.memo);
            item.SubItems.Add("");

            if (isNew)
                item.BackColor = Color.LightGreen;

            listView1.Items.Add(item);
            listView1.ItemCheck += listView1_ItemCheck;
            ListViewAutoResizeColumns();
        }


        private List<Proxy> GetProxyData()
        {
            List<Proxy> proxies = new();

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Tag is not ProxyObject proxyObject)
                    continue;

                if (proxyObject.changed != null)
                {
                    proxies.Add(proxyObject.changed);
                }
                else
                {
                    if (proxyObject.first is Proxy)
                        proxies.Add(proxyObject.first);
                }
            }
            return proxies;
        }
        private void SaveProxyData()
        {
            List<Proxy> proxies = GetProxyData();
            Config.Save(proxies);

            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                if (listView1.Items[i].Tag is not ProxyObject proxyObject)
                    continue;

                if (proxyObject.changed == null)
                    continue;

                if (proxyObject.changed.apply == ApplyType.DELETE)
                    listView1.Items[i].Remove();
            }

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].BackColor = Color.White;
            }

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Tag is not ProxyObject proxyObject)
                    continue;
                if (proxyObject.changed != null)
                {
                    proxyObject.first = proxyObject.changed;
                    proxyObject.changed = null;
                }
            }

        }

        private void ChangeProxyData()
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].Tag is ProxyObject proxyObject)
                {
                    ChangeProxyData(listView1.Items[i], proxyObject);
                }
            }
        }
        private void ChangeProxyData(ListViewItem item, ProxyObject proxyObject)
        {
            bool isFirst = false;

            if (proxyObject.first is not null)
            {
                if (proxyObject.changed is not null)
                {
                    if (proxyObject.first.Compare(proxyObject.changed))
                    {
                        proxyObject.changed = null;
                        item.BackColor = Color.White;
                        isFirst = true;
                    }
                }
                else
                {
                    proxyObject.changed = null;
                    item.BackColor = Color.White;
                    isFirst = true;
                }
            }

            if (isFirst is false)
            {
                item.BackColor = Color.LightGreen;
            }

            if (proxyObject.changed is not null)
            {
                listView1.ItemCheck -= listView1_ItemCheck;
                item.Checked = proxyObject.changed.active;
                item.SubItems[columnHeader_Src.Index].Text = $"{proxyObject.changed.srcAddr}:{proxyObject.changed.srcPort}";
                item.SubItems[columnHeader_Dest.Index].Text = $"{proxyObject.changed.destAddr}:{proxyObject.changed.destPort}";
                item.SubItems[columnHeader_Type.Index].Text = proxyObject.changed.type;
                item.SubItems[columnHeader_Memo.Index].Text = proxyObject.changed.memo;
                listView1.ItemCheck += listView1_ItemCheck;
                return;
            }

            if (proxyObject.first is not null)
            {
                listView1.ItemCheck -= listView1_ItemCheck;
                item.Checked = proxyObject.first.active;
                item.SubItems[columnHeader_Src.Index].Text = $"{proxyObject.first.srcAddr}:{proxyObject.first.srcPort}";
                item.SubItems[columnHeader_Dest.Index].Text = $"{proxyObject.first.destAddr}:{proxyObject.first.destPort}";
                item.SubItems[columnHeader_Type.Index].Text = proxyObject.first.type;
                item.SubItems[columnHeader_Memo.Index].Text = proxyObject.first.memo;
                listView1.ItemCheck += listView1_ItemCheck;
                return;
            }
            ListViewAutoResizeColumns();
        }

        private void ListviewSelectDelete()
        {
            foreach (ListViewItem lvItem in listView1.SelectedItems)
            {
                if (lvItem.Tag is not ProxyObject proxyObject)
                    return;

                if (proxyObject.changed == null)
                {
                    if (proxyObject.first != null)
                        proxyObject.changed = proxyObject.first.Clone();
                }
                if (proxyObject.changed == null)
                {
                    return;
                }

                if (proxyObject.changed.apply == ApplyType.DELETE)
                {
                    proxyObject.changed.apply = ApplyType.NORMAL;
                    lvItem.SubItems[columnHeader_Noti.Index].Text = "";
                }
                else
                {
                    lvItem.SubItems[columnHeader_Noti.Index].Text = "Delete";
                    proxyObject.changed.apply = ApplyType.DELETE;

                }
                ChangeProxyData(lvItem, proxyObject);
            }
            ListViewAutoResizeColumns();
        }
        private void ListviewSingleModify()
        {
            ListViewItem lvItem = listView1.SelectedItems[0];

            if (lvItem.Tag is not ProxyObject proxyObject)
                return;

            if (proxyObject.changed == null)
            {
                if (proxyObject.first != null)
                    proxyObject.changed = proxyObject.first.Clone();
            }
            if (proxyObject.changed == null)
            {
                return;
            }

            ProxyForm frm = new(proxyObject.changed)
            {
                Owner = this
            };

            if (frm.ShowDialog() == DialogResult.OK)
            {
                proxyObject.changed = frm._proxy;
                ChangeProxyData(lvItem, proxyObject);
            }
            ListViewAutoResizeColumns();
        }

        private void ListviewMultiModify()
        {

            ProxyForm frm = new()
            {
                Owner = this
            };
            frm.SetMultiMode(true);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                foreach (ListViewItem lvItem in listView1.SelectedItems)
                {
                    if (lvItem.Tag is not ProxyObject proxyObject)
                        return;

                    if (proxyObject.changed == null)
                    {
                        if (proxyObject.first != null)
                            proxyObject.changed = proxyObject.first.Clone();
                    }
                    if (proxyObject.changed == null)
                    {
                        return;
                    }
                    proxyObject.changed.type = frm._proxy.type;
                    ChangeProxyData(lvItem, proxyObject);
                }
            }
            ListViewAutoResizeColumns();
        }

        /// <summary>
        /// This class is an implementation of the 'IComparer' interface.
        /// </summary>
        public class ListViewColumnSorter : IComparer
        {
            /// <summary>
            /// Specifies the column to be sorted
            /// </summary>
            private int ColumnToSort;

            /// <summary>
            /// Specifies the order in which to sort (i.e. 'Ascending').
            /// </summary>
            private SortOrder OrderOfSort;

            /// <summary>
            /// Case insensitive comparer object
            /// </summary>
            private CaseInsensitiveComparer ObjectCompare;

            /// <summary>
            /// Class constructor. Initializes various elements
            /// </summary>
            public ListViewColumnSorter()
            {
                // Initialize the column to '0'
                ColumnToSort = 0;

                // Initialize the sort order to 'none'
                OrderOfSort = SortOrder.None;

                // Initialize the CaseInsensitiveComparer object
                ObjectCompare = new CaseInsensitiveComparer();
            }

            /// <summary>
            /// This method is inherited from the IComparer interface. It compares the two objects passed using a case insensitive comparison.
            /// </summary>
            /// <param name="x">First object to be compared</param>
            /// <param name="y">Second object to be compared</param>
            /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
            public int Compare(object x, object y)
            {
                int compareResult;
                ListViewItem listviewX, listviewY;
                ProxyObject proxyObjectX, proxyObjectY;
                Proxy proxyX, proxyY;

                // Cast the objects to be compared to ListViewItem objects
                listviewX = (ListViewItem)x;
                listviewY = (ListViewItem)y;

                proxyObjectX = listviewX.Tag as ProxyObject;
                proxyObjectY = listviewY.Tag as ProxyObject;

                proxyX = proxyObjectX.changed != null ? proxyObjectX.changed : proxyObjectX.first;
                proxyY = proxyObjectY.changed != null ? proxyObjectY.changed : proxyObjectY.first;

                switch (ColumnToSort)
                {
                    case 0:
                        compareResult = ObjectCompare.Compare(int.Parse(listviewX.SubItems[ColumnToSort].Text), int.Parse(listviewY.SubItems[ColumnToSort].Text));
                        break;
                    case 1:
                        {
                            int portX, portY;
                            if (int.TryParse(proxyX.srcPort, out portX) && int.TryParse(proxyY.srcPort, out portY))
                            {
                                compareResult = ObjectCompare.Compare(portX, portY);
                            }
                            else
                            {
                                compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);
                            }
                        }
                        break;
                    default:
                        compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);
                        break;
                }



                // Calculate correct return value based on object comparison
                if (OrderOfSort == SortOrder.Ascending)
                {
                    // Ascending sort is selected, return normal result of compare operation
                    return compareResult;
                }
                else if (OrderOfSort == SortOrder.Descending)
                {
                    // Descending sort is selected, return negative result of compare operation
                    return (-compareResult);
                }
                else
                {
                    // Return '0' to indicate they are equal
                    return 0;
                }
            }

            /// <summary>
            /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
            /// </summary>
            public int SortColumn
            {
                set
                {
                    ColumnToSort = value;
                }
                get
                {
                    return ColumnToSort;
                }
            }

            /// <summary>
            /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
            /// </summary>
            public SortOrder Order
            {
                set
                {
                    OrderOfSort = value;
                }
                get
                {
                    return OrderOfSort;
                }
            }

        }
    }
}
