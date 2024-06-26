﻿using System;
using System.Threading;

// Interface untuk Skill
public interface ISkill
{
    void GunakanSkill(Karakter pengguna, Karakter target);
    string GetDeskripsi();
}

// Abstract class untuk Karakter
public abstract class Karakter
{
    public string Nama { get; set; }
    public int HP { get; set; }
    public int MP { get; set; }
    public int Kekuatan { get; set; }

    // Constructor
    public Karakter(string nama, int hp, int mp, int kekuatan)
    {
        Nama = nama;
        HP = hp;
        MP = mp;
        Kekuatan = kekuatan;
    }

    // Method serang
    public virtual void Serang(Karakter target)
    {
        Console.WriteLine($"{Nama} menyerang {target.Nama}!");
        target.Serang(this);
    }

    // Method gunakanSkill
    public abstract void GunakanSkill(ISkill skill, Karakter target);

    // Method cetakInformasi
    public virtual void CetakInformasi()
    {
        Console.WriteLine($"Nama: {Nama}, HP: {HP}, MP: {MP}, Kekuatan: {Kekuatan}");
    }

    // Method untuk mengecek apakah karakter masih hidup
    public bool ApakahHidup()
    {
        return HP > 0;
    }
}

// Class untuk Karakter Hero
public class Hero : Karakter
{
    // Constructor
    public Hero(string nama, int hp, int mp, int kekuatan) : base(nama, hp, mp, kekuatan)
    {
    }

    // Implementasi method abstrak GunakanSkill dari kelas Karakter
    public override void GunakanSkill(ISkill skill, Karakter target)
    {
        skill.GunakanSkill(this, target);
    }
}

// Class untuk Musuh
public class Musuh : Karakter
{
    public Musuh(string nama, int hp, int kekuatan) : base(nama, hp, 0, kekuatan)
    {
    }

    // Method diserang
    public override void Serang(Karakter karakter)
    {
        HP -= karakter.Kekuatan;
        Console.WriteLine($"{Nama} menerima serangan dari {karakter.Nama}!");
        if (!ApakahHidup())
            Mati();
    }

    // Method mati
    public void Mati()
    {
        Console.WriteLine($"{Nama} telah mati.");
    }

    // Implementasi method GunakanSkill untuk Musuh
    public override void GunakanSkill(ISkill skill, Karakter target)
    {
        // Musuh tidak dapat menggunakan skill
        Console.WriteLine($"{Nama} tidak dapat menggunakan skill.");
    }
}

// Class untuk Skill Heal
public class HealSkill : ISkill
{
    public void GunakanSkill(Karakter pengguna, Karakter target)
    {
        pengguna.HP += 20; // Menambah HP pengguna sebanyak 20
        Console.WriteLine($"{pengguna.Nama} menggunakan skill Heal, HP bertambah menjadi {pengguna.HP}");
    }

    public string GetDeskripsi()
    {
        return "Heal: Memulihkan HP karakter.";
    }
}

// Class untuk Skill Fireball
public class FireballSkill : ISkill
{
    public void GunakanSkill(Karakter pengguna, Karakter target)
    {
        target.HP -= 30; // Mengurangi HP target sebanyak 30
        Console.WriteLine($"Fireball dari {pengguna.Nama} mengenai {target.Nama}, HP berkurang menjadi {target.HP}");
    }

    public string GetDeskripsi()
    {
        return "Fireball: Menyerang musuh dengan damage api.";
    }
}

// Class untuk Skill Ice Blast
public class IceBlastSkill : ISkill
{
    public void GunakanSkill(Karakter pengguna, Karakter target)
    {
        target.HP -= 20; // Mengurangi HP target sebanyak 20
        Console.WriteLine($"Ice Blast dari {pengguna.Nama} mengenai {target.Nama}, HP berkurang menjadi {target.HP}");
        Console.WriteLine($"{target.Nama} terkena efek memperlambat.");
    }

    public string GetDeskripsi()
    {
        return "Ice Blast: Menyerang musuh dengan damage es dan memperlambat mereka.";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(" Selamat Datang di Game Bobodu ");
        Console.WriteLine("Inilah pertarungan antara Boboboy dan Adudu!");

        // Inisialisasi karakter hero
        Hero Boboboy = new Hero("Boboboy", 100, 50, 10);

        // Inisialisasi musuh
        Musuh Adudu = new Musuh("Adudu", 80, 15);

        // Inisialisasi skill
        ISkill heal = new HealSkill();
        ISkill fireball = new FireballSkill();
        ISkill iceBlast = new IceBlastSkill();

        // Menampilkan informasi karakter dan musuh sebelum pertarungan dimulai
        Console.WriteLine("\nInformasi Karakter:");
        Boboboy.CetakInformasi();
        Console.WriteLine("\nInformasi Musuh:");
        Adudu.CetakInformasi();

        Console.WriteLine("\n== Mulai Pertarungan ==\n");

        // Delay untuk efek dramatis
        Thread.Sleep(2000);

        // Looping permainan sampai salah satu karakter kalah
        while (Boboboy.ApakahHidup() && Adudu.ApakahHidup())
        {
            // Menampilkan informasi pilihan skill
            Console.WriteLine("Pilih skill yang akan digunakan:");
            Console.WriteLine($"1. {heal.GetDeskripsi()}");
            Console.WriteLine($"2. {fireball.GetDeskripsi()}");
            Console.WriteLine($"3. {iceBlast.GetDeskripsi()}");

            int pilihanSkill = int.Parse(Console.ReadLine());

            // Menggunakan skill sesuai pilihan
            switch (pilihanSkill)
            {
                case 1:
                    Boboboy.GunakanSkill(heal, Adudu);
                    break;
                case 2:
                    Boboboy.GunakanSkill(fireball, Adudu);
                    break;
                case 3:
                    Boboboy.GunakanSkill(iceBlast, Adudu);
                    break;
                default:
                    Console.WriteLine("Pilihan tidak valid.");
                    break;
            }

            // Musuh melakukan serangan balasan jika masih hidup
            if (Adudu.ApakahHidup())
                Adudu.Serang(Boboboy);

            // Cetak informasi karakter dan musuh setelah menggunakan skill dan serangan musuh
            Console.WriteLine("\nInformasi setelah menggunakan skill dan serangan musuh:");
            Boboboy.CetakInformasi();
            Adudu.CetakInformasi();
        }

        if (Boboboy.ApakahHidup())
            Console.WriteLine($"{Boboboy.Nama} menang!");
        else
            Console.WriteLine($"{Boboboy.Nama} menang!");
    }
}