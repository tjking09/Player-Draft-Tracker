using System.Data;
using System.Data.SqlClient;
using System.Windows;


namespace ProspectUI
{

    public partial class MainWindow : Window
    {
        //global variable for connection string
        public string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Projects;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public MainWindow()
        {
            InitializeComponent();
            SqlConnection conn = new SqlConnection(connString);

            string sql = "select * from Prospects";

            SqlCommand cmd = new SqlCommand(sql, conn);

            conn.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();

            ProspectsData.DataContext = dt;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Set up SQL Server Connection
            SqlConnection conn = new SqlConnection(connString);
            // SQL Query to execute later
            string sqlWrite = "insert into Prospects ([name], [position], [overall], [potential], [draft year], [draft round], [draft spot], [age]) values (@name, @position, @overall, @potential, @draftyear, @draftround, @draftspot, @age)";
            string sqlRead = "select * from Prospects";
            // Open the Server connection
            conn.Open();
            // Assign values to the parameters and execute the above querey 
            using (SqlCommand cmd = new SqlCommand(sqlWrite, conn))
            {
                // Update parameter with the object variable, if empty, then add DB Null value
                cmd.Parameters.AddWithValue("@name", playerNameText.Text);
                cmd.Parameters.AddWithValue("@position", playerPositionText.Text);
                cmd.Parameters.AddWithValue("@overall", overallText.Text);
                cmd.Parameters.AddWithValue("@potential", potentialText.Text);
                cmd.Parameters.AddWithValue("@draftyear", draftYearText.Text);
                cmd.Parameters.AddWithValue("@draftround", draftRoundText.Text);
                cmd.Parameters.AddWithValue("@draftspot", draftSpotText.Text);
                cmd.Parameters.AddWithValue("@age", playerAgeText.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("New Prospect Added");
            }
            using (SqlCommand cmd = new SqlCommand(sqlRead, conn)) // convert data into a data table
            {
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                ProspectsData.DataContext = dt;
            }
            conn.Close();
        }
    }
}
