abstract class Robot : IKemampuan
{
    public string nama;
    public int energi;
    public int pertahanan;
    public int serangan;
    protected int Cooldownperbaikan = 0;
    protected int Cooldownseranganlistrik = 0;
    protected int Cooldownseranganplasma = 0;
    protected int Cooldownpertahanansuper = 0;
    protected int DurasiPertahanan = 0; // Menyimpan durasi aktif pertahanan super

    public Robot(string NAMA, int ENERGI, int PERTAHANAN, int SERANGAN)
    {
        this.nama = NAMA;
        this.energi = ENERGI;
        this.pertahanan = PERTAHANAN;
        this.serangan = SERANGAN;
    }

    public virtual void gunakanKemampuan(Robot Target) { }

    public void seranganplasma(Robot Target)
    {
        if (Cooldownseranganplasma > 0)
        {
            System.Console.WriteLine("Maaf skill sedang cooldown");
        }
        else
        {
            Target.energi -= (this.serangan * 10 / Target.pertahanan);
            Cooldownseranganplasma = 4; // Reset cooldown
            System.Console.WriteLine("Target berhasil diserang sebesar: " + (this.serangan * 10 / Target.pertahanan) + "! energi musuh tinggal: " + Target.energi);
        }
    }

    public void perbaikan(Robot Target)
    {
        if (Cooldownperbaikan > 0)
        {
            System.Console.WriteLine("maaf skill sedang cooldown");
        }
        else
        {
            this.energi += 20;
            Cooldownperbaikan = 2; // Reset cooldown
            System.Console.WriteLine("Energi diperbaiki, energi menjadi: " + energi);
        }
    }

    public void seranganlistrik(Robot Target)
    {
        if (Cooldownseranganlistrik > 0)
        {
            System.Console.WriteLine("maaf skill sedang cooldown");
        }
        else
        {
            Target.energi -= (this.serangan * 8 / Target.pertahanan);
            Cooldownseranganlistrik = 3; // Reset cooldown
            System.Console.WriteLine("Target berhasil diserang sebesar: " + (this.serangan * 8 / Target.pertahanan) + "! energi musuh tinggal: " + Target.energi);
        }
    }

    public void pertahanansuper(Robot Target)
    {
        if (Cooldownpertahanansuper > 0)
        {
            System.Console.WriteLine("Maaf skill sedang cooldown");
        }
        else
        {
            this.pertahanan += 8;
            Cooldownpertahanansuper = 3; // Reset cooldown
            DurasiPertahanan = 3; // Pertahanan aktif selama 3 turns
            System.Console.WriteLine("Pertahanan berhasil ditambah, pertahanan menjadi: " + this.pertahanan);
        }
    }

    public abstract void cetakInformasi();

    public void updatePertahanan()
    {
        if (DurasiPertahanan > 0)
        {
            DurasiPertahanan--; // Kurangi durasi pertahanan setiap giliran
            if (DurasiPertahanan == 0)
            {
                this.pertahanan -= 8; // Kembalikan pertahanan ke nilai awal
                System.Console.WriteLine("Efek pertahanan super telah habis. Pertahanan kembali menjadi: " + this.pertahanan);
            }
        }
    }
}

class BosRobot : Robot
{
    public BosRobot(string nama, int energi, int pertahanan, int serangan) : base(nama, energi, pertahanan, serangan) { }

    public override void gunakanKemampuan(Robot Target)
    {
        pertahanansuper(Target);
        perbaikan(Target);
    }

