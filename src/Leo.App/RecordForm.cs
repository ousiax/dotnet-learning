using Leo.App.ViewModels;

namespace Leo.App
{
    public partial class RecordForm : Form
    {
        private readonly MemberDetailViewModel _detail;

        public RecordForm(MemberDetailViewModel detail)
        {
            _detail = detail;
            InitializeComponent();
            InitializeDataBinding();
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            this.btnConfirm.Click += (s, a) =>
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            this.btnCancel.Click += (s, a) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };
        }

        private void InitializeDataBinding()
        {
            this.dtpDate.DataBindings.Add(new Binding("Value", _detail, "Date"));
            this.txtItem.DataBindings.Add(new Binding("Text", _detail, "Item"));
            this.txtCount.DataBindings.Add(new Binding("Text", _detail, "Count"));
            this.txtHeight.DataBindings.Add(new Binding("Text", _detail, "Height"));
            this.txtWeight.DataBindings.Add(new Binding("Text", _detail, "Weight"));
        }

        private void RecordForm_Load(object sender, EventArgs e)
        {

        }
    }
}
