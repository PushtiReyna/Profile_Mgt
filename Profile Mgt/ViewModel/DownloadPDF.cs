namespace Profile_Mgt.ViewModel
{
    public class DownloadPDF
    {
        public string FullName { get; set; } = null!;

        //public string? Middlename { get; set; }

        //public string Lastname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime Dob { get; set; }

        public string Address { get; set; } = null!;

        public int Pincode { get; set; }
    }
}
