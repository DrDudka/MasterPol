using System;
using System.Windows.Forms;

namespace УП_PR1
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        private void buttonAuth_Click(object sender, EventArgs e)
        {
            string login = txtUsername.Text;
            string password = txtPassword.Text;

            AuthService authService = new AuthService();
            int userId = authService.AuthUser(login, password);

            if (userId == -1)
            {
                MessageBox.Show("Неверный логин или пароль.");
                return;
            }

            MenuForm menuForm = new MenuForm(userId);
            menuForm.Show();
            this.Hide();
        }
    }
}
