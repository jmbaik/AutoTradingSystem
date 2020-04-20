using AxKHOpenAPILib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTradingSystem
{
    public partial class Form1 : Form
    {
        private List<Condition> conditionList = new List<Condition>();

        public Form1()
        {
            InitializeComponent();
            로그인ToolStripMenuItem.Click += ToolStripMenuItem_Click;
            axKHOpenAPI1.OnEventConnect += API_OnEventConnect;
            axKHOpenAPI1.OnReceiveConditionVer += API_OnReceiveConditionVer;
        }

        private void API_OnReceiveConditionVer(object sender, _DKHOpenAPIEvents_OnReceiveConditionVerEvent e)
        {
            string cList = axKHOpenAPI1.GetConditionNameList();
            Console.WriteLine(cList);
            string[] conditionArray = cList.Split(';');
            foreach (string condition in conditionArray)
            {
                if (condition.Length > 0)
                {
                    string[] conditionInfo = condition.Split('^');
                    string index = conditionInfo[0];
                    string name = conditionInfo[1];
                    conditionList.Add(new Condition(int.Parse(index), name));
                }
            }
            //매수전략 콤보박스에 추가
            foreach (Condition condition in conditionList)
            {
                buyConditionComboBox.Items.Add(condition.Name);
            }
        }

        private void API_OnEventConnect(object sender, _DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            if (e.nErrCode == 0)
            {
                // 사용자 계좌번호
                string accList = axKHOpenAPI1.GetLoginInfo("ACCLIST");
                string[] accountArray = accList.Split(';');
                foreach (string account in accountArray)
                {
                    if (account.Length > 0)
                        accountComboBox.Items.Add(account);
                }
                // 사용자 조건식
                axKHOpenAPI1.GetConditionLoad();

            }
            else
            {
                MessageBox.Show("로그인 실패");
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.Equals(로그인ToolStripMenuItem))
            {
                axKHOpenAPI1.CommConnect();
            }
        }
    }
}
