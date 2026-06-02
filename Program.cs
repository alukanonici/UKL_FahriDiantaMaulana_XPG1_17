using System.Globalization;
using System.Numerics;
using System.Threading.Channels;

List<Stand> data_stand = new List<Stand>()
{
    new Stand("Outdoor Stand 1", 400000),
    new Stand("Outdoor Stand 2", 500000),
    new Stand("Indoor Stand 1", 700000),
    new Stand("Indoor Stand 2", 800000),
    new Stand("Premium Stand 1", 1800000),
    new Stand("Premium Stand 2", 2000000)
};

while (true)
{
    Console.WriteLine("___ Moklet Expo Management Center ___");
    Console.WriteLine("\nDaftar Kendaraan");
    foreach (var ds in data_stand)
    {

        ds.TampilInfo();
    }

    Console.Write("\nSilahkan pilih Stand yang ingin digunakan! ");
    Console.WriteLine("\n1. Sewa Stand\n2. Akhiri Sewa Stand\n3. Keluar");
    Console.WriteLine("Masukkan Pilihan: ");
    string pilihan = Console.ReadLine();

    if (pilihan == "1")
    {
        //penyewaan stand
        Console.WriteLine("\nInput nama Stand: ");
        string nama_Stand = Console.ReadLine();

        var cari_Stand = data_stand.FirstOrDefault(cs => string.Equals(nama_Stand, cs.NamaStand, StringComparison.OrdinalIgnoreCase));

        if (cari_Stand == null)
        {
            Console.WriteLine("\nStand tidak ditemukan");
        }
        else if (cari_Stand.IsAvailable)
        {
            Console.WriteLine("\nInput jumlah hari sewa: ");
            int hari = int.Parse(Console.ReadLine());

            double total_sewa = cari_Stand.HitungTotal(hari);

            cari_Stand.ketersediaan();

            Console.Write($"Total pembayaran sewa: Rp {total_sewa}");
        }
        else
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\nStand sedang tidak tersedia");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
    else if (pilihan == "2")
    {
        //Mengakhiri penyewaan

        Console.WriteLine("\nInput nama stand: ");
        string nama_Stand = Console.ReadLine();

        var cari_Stand = data_stand.FirstOrDefault(cs => string.Equals(nama_Stand, cs.NamaStand, StringComparison.OrdinalIgnoreCase));

        if (cari_Stand == null)
        {
            Console.WriteLine("\n Stand tidak ditemukan");
        }
        else if (!cari_Stand.IsAvailable)
        {
            cari_Stand.ketersediaan();
            Console.WriteLine("Pengakhiran masa sewa berhasil diakhiri");
        }
        else
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Stand belum disewa");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    else if (pilihan == "3")
    {
        Console.WriteLine("\nTekan ENTER untuk keluar..");
        Console.ReadLine();
        break;
    }
    else
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("\nPilihan Invalid!");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
    }
    Console.WriteLine("\nTekan ENTER untuk mengulang..");
    Console.ReadLine();
    Console.Clear();

}
class Stand
{
    protected string _namaStand;
    protected double _hargaSewaPerHari;
    protected bool _IsAvailable;

    public Stand(string namaStand, double hargaSewaPerHari)
    {
        _namaStand = namaStand;
        _hargaSewaPerHari = hargaSewaPerHari;
        _IsAvailable = true;
    }

    public string NamaStand
    {
        get { return _namaStand; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _namaStand = value;
            }
            else
            {
                Console.WriteLine("Nama Stand tidak boleh kosong atau hanya berisi spasi!");
            }
        }
    }

    public double HargaSewaPerHari
    {
        get { return _hargaSewaPerHari; }
        set
        {
            {
                if (value > 0)
                {
                    _hargaSewaPerHari = value;
                }
                else
                {
                    Console.WriteLine("Harga Sewa tidak boleh kurang dari 0!");
                }
            }
        }
    }

    public bool IsAvailable
    {
        get { return _IsAvailable; }
    }

    public void ketersediaan()
    {
        _IsAvailable = !IsAvailable;
    }

    public void TampilInfo()
    {
        Console.WriteLine($"\nNama Stand: {_namaStand} " + $"|Sewa/Hari: {_hargaSewaPerHari} " + $"|Ketersediaan Stand: ({(IsAvailable ? "Tersedia!" : "Sedang disewa!")})");
    }

    public virtual double HitungTotal(int jumlahHari)
    {
        return HargaSewaPerHari * jumlahHari;
    }
}

class OStand : Stand

{
    public OStand(string namaStand, double hargaSewaPerHari) : base(namaStand, hargaSewaPerHari)
    { }

    protected double _biayaTenda = 75000;
    
    public double BiayaTenda
    {
        get { return _biayaTenda; }
    }

    public override double HitungTotal(int jumlahHari)
    {
        return base.HitungTotal(jumlahHari) + (BiayaTenda * jumlahHari);
    }
}

class IStand : Stand
{
    public IStand(string namaStand, double hargaSewaPerHari) : base(namaStand, hargaSewaPerHari)
    { }

    protected double _biayaListrik = 100000;

    public double BiayaListrik
    {
        get { return _biayaListrik; }
    }
    public override double HitungTotal(int jumlahHari)
    {
        return base.HitungTotal(jumlahHari) + (BiayaListrik * jumlahHari);
    }

}

class PStand : Stand
{
    public PStand(string namaStand, double hargaSewaPerHari) : base(namaStand, hargaSewaPerHari)
    { }

    protected double _biayaKeamanan = 300000;

    public double BiayaKeamananan
    {
        get { return _biayaKeamanan; }
    }

    public override double HitungTotal(int jumlahHari)
    {
        return base.HitungTotal(jumlahHari) + BiayaKeamananan;
    }
}