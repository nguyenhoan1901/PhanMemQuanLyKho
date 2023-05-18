- Chạy file CSDL trong MSSQLServer
- Kết nối Entity Framework (connect tới CSDL)
- Chỉnh sửa trong các file model để bắt được sự kiện thay đổi:
VD: Thay thuộc tính Id tự tạo sẵn của Entity Framework bằng 
	public int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

- Trong dự án đã cài đặt bao gồm 
  + Interaction WPF : chạy lệnh : Install-Package System.Windows.Interactivity.WPF
  + Cài đặt Material Design: Làm theo hướng dẫn tại trang chủ Material Design hoặc vào Tools -> Nuget PackageManager -> tìm kiếm MaterialDesign
	