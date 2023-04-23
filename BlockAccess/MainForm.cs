using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BlockAccess
{
    public partial class MainForm : Form
    {
        List<EntryHost> entryHosts = new List<EntryHost>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void FomatGrid()
        {
            dgvLstHostEntry.Columns["Host"].Visible = false;

            dgvLstHostEntry.Columns["Domain"].HeaderText = "Domain";
            dgvLstHostEntry.Columns["Domain"].Width = 390;
            dgvLstHostEntry.Columns["Domain"].ReadOnly = true;

            dgvLstHostEntry.Columns["IsBlock"].HeaderText = "Blocked";
            dgvLstHostEntry.Columns["IsBlock"].ReadOnly = true;

            dgvLstHostEntry.AllowUserToResizeRows = false;
            dgvLstHostEntry.AllowUserToResizeColumns = false;
            var selCol = new DataGridViewCheckBoxColumn();
            selCol.HeaderText = "";
            selCol.DisplayIndex = 0;
            selCol.ValueType = typeof(bool);
            dgvLstHostEntry.Columns.Add(selCol);

            dgvLstHostEntry.RowHeadersVisible = false;

        }

        private void InitData(List<EntryHost> entryHosts)
        {
            dgvLstHostEntry.DataSource = entryHosts;
            lblTotal.Text = entryHosts.Count.ToString();
            ckbOnGroupAction.Checked = true;
            cbbGroup.Enabled = true;
            cbbGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cbbGroup.Items.Add("- select group -");
            string[] group = Lib.GetAllGroup();
            foreach (string groupItem in group)
            {
                cbbGroup.Items.Add(groupItem);
            }

            cbbGroup.SelectedIndex = 0;
            FomatGrid();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Lib.Init();
            entryHosts = Lib.GetAllEntryHosts();
            InitData(entryHosts);
        }

        private void ReloadData()
        {
            entryHosts = Lib.GetAllEntryHosts();
            UpdateDataSource(entryHosts);
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(txtDomain.Text.Trim(), "^[a-zA-Z0-9]+([-.][a-zA-Z0-9]+)*\\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show("Invalid Domain!");
                return;
            }
            Lib.AddNewDomain(new List<string>() { txtDomain.Text.Trim() });
            txtDomain.Text = string.Empty;
            ReloadData();
        }

        private void UpdateDataSource(List<EntryHost> entryHosts)
        {

            dgvLstHostEntry.DataSource = null;

            dgvLstHostEntry.Columns.Clear();

            dgvLstHostEntry.DataSource = entryHosts;

            lblTotal.Text = entryHosts.Count.ToString();

            FomatGrid();

            dgvLstHostEntry.Refresh();
        }

        private void txtKeyword_TextChanged(object sender, EventArgs e)
        {
            var result = entryHosts.AsEnumerable().Where(obj => obj.Domain == null || obj.Domain.Contains(txtKeyword.Text)).ToList();
            UpdateDataSource(result);
        }

        private void dgvLstHostEntry_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            int rowIndex = e.RowIndex + 1;

            // Create a new rectangle for the index cell
            Rectangle rect = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, ((DataGridView)sender).RowHeadersWidth - 4, e.RowBounds.Height);

            // Draw the row index in the index cell
            TextRenderer.DrawText(e.Graphics, rowIndex.ToString(), ((DataGridView)sender).DefaultCellStyle.Font, rect, ((DataGridView)sender).DefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void btnBlock_Click(object sender, EventArgs e)
        {
            if (ckbOnGroupAction.Checked)
            {
                UpdateGroupDomain(MODE.BLOCK);
            }
            else
            {
                UpdateDomain(MODE.BLOCK);
            }
            txtKeyword.Text = string.Empty;
            ReloadData();
        }

        private void btnUnBlock_Click(object sender, EventArgs e)
        {
            if (ckbOnGroupAction.Checked)
            {
                UpdateGroupDomain(MODE.UNBLOCK);
            }
            else
            {
                UpdateDomain(MODE.UNBLOCK);

            }
            txtKeyword.Text = string.Empty;
            ReloadData();
        }

        private void UpdateDomain(MODE mode)
        {

            List<string> listBlock = new List<string>();
            foreach (DataGridViewRow row in dgvLstHostEntry.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells[3] as DataGridViewCheckBoxCell;

                if (checkBoxCell != null && checkBoxCell.Value != null && (bool)checkBoxCell.Value == true)
                {
                    listBlock.Add(row.Cells["Domain"].Value.ToString());
                }
            }

            Lib.UpdateDomain(listBlock, mode);
        }

        private void UpdateGroupDomain(MODE mode)
        {
            if (cbbGroup.SelectedIndex != 0)
                Lib.BlockGroup(cbbGroup.SelectedItem.ToString(), mode);
        }

        private void ckbOnGroupAction_CheckedChanged(object sender, EventArgs e)
        {
            cbbGroup.Enabled = ckbOnGroupAction.Checked;
        }

        private void txtDomain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnAddNew_Click(null, null);
            }
        }
    }
}