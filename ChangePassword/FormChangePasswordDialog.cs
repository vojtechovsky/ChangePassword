using System.Windows.Forms;
using System.DirectoryServices.AccountManagement;

namespace ChangePassword
{
    public partial class FormChangePasswordDialog : Form
    {
        public FormChangePasswordDialog()
        {
            InitializeComponent();
        }

        private void FormChangePasswordDialog_Load(object sender, System.EventArgs e)
        {
            this.txtUsername.Text = System.Environment.UserName;
            text_TextChanged(sender, e);
        }

        private void btnChangePassword_Click(object sender, System.EventArgs e)
        {
            if (txtNewPassword.Text != txtConfirmNewPassword.Text)
            {
                FlexibleMessageBox.Show("The Passwords you entered did not match.", "Error", MessageBoxButtons.OK);
                return;
            }


            using (var context = new PrincipalContext( ContextType.Domain ))
            {
                using (var user = UserPrincipal.FindByIdentity( context, IdentityType.SamAccountName, txtUsername.Text ))
                {
                    if (user != null)
                    {
                        user.ChangePassword( txtOldPassword.Text, txtNewPassword.Text );
                        user.Save();
                        FlexibleMessageBox.Show("Your password has been changed");
                    }
                }
            } 
        }

        private void text_TextChanged(object sender, System.EventArgs e)
        {
            var isValid =
                !string.IsNullOrWhiteSpace(txtUsername.Text) &&
                !string.IsNullOrWhiteSpace(txtOldPassword.Text) &&
                !string.IsNullOrWhiteSpace(txtNewPassword.Text) &&
                !string.IsNullOrWhiteSpace(txtConfirmNewPassword.Text);

            btnChangePassword.Enabled = isValid;
        }
    }
}
