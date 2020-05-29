using ConsoleApp27.DBEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading;

namespace ConsoleApp27
{

    class Program
    {
        static void Main(string[] args)
        {
            string girilecekId, girilecekBaslik, girilecekKonu;
            int girilecekSifre, girisDeger, bitir, notGiris;

            DatabaseContext db = new DatabaseContext();
            Database.SetInitializer<DatabaseContext>(null);
            //log basmak için
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            while (true)
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.Write("**");
                    Console.Write(" ");
                    Thread.Sleep(200);//200 milisaniye gecikmeli gösterim.
                }
                Console.Write("Hoşgeldiniz");
                for (int i = 0; i < 5; i++)
                {
                    Console.Write("**");
                    Console.Write(" ");
                    Thread.Sleep(200);
                }
            StartPoint:
                Console.WriteLine("\nGiriş yapmak istiyorsanız 1 tuşuna basarak giriş yapın:");
                Thread.Sleep(500);
                Console.WriteLine("Kayıt olmak için 2 tuşuna basarak kayıt olun:");
                Thread.Sleep(500);
                Console.WriteLine("Var olan kayıtları listelemek için 3 tuşuna basarak listeleyin:");
                //Klavyeden 1 2 3 dışında herhangi tuş girmesini engelleme.
                string editedInputVal = "";
                ConsoleKeyInfo key;
                do {
                    key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Backspace)
                    {
                        double val = 0;
                        bool isNumber = double.TryParse(key.KeyChar.ToString(), out val);
                        bool isOneTwoThree = ((key.KeyChar == '1') || (key.KeyChar == '2') || (key.KeyChar == '3')) ? true : false;
                        if (isNumber && isOneTwoThree && editedInputVal.Length == 0)
                        {
                            editedInputVal += key.KeyChar;
                            Console.Write(key.KeyChar);
                        }
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && editedInputVal.Length > 0)
                        {
                            editedInputVal = editedInputVal.Substring(0, (editedInputVal.Length - 1));
                            Console.Write("\b \b");
                        }
                    }
                }
                while (key.Key != ConsoleKey.Enter);


                girisDeger = Convert.ToInt32(editedInputVal);
                
                if (girisDeger == 1)
                {
                    Console.WriteLine("\nKullanıcı adı giriniz: ");
                    girilecekId = Console.ReadLine();
                    Console.WriteLine("Şifre giriniz: ");
                    girilecekSifre = Convert.ToInt32(Console.ReadLine());
                    Thread.Sleep(200);
                    Console.Write("İşleminiz yapılıyor. Lütfen bekleyiniz");
                    for (int i = 0; i < 3; i++)
                    {
                        Thread.Sleep(200);
                        Console.Write(".");
                    }
                    bool doesUserExist = db.Users.Any(x => x.kullaniciAdi == girilecekId && x.sifre == girilecekSifre);
                    if (doesUserExist)
                    {
                        Console.WriteLine("\nTebrikler giriş yaptınız.");
                        Console.WriteLine("Notlarınızı görüntülemek için 1 tuşuna basarak görüntüleyin:");
                        Thread.Sleep(1000);
                        Console.WriteLine("Not eklemek için 2 tuşuna basarak ekleyin:");
                        notGiris = Convert.ToInt32(Console.ReadLine());
                        Thread.Sleep(200);
                        Console.Write("İşleminiz yapılıyor. Lütfen bekleyiniz");
                        for (int i = 0; i < 3; i++)
                        {
                            Thread.Sleep(200);
                            Console.Write(".");
                        }
                  
                        {

                        }
                        if (notGiris == 1)
                        {
                            var user = db.Users.Include(x => x.notlar).First(x => x.kullaniciAdi == girilecekId);
                            List<Note> notlars = user.notlar;
                            //ForEach'le yapsaydım.
                            //notlars.ForEach(x => Console.WriteLine(string.Concat("Başlık: ", x.baslik, "   Konu:", x.konu)));
                            for (int i = 0; i < notlars.Count; i++)
                            {
                                Console.WriteLine(string.Concat("\nBaşlık: ", notlars[i].baslik, "     Konu: ", notlars[i].konu));
                            }
                        }
                        if (notGiris == 2)
                        {
                            Console.WriteLine("Eklemek istediğiniz başlığı ekleyiniz:");
                            girilecekBaslik = Console.ReadLine();
                            Console.WriteLine("Eklemek istediğiniz notu ekleyiniz:");
                            girilecekKonu = Console.ReadLine();
                            Thread.Sleep(200);
                            Console.Write("İşleminiz yapılıyor. Lütfen bekleyiniz");
                            for (int i = 0; i < 3; i++)
                            {
                                Thread.Sleep(200);
                                Console.Write(".");
                            }
                            var notEkle = new Note()
                            {
                                baslik = girilecekBaslik,
                                konu = girilecekKonu
                            };
                            Console.WriteLine("\nTebrikler başlığınızı ve notunuz eklediniz.");
                            //log basmak için
                            //db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                            var user = db.Users.First(x => x.kullaniciAdi == girilecekId);
                            if (user.notlar == null)
                                user.notlar = new List<Note>();
                            user.notlar.Add(notEkle);
                            db.Entry(user).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sistemde kayıtlı değilsiniz");
                        goto WrongLogin;

                    }


                    db.SaveChanges();
                }
                else if (girisDeger == 2)
                {
                    Console.WriteLine("Sisteme kayıt olacak kullanıcı adı giriniz: ");
                    girilecekId = Console.ReadLine();
                    Console.WriteLine("Sisteme kayıt olacak şifre giriniz: ");
                    girilecekSifre = Convert.ToInt32(Console.ReadLine());
                    Thread.Sleep(200);
                    Console.Write("İşleminiz yapılıyor. Lütfen bekleyiniz");
                    for (int i = 0; i < 3; i++)
                    {
                        Thread.Sleep(200);
                        Console.Write(".");
                    }
                    var user = new User()
                    {
                        kullaniciAdi = girilecekId,
                        sifre = girilecekSifre
                    };

                    if (db.Users.Any(x => x.kullaniciAdi == user.kullaniciAdi))
                    {
                        Console.WriteLine("\nBoyle kullanıcı adı var. Lütfen tekrar başka bir kullanıcı adıyla kayıt olunuz.");
                        goto StartPoint;
                    }
                    db.SaveChanges();
                    db.Users.Add(user);
                    Console.WriteLine("\nKayıtlı olundu.");
                    db.SaveChanges();
                }
                else if (girisDeger == 3)
                {
                    Thread.Sleep(200);
                    Console.Write("İşleminiz yapılıyor. Lütfen bekleyiniz");
                    for (int i = 0; i < 3; i++)
                    {
                        Thread.Sleep(200);
                        Console.Write(".");
                    }
                    List<string> kisiler = db.Users.Select(x => x.kullaniciAdi).ToList();
                    //kisiler.ForEach(x=>);
                    for (int i = 0; i < kisiler.Count; i++)
                    {
                        Console.WriteLine("\n"+string.Concat(+i + 1, ".", kisiler[i]));
                    }
                    //Eklemenin farklı yolları.
                    //List<User> users = db.Users.ToList();
                    //foreach(var user in users)
                    //{
                    //    user.
                    ////}
                    //for (int i = 0; i < users.Count; i++)
                    //{
                    //    users[i].kullaniciAdi
                    //}
                }
            WrongLogin:
                Console.WriteLine("Tekrar menüye dönmek istiyorsanız 1 tuşuna basarak geçişinizi sağlayın ya da çıkmak için herhangi bir sayı girin:");
                bitir = Convert.ToInt32(Console.ReadLine());
                if (bitir != 1)
                {
                    break;
                }
                else
                {
                    goto StartPoint;
                }
            }
        }
    }
}