    public void diserang(Robot penyerang)
    {
        this.energi += 20; // cooldown berkurang 1 ketika Robot diserang

        Cooldownperbaikan -= 1;
        Cooldownpertahanansuper -= 1;
        Cooldownseranganlistrik -= 1;
        Cooldownseranganplasma -= 1;

        if (Cooldownperbaikan < 0) Cooldownperbaikan = 0;
        if (Cooldownpertahanansuper < 0) Cooldownpertahanansuper = 0;
        if (Cooldownseranganlistrik < 0) Cooldownseranganlistrik = 0;
        if (Cooldownseranganplasma < 0) Cooldownseranganplasma = 0;

        if (DurasiPertahanan > 0)
        {
            System.Console.WriteLine(this.nama + " diserang tetapi pertahanan aktif, damage berkurang.");
            this.energi -= penyerang.serangan / 2; // Kurangi damage jika pertahanan aktif
        }
        else
        {
            this.energi -= penyerang.serangan;
        }

        if (this.energi <= 0)
        {
            this.energi = 0;
            mati();
        }

        System.Console.WriteLine("Energi tersisa: " + this.energi);
    }

    private void mati()
    {
        System.Console.WriteLine("BOS ROBOT telah kehilangan energi!");
    }

    public override void cetakInformasi()
    {
        System.Console.WriteLine("NAMA: " + nama + " Energi: " + energi + " Pertahanan: " + pertahanan + " Serangan: " + serangan);
    }
}

class RobotMedium : Robot
{
    public RobotMedium(string nama, int energi, int pertahanan, int serangan) : base(nama, energi, pertahanan, serangan) { }

    public override void gunakanKemampuan(Robot target)
    {
        seranganplasma(target);
        seranganlistrik(target);
    }

    public void diserang(Robot penyerang)
    {
        this.energi += 20; // Cooldown berkurang 1 ketika diserang

        Cooldownperbaikan -= 1;
        Cooldownpertahanansuper -= 1;
        Cooldownseranganlistrik -= 1;
        Cooldownseranganplasma -= 1;

        if (Cooldownperbaikan < 0) Cooldownperbaikan = 0;
        if (Cooldownpertahanansuper < 0) Cooldownpertahanansuper = 0;
        if (Cooldownseranganlistrik < 0) Cooldownseranganlistrik = 0;
        if (Cooldownseranganplasma < 0) Cooldownseranganplasma = 0;

        if (DurasiPertahanan > 0)
        {
            System.Console.WriteLine(this.nama + " diserang tetapi pertahanan aktif, damage berkurang.");
            this.energi -= penyerang.serangan / 2; // Kurangi damage jika pertahanan aktif
        }
        else
        {
            this.energi -= penyerang.serangan;
        }

        if (this.energi <= 0)
        {
            this.energi = 0;
            mati();
        }

        System.Console.WriteLine("Energi tersisa: " + this.energi);
    }

    private void mati()
    {
        System.Console.WriteLine("MEDIUM ROBOT telah kehilangan energi!");
    }

    public override void cetakInformasi()
    {
        System.Console.WriteLine("NAMA: " + nama + " Energi: " + energi + " Pertahanan: " + pertahanan + " Serangan: " + serangan);
    }
}

interface IKemampuan
{
    void perbaikan(Robot target);
    void seranganlistrik(Robot target);
    void seranganplasma(Robot target);
    void pertahanansuper(Robot target);
}

class RunProgram
{
    static void Main(string[] args)
    {
        BosRobot robot1 = new BosRobot("BOSROBOT1", 1000, 50, 100);
        BosRobot robot2 = new BosRobot("BOSROBOT2", 2000, 100, 150);
        RobotMedium robot3 = new RobotMedium("ROBOTMEDIUM1", 500, 25, 50);

        robot1.cetakInformasi();
        robot2.cetakInformasi();

        // Simulasi beberapa serangan dan penggunaan kemampuan
        robot1.gunakanKemampuan(robot1);
        robot1.updatePertahanan();  // Kurangi durasi pertahanan super
        robot1.diserang(robot2);
        robot2.diserang(robot3);
        robot1.diserang(robot2);
        robot1.updatePertahanan();  // Durasi pertahanan terus diperbarui

        robot1.cetakInformasi();
        robot2.cetakInformasi();
    }
}
