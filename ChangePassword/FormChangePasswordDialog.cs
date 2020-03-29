﻿using System.Windows.Forms;
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
            
            using (var context = new PrincipalContext(ContextType.Machine))
            {
                var isCorrect = context.ValidateCredentials(txtUsername.Text, txtOldPassword.Text);

                if (!isCorrect)
                {
                    FlexibleMessageBox.Show("Your old password is NOT correct!");
                    return;
                }

                using (var user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, txtUsername.Text))
                {
                    if (user != null)
                    {
                        if (user.UserCannotChangePassword)
                        {
                            FlexibleMessageBox.Show("User cannot change password", "Error");
                            return;
                        }
                        
                        user.ChangePassword(txtOldPassword.Text, txtNewPassword.Text);
                        FlexibleMessageBox.Show("Your password has been changed", "Success");
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
