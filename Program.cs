using System.Numerics;

class Stand
{
    protected string _namaStand;
    protected double _hargaSewaPerHari;
    protected bool _IsAvailable;

    public Stand(string namaStand, double hargaSewaPerHari, bool IsAvalilable)
    {
        _namaStand = namaStand;
        _hargaSewaPerHari = hargaSewaPerHari;
        IsAvalilable = true;
    }

    public string NamaStand
    //tidak boleh kosong / hanya berisi spasi, gunakan method = "string.IsNullorWhiteSpace ()"
    {
        get { return _namaStand; }
        set { _namaStand = value; }
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

    public void info()
    {
        Console.WriteLine($"\nNama Stand: {_namaStand}");
        Console.WriteLine($"Harga Sewa/Hari: {_hargaSewaPerHari}");
        Console.WriteLine($"Status Ketersediaan {_namaStand}: ({(IsAvailable ? "Tersedia!" : "Tidak Tersedia!")}");
    }

    public virtual double HitungTotal(int jumlahHari)
    {
        return HargaSewaPerHari * jumlahHari;
    }
}

class OStand : Stand

{
    public OStand(string namaStand, double hargaSewaPerHari, bool IsAvalilable) : base(namaStand, hargaSewaPerHari, IsAvalilable)
    { }

    protected double _biayaTenda = 75000;
    
    public double BiayaTenda
    {
        get { return _biayaTenda; }
    }

    public override double HitungTotal(int jumlahHari)
    {
        return base.HitungTotal((int)(HargaSewaPerHari * jumlahHari)) + (BiayaTenda * jumlahHari);
    }
}

class IStand : Stand
{
    public IStand(string namaStand, double hargaSewaPerHari, bool IsAvalilable) : base(namaStand, hargaSewaPerHari, IsAvalilable)
    { }

    protected double _biayaListrik = 100000;

    public double BiayaListrik
    {
        get { return _biayaListrik; }
    }
    public override double HitungTotal(int jumlahHari)
    {
        return base.HitungTotal((int)(HargaSewaPerHari * jumlahHari)) + (BiayaListrik * jumlahHari);
    }

}

class PStand : Stand
{
    public PStand(string namaStand, double hargaSewaPerHari, bool IsAvalilable) : base(namaStand, hargaSewaPerHari, IsAvalilable)
    { }

    protected double _biayaKeamanan = 300000;

    public double BiayaKeamananan
    {
        get { return _biayaKeamanan; }
    }

    public override double HitungTotal(int jumlahHari)
    {
        return base.HitungTotal((int)(HargaSewaPerHari * jumlahHari)) + BiayaKeamananan;
    }
}