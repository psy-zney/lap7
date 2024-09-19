using System;using System.Collections.Generic;using System.Linq;using System.Text;using System.Threading.Tasks;namespace lap5{    internal class Program    {
        
            static void Main(string[] args)
            {
            Console.OutputEncoding = Encoding.UTF8;
                // Khởi tạo tài khoản với số dư ban đầu
                TaiKhoan taiKhoan = new TaiKhoan(10000000);

                // Đăng ký sự kiện khi rút tiền
                taiKhoan.RutTien += TaiKhoan_RutTien;
            // Đăng ký sự kiện khi gửi tiền
            taiKhoan.GuiTien += TaiKhoan_GuiTien;

            // Hiển thị menu cho người dùng
            while (true)
                {
                    Console.WriteLine("******** Chào mừng đến với dịch vụ ATM ********");
                    Console.WriteLine("1. Kiểm tra số dư");
                    Console.WriteLine("2. Rút tiền");
                    Console.WriteLine("3. Gửi tiền");
                    Console.WriteLine("4. Thoát");
                    Console.WriteLine("**********************************************");
                    Console.Write("Nhập lựa chọn của bạn: ");
                
                
                    int luaChon = int.Parse(Console.ReadLine());

                    switch (luaChon)
                    {
                        case 1:
                            Console.WriteLine($"Số dư của bạn: {taiKhoan.SoDu:C}");
                            break;
                        case 2:
                            Console.Write("Nhập số tiền muốn rút: ");
                            int soTienRut = int.Parse(Console.ReadLine());
                            taiKhoan.Rut(soTienRut);
                            break;
                        case 3:
                            Console.Write("Nhập số tiền muốn gửi: ");
                            int soTienGui = int.Parse(Console.ReadLine());
                            taiKhoan.Gui(soTienGui);

                            break;
                        case 4:
                            Console.WriteLine("Cảm ơn bạn đã sử dụng dịch vụ ATM của chúng tôi. Hẹn gặp lại!");
                            return;
                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng thử lại.");
                            break;
                        
                    }
                }
            }

            // Sự kiện khi rút tiền
            private static void TaiKhoan_RutTien(object sender, TaiKhoanEventArgs e)
            {
                Console.WriteLine($"Bạn đã rút {e.SoTienRut:C}. Số dư còn lại: {e.SoDu:C}");
                Console.WriteLine("Một thông báo đã được gửi đến điện thoại  của bạn.");
            }
        // Sự kiện khi gửi tiền
        private static void TaiKhoan_GuiTien(object sender, TaiKhoanEventArgs e)
        {
            Console.WriteLine($"Bạn đã gửi {e.SoTienGui:C}. Số dư hiện tại: {e.SoDu:C}");
            Console.WriteLine("Một thông báo đã được gửi đến điện thoại của bạn.");
        }
    }

        // Lớp tài khoản
        class TaiKhoan
        {
            private int soDu;

            public event EventHandler<TaiKhoanEventArgs> RutTien;
        public event EventHandler<TaiKhoanEventArgs> GuiTien;
        public int SoDu
            {
                get { return soDu; }
            }

            public TaiKhoan(int soDuBanDau)
            {
                soDu = soDuBanDau;
            }

            public void Rut(int soTien)
            {
                if (soTien > 0 && soTien <= soDu)
                {
                    soDu -= soTien;
                    RutTien?.Invoke(this, new TaiKhoanEventArgs(soTien, soDu));
                }
                else
                {
                    Console.WriteLine("Số tiền rút không hợp lệ.");
                }
            }

            public void Gui(int soTien)
            {
                if (soTien > 0)
                {
                    soDu += soTien;
                GuiTien?.Invoke(this, new TaiKhoanEventArgs(soTien, soDu));
            }
                else
                {
                    Console.WriteLine("Số tiền gửi không hợp lệ.");
                }
            }
        }

        // Lớp đối tượng chứa thông tin sự kiện
        class TaiKhoanEventArgs : EventArgs
        {
            public int SoTienRut { get; }
            public int SoDu { get; }
        public int SoTienGui { get; }

            public TaiKhoanEventArgs(int soTienRut, int soDu)
            {
                SoTienRut = soTienRut;
                SoDu = soDu;
            }
        }
    }