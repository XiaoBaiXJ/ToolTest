using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PGSqlDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TreeList treeList = new TreeList();
            treeList.OptionsBehavior.Editable = false;
            treeList.Dock = DockStyle.Fill;
            treeList.Parent = this;
            treeList.DataSource = Test.LoadData();

            SimpleButton button = new SimpleButton();
            button.Dock = DockStyle.Top;
            button.Parent = this;
            button.Height = button.CalcBestSize().Height;
            treeList.Load += (s, e) =>
            {
                treeList.ExpandAll();
                treeList.Columns["Name"].BestFit();
            };
            //IPAddress ip = new IPAddress(new byte[] { 127, 0, 0, 1 });
            //IPEndPoint iPEnd = new IPEndPoint(ip, 8000);
            TcpClient client = new TcpClient();
            try
            {
                var ar =client.BeginConnect("127.0.0.1",8000,null,null);
                ar.AsyncWaitHandle.WaitOne(5);
                var aa = client.Connected;
                var result = IsSocketConnected(client.Client);
            }
            catch
            {
                
            }
            button.Text = "Append node";
            // UI Binding
            button.Click += (sender, e) =>
            {
                // Appending a new Node
                Test spaceObject = new Test();

                spaceObject.ID = treeList.AllNodesCount;
                spaceObject.ParentID = 1;
                spaceObject.Name = "李四";
                TreeListNode newNode = treeList.AppendNode(
                    nodeData: new object[] {
                        spaceObject.ID,
                        spaceObject.ParentID,
                        spaceObject.Name
                    },
                    parentNodeId: 0
                );
                // Using the newly added node
                treeList.FocusedNode = newNode;
            };
        }
        // 检查一个Socket是否可连接  false 代表已连接true代表未连接
        private bool IsSocketConnected(Socket client)
        {
            bool blockingState = client.Blocking;
            try
            {
                byte[] tmp = new byte[1];
                client.Blocking = false;
                client.Send(tmp, 0, 0);
                return false;
            }
            catch (SocketException e)
            {
                // 产生 10035 == WSAEWOULDBLOCK 错误，说明被阻止了，但是还是连接的
                if (e.NativeErrorCode.Equals(10035))
                    return false;
                else
                    return true;
            }
            finally
            {
                client.Blocking = blockingState;    // 恢复状态
            }
        }
        public class Test
        {
            public int ID { get; set; }

            public int ParentID { get; set; }
            public string Name { get; set; }

            public static List<Test> LoadData()
            {
                return new List<Test>
                {
                    new Test{  ID=0,ParentID=-1 , Name="张三"},
                    new Test{  ID=1,ParentID=0 , Name="张三"},
                    new Test{  ID=2,ParentID=-1 , Name="张三"},
                    new Test{  ID=3,ParentID=2 , Name="张三"},
                    new Test{  ID=4,ParentID=2 , Name="张三"},
                        new Test{  ID=5,ParentID=2 , Name="张三"},
                };
            }
        }
    }
}
